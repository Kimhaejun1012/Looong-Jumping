using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Transform playerPos;
    private float zDistance = 980f;
    //private float yPosition = -20f;
    void Update()
    {
        if (playerPos.transform.position.z > transform.position.z + 1470)
        {
            Reposition();
            GameManager.instance.areaIndex++;
        }
        Vector3 offset = transform.position;
        offset.x = playerPos.transform.position.x - 750;
        transform.position = offset;
    }

    private void Reposition()
    {
        var offset = new Vector3(-750, 0,(zDistance * GameManager.instance.areaIndex));
        transform.position = offset;
    }

}
