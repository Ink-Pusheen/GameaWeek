using System.Collections.Generic;
using UnityEngine;

public class FSBIngredientInteractable : FSBInteractable
{
    [SerializeField] string IngredientIdentity; //What is this ingredient?

    //Does the cup need something before this ingredient is added?
    [SerializeField] List<string> RequiredPreviousIngredients = new();

    //Does the cup have an ingredient already that it cannot overlap?
    [SerializeField] List<string> UncombinableIngredients = new();

    public override void OnClickInteraction(FSBPlayer player)
    {
        if (player.ReturnHeldItem() is null) return; //Return if the player doesn't have a cup in hand

        FSBCupInteractable cup = player.ReturnHeldItem().GetComponent<FSBCupInteractable>();

        if (cup is not null)
        {
            //Check if this can be interacted with
            if (!CheckInteractionCompatability(cup.identification)) return; //Return if not

            //For loop to ensure that the base ingredient can go in without overlapping others or being added if another exists
            bool UnoverlapableIngredient = false;

            for (int ingredient = 0; ingredient < UncombinableIngredients.Count; ingredient++)
            {
                if (cup.containedIngredients.Contains(UncombinableIngredients[ingredient])) UnoverlapableIngredient = true;
            }

            if (cup.containedIngredients.Contains(IngredientIdentity) || UnoverlapableIngredient) return; //Don't dupe the ingredient

            if(RequiredPreviousIngredients.Count is not 0) //Check if there is a required ingredient before such
            {
                bool hasIngredient = false;

                for (int ingredient = 0; ingredient < RequiredPreviousIngredients.Count; ingredient++)
                {
                    if (cup.containedIngredients.Contains(RequiredPreviousIngredients[ingredient])) hasIngredient = true;
                }

                if(!hasIngredient) return; //Return if it doesn't have any of the specified ingredients

                cup.containedIngredients.Add(IngredientIdentity);

                cup.UpdateSprite(); //Update the sprite
            }
            else
            {
                cup.containedIngredients.Add(IngredientIdentity);

                cup.UpdateSprite(); //Update the sprite
            }
        }
    }
}
