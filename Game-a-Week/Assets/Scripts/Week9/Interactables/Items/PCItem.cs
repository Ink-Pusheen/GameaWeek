using UnityEngine;

public abstract class PCItem : MonoBehaviour
{
    public abstract void ItemInteraction(PCItem playerHeldItem, PCPlayerController player);
}
