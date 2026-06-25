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
}
