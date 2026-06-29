using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class DogCustomer : AnimalCustomerBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RequestedOrder = OrderManager.instance.RandomDogRecipe();

        DisplayOrder(); //Display the requested order
    }

    public override void OnClickInteraction(FSBPlayer player)
    {
        Debug.Log("Dog Click");

        base.OnClickInteraction(player);
    }

    public override void CheckOrder(List<string> ingredientsToCheck, FSBCupInteractable cup)
    {
        bool matches = RequestedOrder.CompareRecipeForMatch(ingredientsToCheck);
        if (cup.identification is not "Dog Cup") matches = false; //Will default false last if this check isn't met

        if (matches) PositiveReview(); //Positive for getting it right

        else if (!matches) NegativeReview(); //Negative for getting something wrong

        Invoke("ToggleExitState", 2); //Toggle the exit state
    }
}
