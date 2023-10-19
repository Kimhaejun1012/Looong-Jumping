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

    private LineRenderer line; // 총알 궤적을 그리기 위한 렌더러

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
        jumpButton.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
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

            Vector3 moveDirection = rb.transform.forward;

            currentPosition = rb.transform.position;

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

        if (!GameManager.instance.isLanding && Physics.Raycast(currentPosition, newPosition - currentPosition, out RaycastHit hit, moveVector.magnitude, groundLayer))
        {
            //2차로 벽 뚫었을 경우를 대비하여 Raycast로 MovePosition()하기 전 포지션과 MovePosition을 할 포지션
            Debug.Log("벽 뚫어서 되돌아옴");
            rb.MovePosition(hit.point);/* = previousPosition*/
            rb.isKinematic = true;
            playerAnimator.SetBool("Jumping", false);
            isMove = false;
            GameManager.instance.Landing();
        }
        else
        {
            isMove = true;
        }


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
        accelCount++;
        playerAnimator.SetInteger("AccelCount", accelCount);
        playerAnimator.SetBool("Run", true);
        tap.gameObject.SetActive(false);
        ObjectSpawner.instance.IncreaseSpawnerZ(GameManager.instance.saveData.playerData.acceleration);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SoundManager.instance.SoundPlay("Gameover");
            rb.isKinematic = true;
            moveSpeed = 0;
            joystick.gameObject.SetActive(false);
            playerAnimator.SetBool("Jumping", false);
            GameManager.instance.Landing();
        }
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
        else if(other.CompareTag("RocketParts1"))
        {
            //GameManager.instance.saveData.shopData.rocketPartsCount++;
            if(GameManager.instance.saveData.gameData.rocketParts1 > 1)
            {
                GameManager.instance.saveData.gameData.rocketParts1++;
            }
            else if (GameManager.instance.saveData.gameData.rocketParts2 > 1)
            {
                GameManager.instance.saveData.gameData.rocketParts2++;
            }
            else if (GameManager.instance.saveData.gameData.rocketParts3 > 1)
            {
                GameManager.instance.saveData.gameData.rocketParts3++;
            }
            positiveEffect.Play();
            SoundManager.instance.SoundPlay("GetSpecialparts");
            //SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
        else if (other.CompareTag("RocketParts2"))
        {
            //GameManager.instance.saveData.shopData.rocketPartsCount++;
            if (GameManager.instance.saveData.gameData.rocketParts1 > 2)
            {
                GameManager.instance.saveData.gameData.rocketParts1++;
            }
            else if (GameManager.instance.saveData.gameData.rocketParts2 > 2)
            {
                GameManager.instance.saveData.gameData.rocketParts2++;
            }
            else if(GameManager.instance.saveData.gameData.rocketParts3 > 1)
            {
                GameManager.instance.saveData.gameData.rocketParts3++;
            }
            positiveEffect.Play();
            SoundManager.instance.SoundPlay("GetSpecialparts");
            //SaveLoadSystem.AutoSave(GameManager.instance.saveData);
        }
        else if (other.CompareTag("PortalParts1"))
        {
            //GameManager.instance.saveData.shopData.rocketPartsCount++;
            if (GameManager.instance.saveData.gameData.portalParts1 == 0)
            {
                GameManager.instance.saveData.gameData.portalParts1++;
            }
            else if (GameManager.instance.saveData.gameData.portalParts2 == 0)
            {
                GameManager.instance.saveData.gameData.portalParts2++;
            }
            else if(GameManager.instance.saveData.gameData.portalParts3 == 0)
            {
                GameManager.instance.saveData.gameData.portalParts3++;
            }
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
            positiveEffect.Play();
            SoundManager.instance.SoundPlay("GetSpecialparts");
        }
        else if (other.CompareTag("PortalParts2"))
        {
            //GameManager.instance.saveData.shopData.rocketPartsCount++;
            if (GameManager.instance.saveData.gameData.portalParts1 == 1)
            {
                GameManager.instance.saveData.gameData.portalParts1++;
            }
            else if (GameManager.instance.saveData.gameData.portalParts2 == 1)
            {
                GameManager.instance.saveData.gameData.portalParts2++;
            }
            else if (GameManager.instance.saveData.gameData.portalParts3 == 1)
            {
                GameManager.instance.saveData.gameData.portalParts3++;
            }
            SaveLoadSystem.AutoSave(GameManager.instance.saveData);
            positiveEffect.Play();
            SoundManager.instance.SoundPlay("GetSpecialparts");
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
        ObjectSpawner.instance.IncreaseSpawnerZ(GameManager.instance.saveData.playerData.speedIncrease);
        SoundManager.instance.SoundPlay("GetBoost");
    }

    public void HitMeteor()
    {
        hitEffect.Play();
        if (!playerAnimator.GetBool("Rocket"))
        {
            moveSpeed = Math.Clamp(moveSpeed += GameManager.instance.saveData.playerData.speedReduction, 0, moveSpeed);
            ObjectSpawner.instance.IncreaseSpawnerZ(GameManager.instance.saveData.playerData.speedReduction);
            SoundManager.instance.SoundPlay("HitMeteor");
        }
    }
}
