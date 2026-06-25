using System.Collections.Generic;
using UnityEngine;

public class AnimalCustomerBase : FSBInteractable
{
    public FSBRecipeSO RequestedOrder; //Order that this animal is expecting

    public override void OnClickInteraction(FSBPlayer player)
    {
        Debug.Log("Animal Click");

        if (player.ReturnHeldItem() is null) return;

        FSBCupInteractable cup = player.ReturnHeldItem().GetComponent<FSBCupInteractable>();

        if (cup is null) return;

        CheckOrder(cup.containedIngredients, cup);
    }

    public virtual void CheckOrder(List<string> ingredientsToCheck, FSBCupInteractable cup)
    {
        bool matches = RequestedOrder.CompareRecipeForMatch(ingredientsToCheck);

        if (matches) Debug.Log("Order Matches");

        else if (!matches) Debug.Log("Order does not Match");
    }

    public void PositiveReview()
    {
        Debug.Log("Customer is happy");
    }

    public void NegativeReview()
    {
        Debug.Log("Customer isn't happy");
    }
}
