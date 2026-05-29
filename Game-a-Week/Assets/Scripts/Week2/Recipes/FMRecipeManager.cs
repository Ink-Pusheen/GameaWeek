using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FMRecipeManager : MonoBehaviour
{
    public static FMRecipeManager instance;

    [SerializeField] List<RecipeFormulaSO> formulas = new();

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public RecipeFormulaSO CheckForRecipe(List<string> inIngredients)
    {
        bool bRecipeFound = false; //Was a recipe found?
        int recipeIndex = -1; //At what index was the recipe found?

        for (int recipe = 0; recipe < formulas.Count; recipe++)
        {
            if (inIngredients.SequenceEqual(formulas[recipe].requiredMaterials))
            {
                bRecipeFound = true;
                recipeIndex = recipe;
                break;
            }

            else continue;
        }

        if (bRecipeFound)
        {
            Debug.Log("Recipe Found");
            return formulas[recipeIndex];
        }

        else
        {
            Debug.Log("No Recipe Found");
            return null;
        }
    }
}
