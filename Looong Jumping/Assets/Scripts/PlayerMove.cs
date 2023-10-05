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

    public BoxCollider startLineCollider;
    public Button jumpButton;
    public Button accelerationButton;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var position = rb.position;

        position += transform.forward * moveSpeed;
        rb.MovePosition(position);
    }

    private void Update()
    {
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
            print("출발선 맞춤");
        }
        else
        {
            rb.AddForce(0f, flingTime, 0f);
            jumpButton.gameObject.SetActive(false);
            accelerationButton.gameObject.SetActive(false);
            print("출발선 못 못 못 맞춤");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 StartLine 콜라이더에 진입한 경우
        if (other.CompareTag("StartLine"))
        {
            isOnStartLine = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 StartLine 콜라이더에서 나온 경우
        if (other.CompareTag("StartLine"))
        {
            isOnStartLine = false;
        }
    }
}
