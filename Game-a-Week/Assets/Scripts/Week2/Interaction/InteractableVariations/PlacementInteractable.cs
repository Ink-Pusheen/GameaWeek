using UnityEngine;

public class PlacementInteractable : ClickInteractable
{
    public bool bOccupied; //Is this spot currently occupied via any item?

    public GameObject heldItem; //What the spot is currently holding

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
        if (bOccupied)
        {
            //Check if the player has something in hand [ Nulls if so ]
            if (inPreviousInteractable != null)
            {
                //Swap the items
                Debug.Log("Hand Occupied");
            }
        }
        else if (!bOccupied && inPreviousInteractable != null)
        {
            //Indicate an item here
            bOccupied = true;
            heldItem = inPreviousInteractable.gameObject;
            heldItem.transform.position = transform.position;
            heldItem.transform.rotation = transform.rotation;
            inPreviousInteractable.Placement = this;
            heldItem.transform.SetParent(null);

            heldItem.GetComponent<Collider>().enabled = true;

            //Indicate player hand empty
            FloralMagicPlayerScript.instance.ChangeSelectedInteractable(null);
        }
    }

    public void AssignSpawnedItem(ClickInteractable inPreviousInteractable)
    {
        //Indicate an item here
        bOccupied = true;
        heldItem = inPreviousInteractable.gameObject;
        heldItem.transform.position = transform.position;
        inPreviousInteractable.Placement = this;
        heldItem.transform.SetParent(null);

        heldItem.GetComponent<Collider>().enabled = true;
    }

    //Reset all attributes
    public void RemoveItem()
    {
        //Debug.Log("RemoveItem");
        bOccupied = false;
        heldItem = null;
    }
}
