using System.Collections.Generic;
using UnityEngine;

public class FlaskInteractable : ClickInteractable
{
    public List<string> containedElements = new();


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
            //Attempt to get the Element script
            ElementInteractable element = inPreviousInteractable.GetComponent<ElementInteractable>();

            //Attempt to get the Watering Can Script
            WateringCanInteractable wateringCan = inPreviousInteractable.GetComponent<WateringCanInteractable>();

            if (element is not null)
            {
                containedElements.Add(element.elementToAdd);
            }

            if (wateringCan is not null)
            {
                wateringCan.Interaction(this);
            }
        }
    }
}
