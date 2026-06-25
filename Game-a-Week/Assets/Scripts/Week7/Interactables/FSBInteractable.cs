using System.Collections.Generic;
using UnityEngine;

public abstract class FSBInteractable : MonoBehaviour
{
    public List<string> WhatCanInteract = new(); //List of string interactions that can interact.

    public string identification; //What does this interactable go by?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function for the on click interaction
    public abstract void OnClickInteraction(FSBPlayer player);

    //Function to check for interaction compatability
    public bool CheckInteractionCompatability(string inInteractingItem)
    {
        if (WhatCanInteract.Contains(inInteractingItem)) return true;

        else return false;
    }
}
