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
    public float acceleration = 0.1f;
    private float moveSpeed;
    public float flingTime = 1000f;
    private bool isOnStartLine;
    public float groundCheckDistance = 0.5f;

    public FloatingJoystick joystick;
    public LayerMask groundLayer;
    public BoxCollider startLineCollider;
    public Button jumpButton;
    public Button accelerationButton;

    public CameraContoller camera;
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
        if(!GameManager.instance.isLanding)
        {
            float x = joystick.Horizontal;
            float y = joystick.Vertical;

            rotateX += x;
            rotateY += y;

            // 이동 방향을 플레이어의 전방 방향으로 설정
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
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer))
        {
            // 땅에 닿았을 때의 동작
            //rb.velocity = Vector3.zero; // 움직임 멈춤
            moveSpeed = 0;
            GameManager.instance.Landing();
            joystick.gameObject.SetActive(false);
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
            jumpButton.gameObject.SetActive(false);
            accelerationButton.gameObject.SetActive(false);
            UIManager.instance.ShowPerfectText();
        }
        else
        {
            rb.AddForce(0f, flingTime, 0f);
            jumpButton.gameObject.SetActive(false);
            accelerationButton.gameObject.SetActive(false);
        }
        joystick.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartLine"))
        {
            isOnStartLine = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StartLine"))
        {
            isOnStartLine = false;
        }
    }

    public void SpeedItem()
    {
        moveSpeed *= 2f;
        Debug.Log("Item 획득");
    }

    public void HitMeteor()
    {
        moveSpeed = 0.5f;
        Debug.Log($"Move Speed {moveSpeed}");
    }
}
