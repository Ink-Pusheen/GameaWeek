using UnityEngine;

public class CCBoxSpawner : MonoBehaviour
{
    [SerializeField] GameObject BoxPrefab;

    [SerializeField] GameObject[] spawnSpots; //Where boxes can be spawned

    [SerializeField] float spawnTimer; //Time remaining till spawn
    [SerializeField] float timeBetweenBoxes; //Time to set between box spawns

    public bool bRunSpawner = false; //Is the spawner currently running? <Default false>

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        Debug.Log("Spawn box");

        float zOffset = Random.Range(-0.5f, 0.5f);
        Vector3 spawnOffset = new Vector3(0, 0, zOffset);

        Instantiate(BoxPrefab, boxSpawnSpot + spawnOffset, Quaternion.identity);
    }
}
