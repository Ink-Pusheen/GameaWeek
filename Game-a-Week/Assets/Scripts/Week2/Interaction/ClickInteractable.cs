using System;
using System.Collections.Generic;
using UnityEngine;

public class ClickInteractable : MonoBehaviour
{
    public WhatCanInteractSO InfluencingInteractable; //What type of interactable is this?

    public List<WhatCanInteractSO> WhatCanInteract = new(); //List of any and all other Interactables that can be inflenced by this. [Essentially check the previously selected object can interact]

    public bool Grabbable; //Will this item when selected be picked up?

    public PlacementInteractable Placement; //Is this item currently placed somewhere?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Using what was previously selected for this interaction
    public virtual void Interaction(ClickInteractable inPreviousInteractable) { UpdatePlacement(); }

    public bool CanInteract(ClickInteractable inPreviousInteractable)
    {
        if (inPreviousInteractable is null) return false;

        if (WhatCanInteract.Contains(inPreviousInteractable.InfluencingInteractable))
        {
            //Debug.Log("This can be interacted with");
            return true;
        }
        else
        {
            //Debug.Log("This cannot be interacted with");
            return false;
        }
    }

    public bool CheckPlayerHandAvailability()
    {
        return FloralMagicPlayerScript.instance.ReturnCurrentInteractable() == null;
    }

    public void UpdatePlacement()
    {
        //Debug.Log("Placement Update");
        if (Placement != null)
        {
            Placement.RemoveItem();
            //Debug.Log("Updated");
            Placement = null;
        }
        else
        {
            //Debug.Log("Failed to Update");
        }
    }
}
