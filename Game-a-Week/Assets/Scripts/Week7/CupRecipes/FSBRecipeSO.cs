using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FSBRecipe", menuName = "Furlicious Sweet Bar/Recipe")]
public class FSBRecipeSO : ScriptableObject
{
    public Sprite RequestSprite; //Visual representation of the order

    public List<string> RequiredItems = new(); //List of items needed for this recipe

    public int MoneyGranted; //How much money this recipe will give

    public bool CompareRecipeForMatch(List<string> inIngredientsList)
    {
        if (inIngredientsList.Count != RequiredItems.Count) return false; //Count doesn't match, return false

        else
        {
            int ingredientMatch = 0; //Integer to represent the matches

            //Loop through the items accordingly
            for (int item = 0; item < RequiredItems.Count; item++)
            {
                if (inIngredientsList.Contains(RequiredItems[item])) ingredientMatch++; //Increment if there is a match
            }

            if (ingredientMatch == RequiredItems.Count) return true; //Return true if the count is accordingly

            else return false; //Else return false
        }
    }
}
