using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform playerPos;
    public GameObject item;
    public GameObject meteor;

    public float meteorSpawnMax = 0.5f;
    //public float meteorSpawnMin = 1f;
    private float meteorTimeBetSpawn;

    public float itemSpawnMax = 3f;
    public float itemSpawnMin = 1f;
    private float itemTimeBetSpawn;

    private float obstacleLastSpawnTime;
    private float itemLastSpawnTime;

    private float spawnRange = 50f;

    public Vector3 offset;

    void Start()
    {
        meteorTimeBetSpawn = Random.Range(0, meteorSpawnMax);
        itemTimeBetSpawn = Random.Range(itemSpawnMin, itemSpawnMax);
        obstacleLastSpawnTime = 0f;
        itemLastSpawnTime = 0f;

        offset = transform.position - playerPos.position;
        StartCoroutine(SpawnMeteor());
        StartCoroutine(SpawnItem());
    }

    void Update()
    {
        transform.position = offset + playerPos.position;

        //if(itemTimeBetSpawn + itemLastSpawnTime < Time.time)
        //{

        //}

    }

    IEnumerator SpawnMeteor()
    {
        while(true)
        {
            yield return new WaitForSeconds(meteorTimeBetSpawn);
            meteorTimeBetSpawn = Random.Range(0, meteorSpawnMax);
            Vector3 spawnPosition = transform.position;

            // 무작위한 위치를 생성 반경 내에서 선택
            Vector2 randomOffset = Random.insideUnitCircle * spawnRange;
            spawnPosition.x += randomOffset.x;
            spawnPosition.y += randomOffset.y;
            Instantiate(meteor, spawnPosition, Quaternion.identity);
            Debug.Log("메테오 생성");
        }
    }

    IEnumerator SpawnItem()
    {
        while(true)
        {
            yield return new WaitForSeconds(itemTimeBetSpawn);
            itemTimeBetSpawn = Random.Range(itemSpawnMin, itemSpawnMax);
            Vector3 spawnPosition = transform.position;

            // 무작위한 위치를 생성 반경 내에서 선택
            Vector2 randomOffset = Random.insideUnitCircle * spawnRange;
            spawnPosition.x += randomOffset.x;
            spawnPosition.y += randomOffset.y;
            Instantiate(item, spawnPosition, Quaternion.identity);
            Debug.Log("스피드 아이템 생성");
        }
    }
}
