using UnityEngine;

public class CCBoxSpawner : MonoBehaviour
{
    public static CCBoxSpawner instance;

    [SerializeField] GameObject[] BoxPrefabs;

    [SerializeField] GameObject[] spawnSpots; //Where boxes can be spawned

    [SerializeField] float spawnTimer; //Time remaining till spawn
    [SerializeField] float timeBetweenBoxes; //Time to set between box spawns
    [SerializeField] float[] TimeIncrements; //Times to change to as the combo increases

    public bool bRunSpawner = false; //Is the spawner currently running? <Default false>

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bRunSpawner) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            int spawnIndex = Random.Range(0, spawnSpots.Length);
            Vector3 spawn = spawnSpots[spawnIndex].transform.position;

            SpawnBox(spawn);

            spawnTimer = timeBetweenBoxes;
        }
    }

    public void SpawnBox(Vector3 boxSpawnSpot)
    {
        //Debug.Log("Spawn box"); //Testing
        if (!bRunSpawner) return;

        float zOffset = Random.Range(-0.5f, 0.5f);
        Vector3 spawnOffset = new Vector3(0, 0, zOffset);

        GameObject ChosenBox = BoxPrefabs[Random.Range(0, BoxPrefabs.Length)];

        Instantiate(ChosenBox, boxSpawnSpot + spawnOffset, Quaternion.identity);
    }

    public void AlterSpawnTimer(int increment)
    {
        timeBetweenBoxes = TimeIncrements[increment];
    }

    public void SwapSpawnerState()
    {
        bRunSpawner = !bRunSpawner;
    }
}
