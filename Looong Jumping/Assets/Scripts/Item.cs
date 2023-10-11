using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class Item : MonoBehaviour, IPoolObject
{
    public string idName;
    private Transform playerPos;

    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        if (transform.position.z - playerPos.position.z < 0)
        {
            ObjectSpawner.instance.ReturnPool(this);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("SpeedItem");
            ObjectSpawner.instance.ReturnPool(this);
        }
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
