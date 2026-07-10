using UnityEngine;

public abstract class PCItem : MonoBehaviour
{
    [SerializeField] GameObject leftHandHoldLocation; //Where the left hand rests holding this object
    [SerializeField] GameObject rightHandHoldLocation; //Where the right hand rests holding this object

    public abstract void ItemInteraction(PCItem playerHeldItem, PCPlayerController player);

    //Function to set the holding positions of this object
    public void SetPlayerHandPosition(PCPlayerController player)
    {
        player.SetHandPositions(leftHandHoldLocation, rightHandHoldLocation);
    }
}
