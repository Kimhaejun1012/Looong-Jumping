using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool isJumping = false;


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
        Debug.Log($"화면이 클릭되었습니다! MoveSpeed : {moveSpeed}");
    }

    public void PlayerJump()
    {
        rb.AddForce(0f, flingTime, 0f);
        Debug.Log("Jump Button Click!");

        //여기서 점프버튼이랑 배경버튼 비활성화 해야됨 그리고
    }

}
