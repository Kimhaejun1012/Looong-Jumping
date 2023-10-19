using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class Parts : MonoBehaviour, IPoolObject
{
    public string idName;
    private Transform playerPos;

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
    }

    public void OnGettingFromPool()
    {
        transform.position = ObjectSpawner.instance.SpawnPosition();
    }
}
