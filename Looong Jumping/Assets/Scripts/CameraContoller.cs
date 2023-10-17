using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraContoller : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public PlayerContoller playerContoller;
    public Vector3 relativePosition;
    public float flowCamTime = 0.5f;
    public bool portal;

    private void Start()
    {
        // 하위 객체의 초기 위치를 상위 객체에 상대적으로 계산
        offset = transform.localPosition;
    }


    void Update()
    {
        if (portal)
        {
            transform.position = relativePosition;
        }
        else
        {
            //transform.localPosition = Mathf.
            transform.localPosition = offset;
        }

        if(portal)
        {
            flowCamTime -= Time.deltaTime;
        }



        if(flowCamTime < 0)
        {
            portal = false;
            flowCamTime = 2f;
        }
    }

    public void PortalOn()
    {
        relativePosition = GameObject.FindGameObjectWithTag("CC").transform.position;
        flowCamTime = 2f;
        portal = true;
    }
}
