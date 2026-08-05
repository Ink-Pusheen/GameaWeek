using UnityEngine;

public class PCTurnInPolished : PCInteractable
{
    public override void OnInteract(PCPlayerController player)
    {
        if (player.heldItem != null)
        {
            PCCardboardBox presumedBox = player.heldItem.GetComponent<PCCardboardBox>();

            if (presumedBox is null) return; //Don't proceed if not a cardboard box

            PCOrderManagerPolished.Instance.CompareInputOrder(presumedBox); //Run the check through the order manager

            Destroy(presumedBox.gameObject);

            player.ChangeHeldItem(null);
        }
    }
}
