using System.Collections.Generic;
using UnityEngine;

public class PCCardboardBox : PCItem
{
    public List<string> contents = new(); //List of what is currently in the cardboard box

    public override void ItemInteraction(PCItem playerHeldItem, PCPlayerController player)
    {
        PCDeliverableItem item = playerHeldItem.GetComponent<PCDeliverableItem>();

        if(item is not null)
        {
            if (item.bCanBePacakged)
            {
                contents.Add(item.ItemName); //Add the item name to the package

                Destroy(item.gameObject);

                player.ChangeHeldItem(null);
            }
        }
    }
}
