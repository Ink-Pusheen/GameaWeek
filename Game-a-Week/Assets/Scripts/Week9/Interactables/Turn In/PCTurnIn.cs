using UnityEngine;

public class PCTurnIn : PCInteractable
{
    public override void OnInteract(PCPlayerController player)
    {
        if(player.heldItem != null)
        {
            PCCardboardBox presumedBox = player.heldItem.GetComponent<PCCardboardBox>();

            if (presumedBox is null) return; //Don't proceed if not a cardboard box

            PCOrderManager.Instance.CompareInputOrder(presumedBox); //Run the check through the order manager

            Destroy(presumedBox.gameObject);

            player.ChangeHeldItem(null);
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
