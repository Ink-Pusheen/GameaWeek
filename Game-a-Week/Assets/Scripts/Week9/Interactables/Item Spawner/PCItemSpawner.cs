using UnityEngine;

public class PCItemSpawner : PCInteractable
{
    [SerializeField] GameObject goItemToSpawn; //What item should this spawner give?

    public override void OnInteract(PCPlayerController player)
    {
        Debug.Log("Spawn item");

        if (goItemToSpawn == null) return; //Don't do anything if this is currently null
        if (player.heldItem != null) return; //Don't give an item if the player is currently holding something

        //If not spawn the item in the players hands
        GameObject SpawnedObject = Instantiate(goItemToSpawn, player.holdItemSpot.transform.position, Quaternion.identity);

        player.ChangeHeldItem(SpawnedObject);
    }
}
