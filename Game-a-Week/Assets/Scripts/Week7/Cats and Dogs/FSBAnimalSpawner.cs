using System.Collections.Generic;
using UnityEngine;

public class FSBAnimalSpawner : MonoBehaviour
{
    public static FSBAnimalSpawner Instance;

    [Header("Spawning Based")]

    [SerializeField] GameObject[] AnimalPrefabs;

    public List<FSBOrderPosition> orderPositions = new();

    [Header("Timer Base")]

    [SerializeField] float TimeTillNextCustomer; //Time until next customer spawn
    [SerializeField] float TimeBetweenCustomers; //Time reset

    public bool bRunSpawner = false; //Should the spawner be running?

    private void Awake()
    {
        Instance = this;

        TimeTillNextCustomer = TimeBetweenCustomers;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bRunSpawner) return; //Don't run the spawner... simple

        if (orderPositions.Count is 0) return; //Don't run timer if there are no more slots

        //Timer
        TimeTillNextCustomer -= Time.deltaTime;

        //Once timer is 0 or below
        if (TimeTillNextCustomer <= 0)
        {
            TimeTillNextCustomer = TimeBetweenCustomers; //Reset the timer

            SpawnAnimal(); //Spawn an animal
        }

    }

    void SpawnAnimal()
    {
        if (!bRunSpawner) return; //Failsafe if the timer isn't meant to run via loss condition

        //Grab a random position and animal to spawn
        int position = Random.Range(0, orderPositions.Count);

        int animalToSpawn = Random.Range(0, AnimalPrefabs.Length);

        //Spawn the animal and set the assigned position
        GameObject spawnedAnimal = Instantiate(AnimalPrefabs[animalToSpawn], orderPositions[position].transform.position, Quaternion.identity);
        spawnedAnimal.GetComponent<AnimalCustomerBase>().assignedPosition = orderPositions[position];

        orderPositions.RemoveAt(position); //Remove the used position
    }

    //Public function to toggle if this should be ran
    public void ToggleTimerState()
    {
        bRunSpawner = !bRunSpawner;
    }
}
