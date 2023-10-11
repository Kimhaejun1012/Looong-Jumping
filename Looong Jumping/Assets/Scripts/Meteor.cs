using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Meteor : MonoBehaviour, IPoolObject
{
    public string idName;
    private Transform playerPos;
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.z - playerPos.position.z < 0)
        {
            ObjectSpawner.instance.ReturnPool(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.SendMessage("HitMeteor");
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
