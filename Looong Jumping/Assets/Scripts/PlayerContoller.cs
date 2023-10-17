using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SaveDataVC = SaveDataV1;

public class PlayerContoller : MonoBehaviour
{
    //Playerinfo

    public float angleIncrement = 1f;
    public float moveSpeed;
    public float moveSpeedOffset;
    public bool isPortal;

    private float groundCheckDistance = 0.2f;
    private float raycastOffset = 0.1f;

    private bool isOnStartLine;
    private bool isJumpButtonClick;
    private bool isJump;

    private float currentGravity;
    private float maxGravity = 0.5f;
    private float addGravity = 0.005f;

    private LineRenderer line; // 총알 궤적을 그리기 위한 렌더러

    public FloatingJoystick joystick;
    public LayerMask groundLayer;
    public Button jumpButton;
    public Button accelerationButton;
    public Image tap;

    private Vector3 previousPosition;
    Vector3 newPosition;
    Vector3 moveVector;
    Vector3 currentPosition;

    public ParticleSystem hitEffect;
    public ParticleSystem positiveEffect;

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
        line = GetComponent<LineRenderer>();
        joystick.gameObject.SetActive(false);
        playerAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        //previousPosition = rb.transform.position;
        jumpButton.gameObject.SetActive(false);
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

            currentPosition = rb.transform.position;

            //if (isJump)
            //{
            //    currentGravity += addGravity;
            //    if (maxGravity <= currentGravity)
            //    {
            //        currentGravity = maxGravity;
            //    }
            //    Vector3 offset = rb.transform.position;
            //    offset.y -= currentGravity;
            //    rb.transform.position = offset;
            //}

            if (isPortal == true)
            {
                rb.isKinematic = true;
                rb.MovePosition(rb.transform.position + Vector3.down * 0.05f);
            }
            else
            {
                RaycastHit hit;
                newPosition = currentPosition + moveDirection * moveSpeed * Time.deltaTime;
                moveVector = moveDirection * moveSpeed * Time.deltaTime;
                if (Physics.Raycast(currentPosition, moveVector, out hit, moveVector.magnitude, groundLayer))
                {
                    // 충돌이 감지된 경우, 이전 위치로 돌아가기
                    Debug.Log("벽 뚫어서 되돌아옴");
                    rb.MovePosition(hit.point);/* = previousPosition*/
                    rb.isKinematic = true;
                    playerAnimator.SetBool("Jumping", false);
                    GameManager.instance.Landing();
                }
                //if (Physics.Raycast(currentPosition, currentPosition - newPosition,out hit, Vector3.Distance(newPosition, currentPosition), groundLayer))
                //{
                //    // 충돌이 감지된 경우, 이전 위치로 돌아가기
                //    Debug.Log("벽 뚫어서 되돌아옴");
                //    rb.MovePosition(hit.point);/* = previousPosition*/
                //    rb.isKinematic = true;
                //    playerAnimator.SetBool("Jumping", false);
                //    GameManager.instance.Landing();
                //}
                else
                {
                    // 충돌하지 않았을 경우, 이동 위치로 이동하고 이전 위치 업데이트
                    rb.MovePosition(newPosition);

                    //transform.position = newPosition;
                    //previousPosition = currentPosition;
                }
            }
            rb.MoveRotation(Quaternion.Euler(-rotateY, rotateX, 0));

        }
        else
        {
            rb.rotation = Quaternion.identity;
        }
    }

    private void Update()
    {
        CheckLanding();

        if (Input.GetKeyDown(KeyCode.R))
        {
            moveSpeed += moveSpeed * 2f;
        }
        else if(Input.GetKeyDown(KeyCode.T))
        {
            moveSpeed -= moveSpeed * 0.7f;
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.instance.saveData.shopData.rocketParts[GameManager.instance.saveData.shopData.rocketPartsCount] = true;
            GameManager.instance.saveData.shopData.rocketPartsCount++;
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
        //Vector3 v3 = transform.position + transform.up * -raycastOffset;
        //line.SetPosition(0, v3);`
        //v3.y += groundCheckDistance;
        //line.SetPosition(1, v3);
        //Debug.DrawLine(transform.position + Vector3.up * raycastOffset, transform.position + Vector3.down * groundCheckDistance, Color.red, groundCheckDistance);

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
        moveSpeed += GameManager.instance.saveData.playerData.acceleration;
        jumpButton.gameObject.SetActive(true);
        //rb.velocity = rb.transform.forward * moveSpeed * Time.deltaTime;
        //rb.AddForce(0,0,moveSpeed * 500);
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("땅닿았담너ㅔㅐㅇ레ㅐ먼러ㅔㅐ");
            rb.isKinematic = true;
            //hasLanded = true;
            //땅에 닿았을 때의 동작
            //rb.velocity = Vector3.zero; // 움직임 멈춤
            moveSpeed = 0;
            //playerInfo.money += (int)UIManager.instance.score;
            //transform.position = hit.point;
            joystick.gameObject.SetActive(false);
            playerAnimator.SetBool("Jumping", false);
            GameManager.instance.Landing();
        }
    }

    public void CheckLanding()
    {
        //if (!GameManager.instance.isLanding && Physics.Raycast(transform.position + transform.up * -raycastOffset, transform.up, out RaycastHit hit, groundCheckDistance, groundLayer))
        //{
        //    Debug.Log("땅닿았담너ㅔㅐㅇ레ㅐ먼러ㅔㅐ");
        //    rb.isKinematic = true;
        //    hasLanded = true;
        //    땅에 닿았을 때의 동작
        //    rb.velocity = Vector3.zero; // 움직임 멈춤
        //    moveSpeed = 0;
        //    playerInfo.money += (int)UIManager.instance.score;
        //    transform.position = hit.point;
        //    joystick.gameObject.SetActive(false);
        //    playerAnimator.SetBool("Jumping", false);
        //    GameManager.instance.Landing();
        //    SaveLoadSystem.AutoSave();
        //}
        //if (!GameManager.instance.isLanding && Physics.Raycast(transform.position + Vector3.down * raycastOffset, Vector3.down, out RaycastHit hit, groundCheckDistance, groundLayer))
        //{
        //    Debug.Log("땅닿았담너ㅔㅐㅇ레ㅐ먼러ㅔㅐ");
        //    rb.isKinematic = true;
        //    //hasLanded = true;
        //    //땅에 닿았을 때의 동작
        //    //rb.velocity = Vector3.zero; // 움직임 멈춤
        //    moveSpeed = 0;
        //    //playerInfo.money += (int)UIManager.instance.score;
        //    transform.position = hit.point;
        //    joystick.gameObject.SetActive(false);
        //    playerAnimator.SetBool("Jumping", false);
        //    GameManager.instance.Landing();
        //    //SaveLoadSystem.AutoSave();
        //}
        //if (!GameManager.instance.isLanding && Physics.Raycast(currentPosition, moveVector, out RaycastHit hit, moveVector.magnitude, groundLayer))
        //{
        //    // 충돌이 감지된 경우, 이전 위치로 돌아가기
        //    rb.MovePosition(hit.point);/* = previousPosition*/
        //    rb.isKinematic = true;
        //    playerAnimator.SetBool("Jumping", false);
        //    GameManager.instance.Landing();
        //    Debug.Log("충돌체크요");
        //}
    }

    public void PlayerJump()
    {
        if (isOnStartLine)
        {
            rb.AddForce(0f, (GameManager.instance.saveData.playerData.jumpingPower * GameManager.instance.saveData.playerData.perfectJumpPowerIncrease) * (barContoller.angleValue), (GameManager.instance.saveData.playerData.jumpingPower * GameManager.instance.saveData.playerData.perfectJumpPowerIncrease) * (100 - barContoller.angleValue));
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

        isJump = true;

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
            newPosition.z += 30f;
            newPosition.y += 10f;
            transform.position = newPosition;
            isPortal = false;
            rb.isKinematic = false;
            moveSpeed *= GameManager.instance.saveData.shopData.portalIncreaseSpeed;
        }
        else if(other.CompareTag("Silver"))
        {

            GameManager.instance.saveData.playerData.gold += 500;
            UIManager.instance.playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Copper"))
        {
            GameManager.instance.saveData.playerData.gold += 100;
            UIManager.instance.playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Gold"))
        {
            GameManager.instance.saveData.playerData.gold += 1000;
            UIManager.instance.playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("RocketParts"))
        {
            GameManager.instance.saveData.shopData.rocketPartsCount++;
            GameManager.instance.saveData.shopData.rocketParts[GameManager.instance.saveData.shopData.rocketPartsCount] = true;
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
            positiveEffect.Play();

        }
        else if (other.CompareTag("PortalParts"))
        {

        }
        else if (other.CompareTag("CybogParts"))
        {

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
        moveSpeed += GameManager.instance.saveData.playerData.speedIncrease;
        positiveEffect.Play();
    }

    public void HitMeteor()
    {
        hitEffect.Play();
        if (!playerAnimator.GetBool("Rocket"))
        {
            //moveSpeed += GameManager.instance.saveData.playerData.speedReduction;
            //Debug.Log("계산된 값 : " + moveSpeed + GameManager.instance.saveData.playerData.speedReduction);
            Debug.Log($"MoveSpeed 감소 전 : {moveSpeed}");
            moveSpeed = Math.Clamp(moveSpeed += GameManager.instance.saveData.playerData.speedReduction, 0, moveSpeed);
            Debug.Log($"MoveSpeed 감소 후 : {moveSpeed}");

        }
    }
}
