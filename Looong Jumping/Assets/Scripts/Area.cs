using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Transform playerPos;
    public AreaSpawner spawner;
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
            spawner.areaIndex++;
        }
    }

    private void Reposition()
    {
        var offset = new Vector3(0, 0, zDistance * spawner.areaIndex);
        transform.position = offset;
    }
    // Update is called once per frame

}
