using UnityEngine;
using UnityEngine.UI;

public class FSBBellInteractable : FSBInteractable
{
    [SerializeField] bool bCanBeRung = true; //Default true, can be rung to start the game

    [SerializeField] SpriteRenderer SignImage; //Image to the sign
    [SerializeField] Sprite SignOpen; //Sprite for the store being open
    [SerializeField] Sprite SignClosed; //Sprite the store being closed

    public override void OnClickInteraction(FSBPlayer player)
    {
        if (!bCanBeRung) return; //Return if it's already been rung

        //Functionality to start the game
        bCanBeRung = !bCanBeRung;

        //Start the Spawn Timer
        FSBAnimalSpawner.Instance.ToggleTimerState();

        if (SignImage is null) return; //Return, else update the sign

        if(!bCanBeRung) SignImage.sprite = SignOpen;

        else SignImage.sprite = SignClosed;
    }
}
