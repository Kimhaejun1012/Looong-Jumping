using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInfo
{
    public float acceleration;
    public float jumpingPower;
    public int money;
}

public class PlayerMove : MonoBehaviour
{
    public float acceleration = 0.3f;
    public float flingTime = 1000f;
    public float groundCheckDistance = 0.5f;

    private float moveSpeed;
    private bool isOnStartLine;
    
    public FloatingJoystick joystick;
    public LayerMask groundLayer;
    public Button jumpButton;
    public Button accelerationButton;
    public ObjectSpawner spawner;

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

            //spawner.transform.position = transform.position + offset.magnitude * moveDirection;
            //spawner.transform.LookAt(transform);

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
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer))
        {
            // ¶¥¿¡ ´ê¾ÒÀ» ¶§ÀÇ µ¿ÀÛ
            //rb.velocity = Vector3.zero; // ¿òÁ÷ÀÓ ¸ØÃã
            moveSpeed = 0;
            GameManager.instance.Landing();

            joystick.gameObject.SetActive(false);
            Debug.Log("ÂøÁö ÂøÁö ÂøÁö");
        }
    }

    public void PerformActionOnClick()
    {
        moveSpeed += acceleration;
        flingTime += moveSpeed;
    }

    public void PlayerJump()
    {
        if(isOnStartLine)
        {
            rb.AddForce(0f, flingTime + moveSpeed, 0f);
            //rb.rotation = Quaternion.Euler(-45f, 0f, 0f);
            jumpButton.gameObject.SetActive(false);
            accelerationButton.gameObject.SetActive(false);
            UIManager.instance.ShowPerfectText();
        }
        else
        {
            rb.AddForce(0f, flingTime, 0f);
            //rb.rotation = Quaternion.Euler(-45f, 0f, 0f);
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
        if(other.CompareTag("CamZone"))
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
        Debug.Log("Item È¹µæ");
    }

    public void HitMeteor()
    {
        moveSpeed *= 0.5f;
        Debug.Log($"Move Speed {moveSpeed}");
    }
}
