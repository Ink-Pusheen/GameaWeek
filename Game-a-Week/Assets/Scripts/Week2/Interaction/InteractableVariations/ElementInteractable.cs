using UnityEngine;

public class ElementInteractable : ClickInteractable
{
    public string elementToAdd;

    public Color elementalColor; //Color that will change how the flask looks

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
        base.Interaction(inPreviousInteractable);

        bool canInteract = CanInteract(inPreviousInteractable);

        if (canInteract)
        {
            //Check if it is a flask
            FlaskInteractable flask = inPreviousInteractable.GetComponent<FlaskInteractable>();

            if (flask is null) return;

            else
            {
                if (flask.containedElements.Contains(elementToAdd)) return;

                else AddElement(flask);
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

    private void AddElement(FlaskInteractable inFlask)
    {
        inFlask.containedElements.Add(elementToAdd);
    }
}
