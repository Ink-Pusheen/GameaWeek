using UnityEngine;

public class PCTabletop : PCInteractable
{
    [SerializeField] GameObject itemPlacementSpot; //Where items will be placed

    [SerializeField] GameObject heldItem; //Does this table have an item that's on top of it

    public override void OnInteract(PCPlayerController player)
    {
        //throw new System.NotImplementedException();
        if (heldItem != null) //Table item is not null
        {
            if(player.heldItem == null) //Player has no item
            {
                player.ChangeHeldItem(heldItem);

                heldItem = null;
            }
            else //Player has item, have interaction on currently held item
            {
                PCItem item = heldItem.GetComponent<PCItem>();

                if (item is null) return; //Return if this doesn exist

                item.ItemInteraction(player.heldItem.GetComponent<PCItem>(), player); //Indicate interaction
            }
        }
        else //Case that the table has nothing on it
        {
            if(player.heldItem != null) //Player has an Item
            {
                heldItem = player.heldItem; //Get the players current item
                heldItem.transform.position = itemPlacementSpot.transform.position; //Set the new position
                heldItem.transform.rotation = itemPlacementSpot.transform.rotation; //Set the rotation

                heldItem.transform.SetParent(null); //Remove the parent

                player.ChangeHeldItem(null); //Reset the player hand
            }
            else
            {
                Debug.Log("Nothing, player has no items");
            }
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
}
