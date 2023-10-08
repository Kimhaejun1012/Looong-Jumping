using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Transform playerPos;
    private float zDistance = 60f;
    private float yPosition = -20f;


    void Update()
    {
        if (playerPos.transform.position.z > transform.position.z + 50f)
        {
            Reposition();
            GameManager.instance.areaIndex++;
        }
    }

    private void Reposition()
    {
        var offset = new Vector3(0, yPosition, zDistance * GameManager.instance.areaIndex);
        transform.position = offset;
    }

}
