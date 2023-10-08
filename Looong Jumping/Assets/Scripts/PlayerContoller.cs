using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerContoller : MonoBehaviour
{
    //Playerinfo
    public PlayerInfo playerInfo;

    public float groundCheckDistance = 0.5f;
    public float angleIncrement = 1f;
    private float moveSpeed;

    private bool isOnStartLine;
    private bool isJumpButtonClick;
    private bool hasLanded = false;

    public FloatingJoystick joystick;
    public LayerMask groundLayer;
    public Button jumpButton;
    public Button accelerationButton;
    
    public ObjectSpawner spawner;
    public BarContoller barContoller;

    private Rigidbody rb;

    private float rotateX = 0f;
    private float rotateY = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystick.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLanding)
        {
            float x = joystick.Horizontal;
            float y = joystick.Vertical;

            rotateX += x;
            rotateY += y;

            Vector3 moveDirection = rb.transform.forward;

            var position = rb.position;
            position += moveDirection * moveSpeed;
            rb.MovePosition(position);
            rb.MoveRotation(Quaternion.Euler(-rotateY, rotateX, 0));
        }
        else
        {
            rb.rotation = Quaternion.identity;
        }
    }

    private void Update()
    {
        if (!hasLanded && Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer))
        {
            hasLanded = true;
            // ¶¥¿¡ ´ê¾ÒÀ» ¶§ÀÇ µ¿ÀÛ
            //rb.velocity = Vector3.zero; // ¿òÁ÷ÀÓ ¸ØÃã
            moveSpeed = 0;
            playerInfo.money += (int)UIManager.instance.score;

            joystick.gameObject.SetActive(false);
            GameManager.instance.Landing();
        }

        if(isJumpButtonClick)
        {
            SetGauge();
        }
    }

    public void JumpButtonDown()
    {
        isJumpButtonClick = true;
        Time.timeScale = 0f;
        barContoller.gameObject.SetActive(true);
    }

    public void SetGauge()
    {
        barContoller.angleValue += angleIncrement;
        if (barContoller.angleValue > 100f)
        {
            angleIncrement = -1f;
        }
        else if (barContoller.angleValue < 0f)
        {
            angleIncrement = 1f;
        }
    }

    public void JumpButtonUp()
    {
        isJumpButtonClick = false;
        Time.timeScale = 1f;
        barContoller.gameObject.SetActive(false);
    }

    public void PerformActionOnClick()
    {
        moveSpeed += playerInfo.acceleration;
        //rb.velocity += new Vector3(0,0,moveSpeed);
        //jumpingPower += moveSpeed;
    }

    public void PlayerJump()
    {
        if (isOnStartLine)
        {
            rb.AddForce(0f, (playerInfo.jumpingPower * 1.2f) * (barContoller.angleValue), (playerInfo.jumpingPower * 1.2f) * (100 - barContoller.angleValue));
            jumpButton.gameObject.SetActive(false);
            accelerationButton.gameObject.SetActive(false);
            UIManager.instance.ShowPerfectText();
        }
        else
        {
            rb.AddForce(0f, playerInfo.jumpingPower * (barContoller.angleValue), playerInfo.jumpingPower * (100 - barContoller.angleValue));
            jumpButton.gameObject.SetActive(false);
            accelerationButton.gameObject.SetActive(false);
        }
        joystick.gameObject.SetActive(true);
        spawner.ObjectActive();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartLine"))
        {
            isOnStartLine = true;
        }
        if (other.CompareTag("CamZone"))
        {
            GameManager.instance.InCamZone();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StartLine"))
        {
            isOnStartLine = false;
        }
        if (other.CompareTag("CamZone"))
        {
            GameManager.instance.OutCamZone();
        }
    }

    public void SpeedItem()
    {
        moveSpeed *= 2f;
    }

    public void HitMeteor()
    {
        moveSpeed *= 0.5f;
    }
}
