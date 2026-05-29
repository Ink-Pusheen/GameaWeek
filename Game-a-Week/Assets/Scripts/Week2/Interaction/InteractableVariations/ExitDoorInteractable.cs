using UnityEngine;

public class ExitDoorInteractable : ClickInteractable
{
    public override void Interaction(ClickInteractable inPreviousInteractable)
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
