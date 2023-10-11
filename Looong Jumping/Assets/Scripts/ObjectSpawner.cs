using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Redcode.Pools;
using UnityEditor.EditorTools;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<ObjectSpawner>();
            }
            return m_instance;
        }
    }
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

    }
    private static ObjectSpawner m_instance;

    public Transform target;
    public GameObject item;
    public GameObject[] meteors;

    PoolManager poolManager;

    public Transform spawnZone;

    private Camera cam;
    public float meteorSpawnMax = 0.1f;
    private float meteorTimeBetSpawn;

    public float itemSpawnMax = 0.2f;
    public float itemSpawnMin = 0.1f;
    private float itemTimeBetSpawn;

    private int spawnCount = 10;

    public Vector3 offset;

    void Start()
    {
        meteorTimeBetSpawn = Random.Range(0, meteorSpawnMax);
        itemTimeBetSpawn = Random.Range(itemSpawnMin, itemSpawnMax);
        poolManager = GetComponent<PoolManager>();
        cam = Camera.main;
        offset = transform.position - target.transform.position;

    }

    private void FixedUpdate()
    {
        transform.position = target.transform.position + offset.magnitude * cam.transform.forward;
        transform.LookAt(target);
    }


    public void ObjectActive()
    {
        StartCoroutine(SpawnMeteor());
        //StartCoroutine(SpawnItem());
    }

    IEnumerator SpawnMeteor()
    {
        while(!GameManager.instance.isLanding)
        {
            yield return new WaitForSeconds(meteorTimeBetSpawn);

            for(int i = 0; i < spawnCount; i++)
            {
                //Instantiate(meteors[Random.Range(0,4)], spawnPosition, Quaternion.identity);
                int ran = Random.Range(1, 4);

                Meteor newMeteor = poolManager.GetFromPool<Meteor>(ran);
                newMeteor.transform.position = SpawnPosition();
                meteorTimeBetSpawn = Random.Range(0, meteorSpawnMax);
            }
        }
    }

    public Vector3 SpawnPosition()
    {
        float randomX = Random.Range(spawnZone.position.x - spawnZone.localScale.x / 2f, spawnZone.position.x + spawnZone.localScale.x / 2f);
        float randomY = Random.Range(spawnZone.position.y - spawnZone.localScale.y / 2f, spawnZone.position.y + spawnZone.localScale.y / 2f);
        float randomZ = Random.Range(spawnZone.position.z - spawnZone.localScale.z / 2f, spawnZone.position.z + spawnZone.localScale.z / 2f);

        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

        return spawnPosition;
    }

    IEnumerator SpawnItem()
    {
        while(!GameManager.instance.isLanding)
        {
            yield return new WaitForSeconds(itemTimeBetSpawn);
            for (int i = 0; i < 10; i++)
            {
                float randomX = Random.Range(spawnZone.position.x - spawnZone.localScale.x / 2f, spawnZone.position.x + spawnZone.localScale.x / 2f);
                float randomY = Random.Range(spawnZone.position.y - spawnZone.localScale.y / 2f, spawnZone.position.y + spawnZone.localScale.y / 2f);
                float randomZ = Random.Range(spawnZone.position.z - spawnZone.localScale.z / 2f, spawnZone.position.z + spawnZone.localScale.z / 2f);

                Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

                // 오브젝트 생성
                Instantiate(item, spawnPosition, Quaternion.identity);

                itemTimeBetSpawn = Random.Range(itemSpawnMin, itemSpawnMax);
            }
        }
    }
    public void ReturnPool(Meteor clone)
    {
        poolManager.TakeToPool<Meteor>(clone.idName, clone);
    }
}
