using UnityEngine;

public class PCTabletop : PCInteractable
{
    [SerializeField] GameObject itemPlacementSpot; //Where items will be placed

    [SerializeField] GameObject heldItem; //Does this table have an item that's on top of it

    public override void OnInteract(PCPlayerController player)
    {
        //throw new System.NotImplementedException();
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
