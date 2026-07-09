using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OrderContents
{
    public string itemName; //Name of the needed item

    public int itemQuantity; //Static amount of the item

    public bool rangedQuantity; //Can this item be requested more than once?
    public int lowerRequest; //Lower and upper request bounds
    public int upperRequest;

    public Sprite displaySprite; //Display of the order content
}

[CreateAssetMenu(menuName = "Packin Crew/Order", fileName = "Packin Crew/Order")]
public class PCOrder : ScriptableObject
{
    public float deliveryTimeAllowed; //How much time until this order expires?

    public OrderContents[] orderContent; //Array of the order contents

    public int rewardAmount; //How much money does this order type give?

    public bool hasRandomizedAmounts; //Does this order have a randomized Variable to the amount of items?
}
