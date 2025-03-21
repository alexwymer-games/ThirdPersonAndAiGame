using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemySpawner : MonoBehaviour
{
    //Container Object for Enemy Spawns
    [SerializeField] private GameObject enemyContainer;

    //Enemy Prefab
    [SerializeField] private GameObject basicEnemyPrefab;

    //Wave Variables 
    [SerializeField] private int timeBetweenSpawnMin;
    [SerializeField] private int timeBetweenSpawnMax;
    private float timeBetweenSpawn = 0;
    private float countDownBetweenSpawns;

    [SerializeField] private List<Transform> spawnPointList = new List<Transform>();

    [SerializeField] private GameObject playerObjectReference;


    private void Update()
    {
        SpawnEnemyIfReady();
    }

    //----------//
    // TRIGGER //
    //--------//

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerObjectReference = collision.gameObject;
        }
    }


    //--------------//
    // ENEMY SPAWN //
    //------------//

    private void SpawnEnemyIfReady()
    {
        //Spawn Enemy if Ready 
        if (countDownBetweenSpawns <= 0.0f)
        {
            GenerateRandomTimeBetweenSpawns();
            SpawnEnemy();
        }
        //Decrease Timer 
        countDownBetweenSpawns -= Time.deltaTime;
    }

    private void GenerateRandomTimeBetweenSpawns()
    {
        //Generate new time between min and max
        timeBetweenSpawn = Random.Range(timeBetweenSpawnMin, timeBetweenSpawnMax);
        countDownBetweenSpawns = timeBetweenSpawn;
    }

    private void SpawnEnemy()
    {
        //Get Random Spawn Point Index
        int randomSpawnIndex = Random.Range(0, spawnPointList.Count);

        //Get Vec3 of RandomSpawn Point from list
        Vector3 spawnPointPos = spawnPointList[randomSpawnIndex].transform.position;

        //Spawn New Enemy 
        GameObject spawnedEnemy = Instantiate(basicEnemyPrefab, spawnPointPos, Quaternion.identity, enemyContainer.transform);

        spawnedEnemy.GetComponent<AiAgentController>().SetPlayerTargetReference(playerObjectReference.transform);
    }

}
