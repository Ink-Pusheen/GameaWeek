using System;
using System.Collections.Generic;
using UnityEngine;

public class FSBCupInteractable : FSBInteractable
{
    [SerializeField] GameObject CupToSpawn; //Prefab of cup to give to player

    public List<string> containedIngredients = new(); //What is this cup currently holding?

    public override void OnClickInteraction(FSBPlayer player)
    {
        if (player.ReturnHeldItem() is null) //If the player is currently holding nothing
        {
            GameObject spawnedCup = Instantiate(CupToSpawn, player.HoldSpot.transform.position, Quaternion.identity, player.HoldSpot.transform);

            FSBCupInteractable cupScript = spawnedCup.GetComponent<FSBCupInteractable>();

            if (cupScript is null) return;

            player.ChangeHeldInteractable(cupScript); //Change the currently held item
        }
        else
        {
            Debug.Log("Player hand already full");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSprite()
    {
        if (containedIngredients.Count is 0) return;

        //Compare the list and change the sprite accordingly as long as there is one ingredient

        if (identification is "Dog Cup") GetComponent<SpriteRenderer>().sprite = OrderManager.instance.CompeareDogCup(containedIngredients);

        else if (identification is "Cat Cup") GetComponent<SpriteRenderer>().sprite = OrderManager.instance.CompareCatCup(containedIngredients);
    }
}
