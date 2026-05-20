using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    [Header("Ships")]

    [SerializeField] GameObject[] shipCatalog; //Catalog of all ships that can appear

    [SerializeField] List<GameObject> activeShips = new(); //List of what can spawn


    [Header("Spawn Variables")]

    public float timeBetweenSpawns;

    [SerializeField] float lastSpawnTime;

    [SerializeField] Vector3 startPosition;
    [SerializeField] float spawnOffset; //Offset which can randomly be placed between

    public bool bSpawnEnemies;

    [SerializeField] float[] difficultySpikeTimes; //When should the game get harder?
    [SerializeField] float lastDifficultyTimeApplied; //For calculation purposes
    [SerializeField] int curProgression; //Where the game currently is in terms of progression

    private void Awake()
    {
        startPosition = transform.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bSpawnEnemies) return; //Return if it is not time yet to spawn enemies

        if (Time.time > lastSpawnTime + timeBetweenSpawns)
        {
            lastSpawnTime = Time.time;

            SpawnEnemyShip();
        }

        if (Time.time >= difficultySpikeTimes[curProgression] + lastDifficultyTimeApplied)
        {
            //Add to the difficulty until the finale [Total 4 min]
            switch (curProgression)
            {
                case 0: //30 seconds
                    timeBetweenSpawns -= 2; //2sec / spawn
                    break;

                case 1: //45 seconds
                    activeShips.Add(shipCatalog[1]); //Medium ship
                    break;

                case 2: //15 seconds
                    timeBetweenSpawns -= 0.25f; //1.75sec / spawn
                    break;

                case 3: //45 seconds
                    timeBetweenSpawns -= 0.25f; //1.5sec / spawn
                    break;

                case 4: //30 sec
                    activeShips.Add(shipCatalog[2]); //Big ship
                    break;

                case 5: //30 sec
                    timeBetweenSpawns -= 0.5f; //1sec / spawn
                    break;

                case 6: //45 sec
                    //Winning condition
                    SpaceshipRachetManager.instance.GameWin();

                    StopSpawner();
                    break;
            }

            lastDifficultyTimeApplied = Time.time; //Reapply the time
            curProgression++;
        }
    }

    public void SpawnEnemyShip()
    {
        GameObject ShipToSpawn = SelectedEnemyShip();

        if (ShipToSpawn is null) return;

        transform.position = startPosition; //Set the initial position
        transform.position += new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, 0);

        Instantiate(ShipToSpawn, transform.position, transform.rotation);
    }

    private GameObject SelectedEnemyShip()
    {
        int chosenShip = Random.Range(0, activeShips.Count);

        return shipCatalog[chosenShip];
    }

    public void StartSpawner()
    {
        lastDifficultyTimeApplied = Time.time; //Set the initial difficulty time

        bSpawnEnemies = true;
    }

    public void StopSpawner()
    {
        bSpawnEnemies = false;

        //Reset the enemies that'll appear
        activeShips.Clear();

        activeShips.Add(shipCatalog[0]); //Re-add basic ship

        //Reset the timer
        curProgression = 0;

        //Reset the spawn timer
        timeBetweenSpawns = 4; //Default 4
    }
}
