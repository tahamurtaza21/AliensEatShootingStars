using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> SpawnPoints;
    [SerializeField] GameObject obstacle;

    [SerializeField] int maxNumofObstacles = 5;
    public int obstaclesInScene = 0;

    [SerializeField] float timeBtwSpawn = 0.75f;
    [SerializeField] float startTimeBtwSpawn = 1.25f;
    [SerializeField] float decreaseTime = 0.02f;
    [SerializeField] float minTime = 1.05f;
    
    // Update is called once per frame
    void Update()
    {
        AddBullet();
    }
    
    void AddBullet()
    {
        if (obstaclesInScene < maxNumofObstacles && timeBtwSpawn <= 0)
        {
            int spawnpoint = Random.Range(0, SpawnPoints.Count);
            GameObject instantiatedobstacle = Instantiate(obstacle, SpawnPoints[spawnpoint].transform.position, Quaternion.identity, SpawnPoints[spawnpoint].transform);
            timeBtwSpawn = startTimeBtwSpawn;
            
            if(startTimeBtwSpawn > minTime)
            {
               startTimeBtwSpawn -= decreaseTime;
            }

            obstaclesInScene++;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
