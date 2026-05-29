using UnityEngine;

public class TrashInteractable : ClickInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interaction(ClickInteractable inPreviousInteractable)
    {
        bool canInteract = CanInteract(inPreviousInteractable);

        if (canInteract)
        {
            Destroy(inPreviousInteractable.gameObject);

            FloralMagicPlayerScript.instance.ChangeSelectedInteractable(null);
        }
        else
        {
            Debug.Log("No item or cannot be tossed");
        }
    }
}
