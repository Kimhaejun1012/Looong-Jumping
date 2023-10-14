using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SaveDataVC = SaveDataV1;

public class PlayerContoller : MonoBehaviour
{
    //Playerinfo

    private float groundCheckDistance = 1f;
    public float angleIncrement = 1f;
    public float moveSpeed;
    private bool isOnStartLine;
    private bool isJumpButtonClick;

    public FloatingJoystick joystick;
    public LayerMask groundLayer;
    public Button jumpButton;
    public Button accelerationButton;
    public Image tap;
    
    public ObjectSpawner spawner;
    public BarContoller barContoller;
    public Rigidbody rb;
    public Animator playerAnimator;
    private int accelCount = 0;

    private float rotateX = 0f;
    private float rotateY = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        joystick.gameObject.SetActive(false);
        playerAnimator = GetComponent<Animator>();
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
        if (!GameManager.instance.isLanding && Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer))
        {
            //hasLanded = true;
            // ¶¥¿¡ ´ê¾ÒÀ» ¶§ÀÇ µ¿ÀÛ
            //rb.velocity = Vector3.zero; // ¿òÁ÷ÀÓ ¸ØÃã
            //moveSpeed = 0;
            //playerInfo.money += (int)UIManager.instance.score;

            //joystick.gameObject.SetActive(false);
            playerAnimator.SetBool("Jumping", false);
            GameManager.instance.Landing();
            //SaveLoadSystem.AutoSave();
        }

        if (isJumpButtonClick)
        {
            SetGauge();
        }
    }

    public void JumpButtonDown()
    {
        isJumpButtonClick = true;
        barContoller.gameObject.SetActive(true);
        Time.timeScale = 0f;
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
        barContoller.gameObject.SetActive(false);

        Time.timeScale = 1f;
    }
    public void PerformActionOnClick()
    {
        moveSpeed += GameManager.instance.saveData.playerData.acceleration * 5f;
        accelCount++;
        //rb.velocity += new Vector3(0,0,moveSpeed);
        //jumpingPower += moveSpeed;
        playerAnimator.SetInteger("AccelCount", accelCount);
        playerAnimator.SetBool("Run", true);
        tap.gameObject.SetActive(false);
        //if (accelCount < 3)
        //{
        // playerAnimator.SetTrigger("Slow Run");
        //}
        //else
        //{
        //    playerAnimator.SetTrigger("Fast Run");
        //}
    }

    public void PlayerJump()
    {
        if (isOnStartLine)
        {
            rb.AddForce(0f, (GameManager.instance.saveData.playerData.jumpingPower * 1.2f) * (barContoller.angleValue), (GameManager.instance.saveData.playerData.jumpingPower * 1.2f) * (100 - barContoller.angleValue));
            jumpButton.gameObject.SetActive(false);
            accelerationButton.gameObject.SetActive(false);
            UIManager.instance.ShowPerfectText();
        }
        else
        {
            rb.AddForce(0f, GameManager.instance.saveData.playerData.jumpingPower * (barContoller.angleValue), GameManager.instance.saveData.playerData.jumpingPower * (100 - barContoller.angleValue));
            jumpButton.gameObject.SetActive(false);
            accelerationButton.gameObject.SetActive(false);
        }
        joystick.gameObject.SetActive(true);
        spawner.ObjectActive();

        ActiveManager.instance.ActivateCurrentItemButton();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartLine"))
        {
            isOnStartLine = true;
        }
        else if (other.CompareTag("CamZone"))
        {
            GameManager.instance.InCamZone();
        }
        else if (other.CompareTag("Portal"))
        {
            Vector3 newPosition = transform.position;
            newPosition.z += 100f;
            newPosition.y += 100f;
            transform.position = newPosition;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StartLine"))
        {
            isOnStartLine = false;
        }
        else if (other.CompareTag("CamZone"))
        {
            GameManager.instance.OutCamZone();
        }
        else if(other.CompareTag("Floor"))
        {
            jumpButton.gameObject.SetActive(false);
            accelerationButton.gameObject.SetActive(false);
            playerAnimator.SetBool("Jumping", true);
            playerAnimator.SetBool("Run", false);
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
