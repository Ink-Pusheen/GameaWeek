using UnityEngine;
using UnityEngine.UI;

public class FlowerpotInteractable : ClickInteractable
{
    [SerializeField] RecipeFormulaSO PottedPlantSO;

    [SerializeField] Image flowerSprite;

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
            //Check if it is the watering can
            WateringCanInteractable WateringCan = inPreviousInteractable.GetComponent<WateringCanInteractable>();

            if (WateringCan is null) return;

            else
            {
                if (WateringCan.contents.Count == 0)
                {
                    Debug.Log("No Watering Can Contents");
                    return;
                }

                PottedPlantSO = FMRecipeManager.instance.CheckForRecipe(WateringCan.contents);

                WateringCan.contents.Clear(); //Clear the contents

                if (PottedPlantSO is null)
                {
                    Debug.Log("No possible plant");
                }

                else
                {
                    Debug.Log(PottedPlantSO);
                    ApplyPlant();

                    FMDiscoveryBook.instance.IdentifyCheck(PottedPlantSO.index);
                }
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

    public void ApplyPlant()
    {
        flowerSprite.enabled = true;
        flowerSprite.sprite = PottedPlantSO.flowerSprite;
    }

}
