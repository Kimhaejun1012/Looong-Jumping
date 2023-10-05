using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo
{
    public float acceleration;
    public float jumpingPower;
    public int money;
}

public class PlayerMove : MonoBehaviour
{
    private float acceleration = 0.1f;
    private float moveSpeed;
    private float flingTime = 500f;
    private bool isOnStartLine;
    public float groundCheckDistance = 1f;


    public FloatingJoystick joystick;
    public LayerMask groundLayer;
    public BoxCollider startLineCollider;
    public Button jumpButton;
    public Button accelerationButton;

    public CameraContoller camera;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystick.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        var position = rb.position;
        position.x += x;
        position.y += y;

        position += transform.forward * moveSpeed;
        rb.MovePosition(position);
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer))
        {
            // ���� ����� ���� ����
            //rb.velocity = Vector3.zero; // ������ ����
            moveSpeed = 0;
            GameManager.instance.Landing();
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
            print("��߼� ����");
        }
        else
        {
            rb.AddForce(0f, flingTime, 0f);
            jumpButton.gameObject.SetActive(false);
            accelerationButton.gameObject.SetActive(false);
            print("��߼� �� �� �� ����");
        }
        GameManager.instance.isLanding = true;
        //camera.SetJumpingCam();
        joystick.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ StartLine �ݶ��̴��� ������ ���
        if (other.CompareTag("StartLine"))
        {
            isOnStartLine = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ StartLine �ݶ��̴����� ���� ���
        if (other.CompareTag("StartLine"))
        {
            isOnStartLine = false;
        }
    }

}
