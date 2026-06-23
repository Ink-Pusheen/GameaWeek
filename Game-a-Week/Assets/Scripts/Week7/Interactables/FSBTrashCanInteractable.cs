using UnityEngine;

public class FSBTrashCanInteractable : FSBInteractable
{
    public override void OnClickInteraction(FSBPlayer player)
    {
        if (player.ReturnHeldItem() is not null)
        {
            Destroy(player.ReturnHeldItem().gameObject);

            player.ChangeHeldInteractable(null);
        }
        else
        {
            Debug.Log("Hands are empty");
        }
    }
}
