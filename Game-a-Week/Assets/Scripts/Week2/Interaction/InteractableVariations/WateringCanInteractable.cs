using System.Collections.Generic;
using UnityEngine;

public class WateringCanInteractable : ClickInteractable
{
    public List<string> contents = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Adding contents to the Watering Can
    public void AddContents(List<string> inContents)
    {
        contents = inContents;
    }

    //Remove all contents from the Watering can
    public void RemoveContents()
    {
        contents.Clear();
    }

    //Interaction override
    public override void Interaction(ClickInteractable inPreviousInteractable)
    {
        base.Interaction(inPreviousInteractable);

        bool canInteract = CanInteract(inPreviousInteractable);

        if (canInteract)
        {
            //Check for the flask script
            FlaskInteractable Flask = inPreviousInteractable.GetComponent<FlaskInteractable>();

            if (Flask is null) return;

            else
            {
                for (int element = 0; element < Flask.containedElements.Count; element++)
                {
                    contents.Add(Flask.containedElements[element]);
                }

                Flask.containedElements.Clear();
            }
        }

        else
        {
            //Check if it can be picked up
            bool HandOpen = CheckPlayerHandAvailability();

            if (HandOpen)
            {
                FloralMagicPlayerScript.instance.ChangeSelectedInteractable(this);
            }
        }
    }
}
