using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;


    [SerializeField] List<FSBRecipeSO> CatRecipes = new();

    [SerializeField] List<FSBRecipeSO> DogRecipes = new();

    private void Awake()
    {
        instance = this;
    }

    public FSBRecipeSO RandomCatRecipe()
    {
        if (CatRecipes.Count is 0) return null;

        int chosenRecipe = Random.Range(0, CatRecipes.Count);

        return CatRecipes[chosenRecipe];
    }

    public FSBRecipeSO RandomDogRecipe()
    {
        if (DogRecipes.Count is 0) return null;

        int chosenRecipe = Random.Range(0, DogRecipes.Count);

        return DogRecipes[chosenRecipe];
    }

    public Sprite CompareCatCup(List<string> inIngredients)
    {
        for (int recipe = 0; recipe < CatRecipes.Count; recipe++)
        {
            if (inIngredients.Count != CatRecipes[recipe].RequiredItems.Count) continue;

            int matches = 0;

            for (int ingredient = 0; ingredient < inIngredients.Count; ingredient++)
            {
                if (CatRecipes[recipe].RequiredItems.Contains(inIngredients[ingredient])) matches++;
            }

            if (matches != CatRecipes[recipe].RequiredItems.Count) continue;

            else return CatRecipes[recipe].RequestSprite;
        }

        return null;
    }

    public Sprite CompeareDogCup(List<string> inIngredients)
    {
        for (int recipe = 0; recipe < DogRecipes.Count; recipe++)
        {
            if (inIngredients.Count != DogRecipes[recipe].RequiredItems.Count) continue;

            int matches = 0;

            for (int ingredient = 0; ingredient < inIngredients.Count; ingredient++)
            {
                if (DogRecipes[recipe].RequiredItems.Contains(inIngredients[ingredient])) matches++;
            }

            if (matches != DogRecipes[recipe].RequiredItems.Count) continue;

            else return DogRecipes[recipe].RequestSprite;
        }

        return null;
    }

}
