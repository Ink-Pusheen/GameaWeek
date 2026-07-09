using UnityEngine;

public class PCItemSpawner : PCInteractable
{
    [SerializeField] GameObject goItemToSpawn; //What item should this spawner give?

    public override void OnInteract(PCPlayerController player)
    {
        Debug.Log("Spawn item");

        if (goItemToSpawn == null) return; //Don't do anything if this is currently null

        //Check if the players hands are empty

        //If not spawn the item in the players hands
        GameObject SpawnedObject = Instantiate(goItemToSpawn, player.holdItemSpot.transform.position, Quaternion.identity); //Replace the spawn position to the player hands later
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
