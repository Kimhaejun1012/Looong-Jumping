using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;
using UnityEditor.EditorTools;

public class FlyingObject : MonoBehaviour, IPoolObject
{
    public string idName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnCreatedInPool()
    {
        //������Ʈ ó�� ������� ��
    }

    public void OnGettingFromPool()
    {
    }
}

