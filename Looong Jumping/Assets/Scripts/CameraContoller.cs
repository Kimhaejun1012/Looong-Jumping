using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraContoller : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    //private void Awake()
    //{
    //    offset = transform.position - player.position;
    //}

    //void Update()
    //{
    //    transform.position = offset + player.transform.position;

    //    // �÷��̾��� ȸ���� ī�޶��� �ʱ� ȸ���� ����
    //}

    public void SetJumpingCam()
    {
        transform.localPosition = new Vector3(5, 0.3f, 0);
        transform.localRotation = new Quaternion(0, -50f, 0, 0);
    }
}
