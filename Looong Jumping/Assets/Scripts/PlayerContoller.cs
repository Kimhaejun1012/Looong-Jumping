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
    public float angleIncrement = 1f;
    public float moveSpeed;
    public float moveSpeedOffset;
    public bool isPortal;
    public bool isMove;

    private float groundCheckDistance = 0.2f;
    private float raycastOffset = 0.1f;

    private bool isOnStartLine;
    private bool isJumpButtonClick;
    private bool isJump;

    private float currentGravity;
    private float maxGravity = 0.5f;
    private float addGravity = 0.005f;
    public bool portalTime;

    private LineRenderer line; // 恥硝 泳旋聖 益軒奄 是廃 兄希君

    public FloatingJoystick joystick;
    public LayerMask groundLayer;
    public Button jumpButton;
    public Button accelerationButton;
    public Image tap;
    Vector2 direction;

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
        // Debug.DrawRay(transform.position, transform.forward, Color.green);

        if (!GameManager.instance.isLanding)
        {
            float x = joystick.Horizontal;
            float y = joystick.Vertical;
            direction = new Vector2(x,y);
            rotateX += x;
            rotateY += y;

            var directionMag = direction.magnitude;

            if (directionMag > 1)
            {
                directionMag = 1f;
            }

            if(moveSpeed >= 0f)
            {
                moveSpeed -= Math.Abs(directionMag * GameManager.instance.saveData.playerData.airResist);
            }
            else
            {
                moveSpeed = 0f;
            }
            //moveSpeed -= Math.Abs(x * 0.1f);
            //moveSpeed -= Math.Abs(y * 0.1f);

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
                rb.MovePosition(rb.transform.position + Vector3.forward * 0.05f);
            }
            else
            {
                RaycastHit hit;
                newPosition = currentPosition + moveDirection * moveSpeed * Time.deltaTime;
                moveVector = moveDirection * moveSpeed * Time.deltaTime;
                Move(isMove);

                //if (Physics.Raycast(currentPosition, newPosition - currentPosition, out hit, moveVector.magnitude, groundLayer))
                //{
                //    //2託稽 混 欣醸聖 井酔研 企搾馬食 Raycast稽 MovePosition()馬奄 穿 匂走芝引 MovePosition聖 拝 匂走芝
                //    Debug.Log("混 欣嬢辞 鞠宜焼身");
                //    rb.MovePosition(hit.point);/* = previousPosition*/
                //    rb.isKinematic = true;
                //    playerAnimator.SetBool("Jumping", false);
                //    GameManager.instance.Landing();
                //}
                //else
                //{
                //    // 中宜馬走 省紹聖 井酔, 戚疑 是帖稽 戚疑馬壱 戚穿 是帖 穣汽戚闘
                //    rb.MovePosition(newPosition);
                //    Debug.DrawRay(transform.position, transform.forward, Color.green);

                //    //transform.position = newPosition;
                //    //previousPosition = currentPosition;
                //}
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

        }


        //Debug.DrawRay(rb.transform.position, rb.transform.forward, Color.green);
        if (!GameManager.instance.isLanding && Physics.Raycast(currentPosition, newPosition - currentPosition, out RaycastHit hit, moveVector.magnitude, groundLayer))
        {
            //2託稽 混 欣醸聖 井酔研 企搾馬食 Raycast稽 MovePosition()馬奄 穿 匂走芝引 MovePosition聖 拝 匂走芝
            Debug.Log("混 欣嬢辞 鞠宜焼身");
            rb.MovePosition(hit.point);/* = previousPosition*/
            rb.isKinematic = true;
            playerAnimator.SetBool("Jumping", false);
            isMove = false;
            GameManager.instance.Landing();
        }
        else
        {
            isMove = true;
            //transform.position = newPosition;
            // 中宜馬走 省紹聖 井酔, 戚疑 是帖稽 戚疑馬壱 戚穿 是帖 穣汽戚闘
            //rb.MovePosition(newPosition);
            //Debug.DrawRay(transform.position, transform.forward, Color.green);

            //transform.position = newPosition;
            //previousPosition = currentPosition;
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
    
    public void Move(bool move)
    {
        if(move)
        rb.MovePosition(newPosition);
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
        SoundManager.instance.SoundPlay("Footstep");
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
        ObjectSpawner.instance.IncreaseSpawnerZ(GameManager.instance.saveData.playerData.acceleration);
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
        //巴傾戚嬢亜 rigidbody.MovePosition()生稽 崇送戚澗汽
        //戚惟 混聖 亜懐 欣澗 獄益亜 赤柔艦陥
        //析舘 混聖 照欣醸聖凶研 亜舛馬食 焼掘稽 中宜端滴研 馬壱
        if (collision.gameObject.CompareTag("Ground"))
        {
            SoundManager.instance.SoundPlay("Gameover");
            rb.isKinematic = true;
            //hasLanded = true;
            //競拭 願紹聖 凶税 疑拙
            //rb.velocity = Vector3.zero; // 崇送績 誇茶
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
        //    Debug.Log("競願紹眼格つだし傾だ胡君つだ");
        //    rb.isKinematic = true;
        //    hasLanded = true;
        //    競拭 願紹聖 凶税 疑拙
        //    rb.velocity = Vector3.zero; // 崇送績 誇茶
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
        //    Debug.Log("競願紹眼格つだし傾だ胡君つだ");
        //    rb.isKinematic = true;
        //    //hasLanded = true;
        //    //競拭 願紹聖 凶税 疑拙
        //    //rb.velocity = Vector3.zero; // 崇送績 誇茶
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
        //    // 中宜戚 姶走吉 井酔, 戚穿 是帖稽 宜焼亜奄
        //    rb.MovePosition(hit.point);/* = previousPosition*/
        //    rb.isKinematic = true;
        //    playerAnimator.SetBool("Jumping", false);
        //    GameManager.instance.Landing();
        //    Debug.Log("中宜端滴推");
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
        spawner.SetSpawn(transform.position.z);
        GameManager.instance.isJumping = true;
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
            rb.isKinematic = false;
            moveSpeed *= GameManager.instance.saveData.shopData.portalIncreaseSpeed;
            isPortal = false;
            ObjectSpawner.instance.IncreaseSpawnerZ(moveSpeed / GameManager.instance.saveData.shopData.portalIncreaseSpeed);
            SoundManager.instance.SoundPlay("Wormhole");
        }
        else if(other.CompareTag("Silver"))
        {
            GameManager.instance.saveData.playerData.gold += 500;
            UIManager.instance.playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";
            Destroy(other.gameObject);
            SoundManager.instance.SoundPlay("GetCoinClip");
        }
        else if (other.CompareTag("Copper"))
        {
            GameManager.instance.saveData.playerData.gold += 100;
            UIManager.instance.playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";
            Destroy(other.gameObject); SoundManager.instance.SoundPlay("GetCoinClip");
        }
        else if (other.CompareTag("Gold"))
        {
            GameManager.instance.saveData.playerData.gold += 1000;
            UIManager.instance.playerMoney.text = $"Gold : {GameManager.instance.saveData.playerData.gold}";
            Destroy(other.gameObject); SoundManager.instance.SoundPlay("GetCoinClip");
        }
        else if(other.CompareTag("RocketParts"))
        {
            //GameManager.instance.saveData.shopData.rocketPartsCount++;
            if(GameManager.instance.saveData.gameData.rocketParts1 == 2)
            {
                GameManager.instance.saveData.gameData.rocketParts2++;
            }
            else if (GameManager.instance.saveData.gameData.rocketParts2 == 2)
            {
                GameManager.instance.saveData.gameData.rocketParts3++;
            }
            else
            {
                GameManager.instance.saveData.gameData.rocketParts1++;
            }
            positiveEffect.Play();
            SoundManager.instance.SoundPlay("GetSpecialparts");
            //SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
        else if (other.CompareTag("PortalParts"))
        {
            //GameManager.instance.saveData.shopData.rocketPartsCount++;
            if (GameManager.instance.saveData.gameData.portalParts1 == 2)
            {
                GameManager.instance.saveData.gameData.portalParts2++;
            }
            else if (GameManager.instance.saveData.gameData.portalParts2 == 2)
            {
                GameManager.instance.saveData.gameData.portalParts3++;
            }
            else
            {
                GameManager.instance.saveData.gameData.portalParts1++;
            }
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
            positiveEffect.Play();
            SoundManager.instance.SoundPlay("GetSpecialparts");
        }
        //else if (other.CompareTag("CybogParts"))
        //{

        //}
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
        ObjectSpawner.instance.IncreaseSpawnerZ(GameManager.instance.saveData.playerData.speedIncrease);
        SoundManager.instance.SoundPlay("GetBoost");
    }

    public void HitMeteor()
    {
        hitEffect.Play();
        if (!playerAnimator.GetBool("Rocket"))
        {
            //moveSpeed += GameManager.instance.saveData.playerData.speedReduction;
            //Debug.Log("域至吉 葵 : " + moveSpeed + GameManager.instance.saveData.playerData.speedReduction);
            moveSpeed = Math.Clamp(moveSpeed += GameManager.instance.saveData.playerData.speedReduction, 0, moveSpeed);
            ObjectSpawner.instance.IncreaseSpawnerZ(GameManager.instance.saveData.playerData.speedReduction);
            SoundManager.instance.SoundPlay("HitMeteor");
        }
    }
}
