using System.Collections.Generic;
using UnityEngine;

public class CatCustomer : AnimalCustomerBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RequestedOrder = OrderManager.instance.RandomCatRecipe();
    }

    public override void OnClickInteraction(FSBPlayer player)
    {
        Debug.Log("Cat Click");

        if (player.ReturnHeldItem() is null || RequestedOrder is null) return;

        FSBCupInteractable cup = player.ReturnHeldItem().GetComponent<FSBCupInteractable>();

        if (cup is null) return;

        CheckOrder(cup.containedIngredients, cup);
    }

    public override void CheckOrder(List<string> ingredientsToCheck, FSBCupInteractable cup)
    {
        bool matches = RequestedOrder.CompareRecipeForMatch(ingredientsToCheck);
        if (cup.identification is not "Cat Cup") matches = false; //Will default false last if this check isn't met

        if (matches) PositiveReview(); //Positive for getting it right

        else if (!matches) NegativeReview(); //Negative for getting something wrong
    }
}
