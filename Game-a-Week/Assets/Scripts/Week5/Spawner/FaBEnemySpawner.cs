using System.Collections.Generic;
using UnityEngine;

public class FaBEnemySpawner : MonoBehaviour
{
    public static FaBEnemySpawner Instance;

    [Header("Spawning attributes")]

    [SerializeField] List<GameObject> possibleEnemies = new(); //List to hold all enemies that can spawn respectively

    [SerializeField] bool bRunSpawner = false; //Should the spawner be running? [Default false]

    [SerializeField] float timeBetweenSpawns; //Time between enemy spawns
    [SerializeField] float lastEnemySpawnTime; //Time.time of the last enemy spawn

    public List<FaBSpawnRegion> spawnRegions = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bRunSpawner) return;

        if (Time.time > timeBetweenSpawns + lastEnemySpawnTime)
        {
            //Spawn an enemy and reset the last enemy spawn time
            lastEnemySpawnTime = Time.time;

            SpawnEnemy();
        }
    }

    //Function to spawn an enemy on the map
    private void SpawnEnemy()
    {
        if (!bRunSpawner) return; //Last second swap to ensure none get spawned after game over

        //Make sure to spawn the enemy *away from the player to make them always not appear in the blink of an eye
        int Spawnlocation = Random.Range(0, spawnRegions.Count);

        GameObject SpawnedEnemy = possibleEnemies[Random.Range(0, possibleEnemies.Count)];

        Instantiate(SpawnedEnemy, spawnRegions[Spawnlocation].transform.position, Quaternion.identity);
    }

    public void StartSpawner()
    {
        lastEnemySpawnTime = Time.time; //Set the initial time

        bRunSpawner = !bRunSpawner; //Flip the spawner state
    }

    public void StopSpawner()
    {
        bRunSpawner = !bRunSpawner; //Flip the spawner state
    }
}
