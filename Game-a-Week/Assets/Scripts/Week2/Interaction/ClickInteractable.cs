using System;
using System.Collections.Generic;
using UnityEngine;

public class ClickInteractable : MonoBehaviour
{
    public WhatCanInteractSO InfluencingInteractable; //What type of interactable is this?

    public List<WhatCanInteractSO> WhatCanInteract = new(); //List of any and all other Interactables that can be inflenced by this. [Essentially check the previously selected object can interact]

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Using what was previously selected for this interaction
    public virtual void Interaction(ClickInteractable inPreviousInteractable)
    {
        if (WhatCanInteract.Contains(inPreviousInteractable.InfluencingInteractable))
        {
            Debug.Log("This can be interacted with");
        }
        else
        {
            Debug.Log("This cannot be interacted with");
        }
    }
}
