using System.Collections;
using UnityEngine;
using Redcode.Pools;

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
    public GameObject[] coins;
    public GameObject[] parts;



    public float playerMoveSpeed;

    PoolManager poolManager;

    public Transform spawnZone;
    public PlayerContoller playerContoller;

    //private Camera cam;
    //public float meteorSpawnMax = 0.1f;
    //private float meteorTimeBetSpawn;

    //public float itemSpawnMax = 0.2f;
    //public float itemSpawnMin = 0.1f;
    //private float itemTimeBetSpawn;

    //public float coinSpawnMax = 1f;
    //public float coinSpawnMin = 0.5f;
    //private float coinTimeBetSpawn;

    //private float partsSpawnMax = 5f;
    //private float partsSpawnMin = 0.2f;
    //private float partsTimeBetSpawn;

    private float spawnMeteorLength = 30f;
    private float spawnItemLength = 100f;
    private float spawnPartsLength = 50f;
    private float spawnCoinLength = 45f;

    public float meteorPosz;
    public float itemPosz;
    public float partsPosz;
    public float coinPosz;

    private int spawnCount = 10;

    public Vector3 offset;

    void Start()
    {
        //meteorTimeBetSpawn = Random.Range(0, meteorSpawnMax);
        //itemTimeBetSpawn = Random.Range(itemSpawnMin, itemSpawnMax);
        //partsTimeBetSpawn = Random.Range(partsSpawnMin, partsSpawnMax);
        poolManager = GetComponent<PoolManager>();
        //cam = Camera.main;
        offset = transform.position - target.transform.position;
    }

    private void FixedUpdate()
    {
        //transform.position = target.transform.position + offset.magnitude * cam.transform.forward;
        //transform.position = target.transform.position + offset.magnitude * target.transform.forward;
        transform.position = offset + target.transform.position;
        transform.LookAt(target);
    }

    private void Update()
    {
        if(target.transform.position.z - meteorPosz > spawnMeteorLength && GameManager.instance.isJumping)
        {
            MeteorSpawn();
            meteorPosz = target.transform.position.z;
            Debug.Log("皋抛坷 积己");
        }
        if (target.transform.position.z - itemPosz > spawnItemLength && GameManager.instance.isJumping)
        {
            ItemSpawn();
            itemPosz = target.transform.position.z;
            Debug.Log("酒捞袍 积己");
        }
        if (target.transform.position.z - partsPosz > spawnPartsLength && GameManager.instance.isJumping)
        {
            PartsSpawn();
            partsPosz = target.transform.position.z;
            Debug.Log("颇明 积己");
        }
        if (target.transform.position.z - coinPosz > spawnCoinLength && GameManager.instance.isJumping)
        {
            CoinSpawn();
            coinPosz = target.transform.position.z;
            Debug.Log("内牢 积己");
        }
    }

    public void IncreaseSpawnerZ(float addSpeed)
    {
        //offset.z += playerContoller.moveSpeed;
        offset.z += addSpeed;
    }

    public void MeteorSpawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            //Instantiate(meteors[Random.Range(0,4)], spawnPosition, Quaternion.identity);
            int ran = Random.Range(0, 4);

            Meteor newMeteor = poolManager.GetFromPool<Meteor>(ran);
            newMeteor.transform.position = SpawnPosition();
        }
    }
    public void ItemSpawn()
    {
        for (int i = 0; i < 10; i++)
        {
            Item newItem = poolManager.GetFromPool<Item>(4);
            newItem.transform.position = SpawnPosition();
        }
    }

    public void PartsSpawn()
    {
        for (int i = 0; i < 10; i++)
        {
            int ran = Random.Range(5, 7);
            Debug.Log(ran);
            Parts newMeteor = poolManager.GetFromPool<Parts>(ran);
            newMeteor.transform.position = SpawnPosition();
        }
    }

    public void CoinSpawn()
    {
            int ran = Random.Range(7,10);
            Coin newCoin = poolManager.GetFromPool<Coin>(ran);
            newCoin.transform.position = SpawnPosition();
    }

    public void SetSpawn(float targetZ)
    {
        meteorPosz = targetZ;
        itemPosz = targetZ;
        partsPosz = targetZ;
        coinPosz = targetZ;
    }


    public void ObjectActive()
    {
        //StartCoroutine(SpawnMeteor());
        //StartCoroutine(SpawnItem());
        //StartCoroutine(SpawnCoin());
        //StartCoroutine(SpawnParts());
    }
    //IEnumerator SpawnParts()
    //{
    //    while (!GameManager.instance.isLanding)
    //    {
    //        yield return new WaitForSeconds(partsTimeBetSpawn);

    //        for (int i = 0; i < 10; i++)
    //        {
    //            //Instantiate(meteors[Random.Range(0,4)], spawnPosition, Quaternion.identity);

    //            Parts newMeteor = poolManager.GetFromPool<Parts>(5);
    //            newMeteor.transform.position = SpawnPosition();
    //            partsTimeBetSpawn = Random.Range(partsSpawnMin, partsSpawnMax);
    //        }
    //    }
    //}
    //IEnumerator SpawnMeteor()
    //{
    //    while(!GameManager.instance.isLanding)
    //    {
    //        yield return new WaitForSeconds(meteorTimeBetSpawn);

    //        for(int i = 0; i < spawnCount; i++)
    //        {
    //            //Instantiate(meteors[Random.Range(0,4)], spawnPosition, Quaternion.identity);
    //            int ran = Random.Range(1, 3);

    //            Meteor newMeteor = poolManager.GetFromPool<Meteor>(ran);
    //            newMeteor.transform.position = SpawnPosition();
    //            meteorTimeBetSpawn = Random.Range(0, meteorSpawnMax);
    //        }
    //    }
    //}
    //IEnumerator SpawnCoin()
    //{
    //    while (!GameManager.instance.isLanding)
    //    {
    //        yield return new WaitForSeconds(coinTimeBetSpawn);

    //        var coin = Instantiate(coins[Random.Range(0,2)]);
    //        coin.transform.position = SpawnPosition();
    //        coinTimeBetSpawn = Random.Range(coinSpawnMin, coinSpawnMax);
    //    }
    //}
    //IEnumerator SpawnItem()
    //{
    //    while(!GameManager.instance.isLanding)
    //    {
    //        yield return new WaitForSeconds(itemTimeBetSpawn);
    //        for (int i = 0; i < 10; i++)
    //        {
    //            Item newItem = poolManager.GetFromPool<Item>(4);
    //            newItem.transform.position = SpawnPosition();
    //        }
    //    }
    //}
    public void ReturnPool(Meteor clone)
    {
        poolManager.TakeToPool<Meteor>(clone.idName, clone);
    }
    public void ReturnPool(Parts clone)
    {
        poolManager.TakeToPool<Parts>(clone.idName, clone);
    }
    public void ReturnPool(Item clone)
    {
        poolManager.TakeToPool<Item>(clone.idName, clone);
    }
    public void ReturnPool(Coin clone)
    {
        poolManager.TakeToPool<Coin>(clone.idName, clone);
    }

    public Vector3 SpawnPosition()
    {
        float randomX = Random.Range(spawnZone.position.x - spawnZone.localScale.x / 2f, spawnZone.position.x + spawnZone.localScale.x / 2f);
        float randomY = Random.Range(spawnZone.position.y - spawnZone.localScale.y / 2f, spawnZone.position.y + spawnZone.localScale.y / 2f);
        float randomZ = Random.Range(spawnZone.position.z - spawnZone.localScale.z / 2f, spawnZone.position.z + spawnZone.localScale.z / 2f);

        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

        return spawnPosition;
    }

}

