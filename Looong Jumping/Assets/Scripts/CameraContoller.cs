using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using static Cinemachine.CinemachineTargetGroup;
using static UnityEngine.GraphicsBuffer;

public class CameraContoller : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public PlayerContoller playerContoller;

    private Vector3 relativePosition;

    public float flowCamTime;

    public float moveSpeed = 2f;
    private float startTime;
    private float journeyLength;

    private float camSpeed;

    public bool portal;


    private void Start()
    {
        // 하위 객체의 초기 위치를 상위 객체에 상대적으로 계산
        offset = transform.localPosition;
    }


    void Update()
    {

        float distanceCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        if (portal)
        {
            transform.position = relativePosition;
        }
        else if(!portal && fractionOfJourney < 1f)
        {
            camSpeed += Time.deltaTime * 3f;
            //transform.localPosition = offset;
            transform.position = Vector3.Lerp(relativePosition, GameObject.FindGameObjectWithTag("CC").transform.position, fractionOfJourney + camSpeed);
        }
        else
        {
            transform.localPosition = offset;
        }

        if (portal)
        {
            flowCamTime -= Time.deltaTime;
        }

        if (flowCamTime < 0)
        {
            portal = false;
        }
    }

    public void PortalOn()
    {
        relativePosition = GameObject.FindGameObjectWithTag("CC").transform.position;
        flowCamTime = 1.2f;
        camSpeed = 0f;
        startTime = Time.time;
        journeyLength = Vector3.Distance(relativePosition, player.position);
        portal = true;
    }
}
