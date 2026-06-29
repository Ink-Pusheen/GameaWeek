using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimalCustomerBase : FSBInteractable
{
    public FSBRecipeSO RequestedOrder; //Order that this animal is expecting

    public GameObject RequestOrderPopup; //Popup that'll display the order
    [SerializeField] Image RequestedOrderImage; //Image to show what they want

    [SerializeField] TMP_Text RequestName; //Name of what is wanted

    public FSBOrderPosition assignedPosition; //Position this was assigned

    public GameObject CupHoldSpot; //Where the cup will be held

    [SerializeField] Sprite HappySprite; //Order was done correctly
    [SerializeField] Sprite UpsetSprite; //Order was done incorrectly

    [SerializeField] bool bStartExit; //Bool to initiate the animal to take their exit

    public override void OnClickInteraction(FSBPlayer player)
    {
        //Debug.Log("Animal Click");

        if (player.ReturnHeldItem() is null || RequestedOrder is null) return;

        FSBCupInteractable cup = player.ReturnHeldItem().GetComponent<FSBCupInteractable>();

        if (cup is null) return;

        GetComponent<Collider>().enabled = false; //Disable self collider to not allow for further interaction

        cup.gameObject.transform.position = CupHoldSpot.transform.position; //Set the cup transform
        cup.transform.parent = CupHoldSpot.transform; //And set the parent

        player.ChangeHeldInteractable(null); //Set player hand back to null

        if (RequestOrderPopup != null) RequestOrderPopup.SetActive(false); //Hide this once an order is given

        CheckOrder(cup.containedIngredients, cup);
    }

    public virtual void CheckOrder(List<string> ingredientsToCheck, FSBCupInteractable cup)
    {
        bool matches = RequestedOrder.CompareRecipeForMatch(ingredientsToCheck);

        if (matches) Debug.Log("Order Matches");

        else if (!matches) Debug.Log("Order does not Match");

        if(assignedPosition is not null) FSBAnimalSpawner.Instance.orderPositions.Add(assignedPosition); //Return the occupied position
    }

    public void DisplayOrder()
    {
        if (RequestedOrder is null) return; //Don't proceed is this isn't allocated

        RequestedOrderImage.sprite = RequestedOrder.RequestSprite;
        RequestName.text = RequestedOrder.name;
    }

    public void PositiveReview()
    {
        Debug.Log("Customer is happy");
        if(HappySprite is not null) GetComponent<SpriteRenderer>().sprite = HappySprite; //Change to happy sprite

        FSBScoreManager.instance.IncrementPositiveReviews(); //Update the board
    }

    public void NegativeReview()
    {
        Debug.Log("Customer isn't happy");
        if(UpsetSprite is not null) GetComponent<SpriteRenderer>().sprite = UpsetSprite; //Change to unhappy sprite

        FSBScoreManager.instance.IncrementNegativeReviews(); //Update the board
    }

    private void Update()
    {
        if (!bStartExit) return; //Don't proceed till such
    }

    public void ToggleExitState()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.useGravity = true;

        rb.AddForce(Vector3.up * Random.Range(2, 4), ForceMode.Impulse);

        Destroy(gameObject, 2.25f);
    }

    private void OnDestroy()
    {
        if (assignedPosition is not null) FSBAnimalSpawner.Instance.orderPositions.Add(assignedPosition); //Return the occupied position
    }
}
