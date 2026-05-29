using System;
using UnityEngine;

public class SpawnButtonInteractable : ClickInteractable
{
    [SerializeField] GameObject ItemToSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interaction(ClickInteractable inPreviousInteractable)
    {
        if (Placement == null) throw new ArgumentException("Placement shouldn't be null");

        if (ItemToSpawn is null) throw new ArgumentException("Needs item to spawn");

        if (Placement.bOccupied) return;

        else
        {
            GameObject spawn = Instantiate(ItemToSpawn);

            Placement.AssignSpawnedItem(spawn.GetComponent<ClickInteractable>());
        }
    }
}
