using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class Parts : MonoBehaviour, IPoolObject
{
    public string idName;
    private Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        if (transform.position.z - playerPos.position.z < -10)
        {
            ObjectSpawner.instance.ReturnPool(this);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
    }

    public void OnCreatedInPool()
    {
        //오브젝트 처음 실행됐을 떄
    }

    public void OnGettingFromPool()
    {
        transform.position = ObjectSpawner.instance.SpawnPosition();
    }
}
