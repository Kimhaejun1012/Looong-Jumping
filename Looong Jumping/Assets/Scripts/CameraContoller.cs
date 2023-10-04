using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position + player.transform.position;

    }

    void Update()
    {
        transform.position = offset + player.transform.position; ;
    }
}
