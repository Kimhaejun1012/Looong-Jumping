using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraContoller : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    private void Awake()
    {
        offset = transform.position - player.position;
    }

    void Update()
    {
        transform.position = offset + player.transform.position;
        
        transform.rotation = Quaternion.Euler(player.transform.forward);
        // 플레이어의 회전을 카메라의 초기 회전에 누적
    }
}
