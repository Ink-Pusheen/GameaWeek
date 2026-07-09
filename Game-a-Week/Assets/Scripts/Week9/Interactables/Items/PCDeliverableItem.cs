using UnityEngine;

public class PCDeliverableItem : PCItem
{
    public bool bCanBePacakged = false; //Can this item be packaged? <Default False>

    public string ItemName; //Name of the item

    public void AlterPackageAvailabilityState()
    {
        bCanBePacakged = true;
    }

    public override void ItemInteraction(PCItem playerHeldItem, PCPlayerController player)
    {
        //Nothing... yeah
    }
}
