using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Transform playerPos;
    private float zDistance = 20f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (playerPos.transform.position.z > transform.position.z + 10f)
        {
            Reposition();
            GameManager.instance.areaIndex++;
        }
    }

    private void Reposition()
    {
        var offset = new Vector3(0, 0, zDistance * GameManager.instance.areaIndex);
        transform.position = offset;
    }

}
