using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IJaSHealth : MonoBehaviour
{
    public string Identifier; //Is this the player or the slime

    [SerializeField] private float CurHealth; //Current health
    public float MaxHealth; //Max health

    public bool bHasCrown; //Does this entity currently have a crown?

    //List of effects to conduct once the designated turn rolls around
    public List<IJaSStatusEffect> statuses = new();


    [SerializeField] Image healthBar; //Image of the healthbar for UI purposes

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurHealth = MaxHealth;

        //Testing
        return;
        IJaSBurnEffect effect = new();

        AddStatus(effect);

        RunStatuses();
    }

    //Function to update the UI Healthbar
    void UpdateUI()
    {
        if (healthBar is null) return;

        healthBar.fillAmount = CurHealth / MaxHealth; //Divide and set the fill amount
    }

    public void Heal(int inHealing)
    {
        if (CurHealth + inHealing >= MaxHealth) CurHealth = MaxHealth;

        else CurHealth += inHealing;

        UpdateUI();
    }

    public void TakeDamage(int inDamage)
    {
        CurHealth -= inDamage;

        if (CurHealth <= 0) Die();

        else UpdateUI();
    }

    void Die()
    {
        Debug.Log($"Die {Identifier}");
    }

    //Function to multiply the specified targets health
    public void MultiplyHealth(int multiplier)
    {
        CurHealth *= multiplier;
        MaxHealth *= multiplier;

        UpdateUI();
    }

    public void AddStatus(IJaSStatusEffect inNewStatus)
    {
        inNewStatus.target = this; //Assign the target to be this

        statuses.Add(inNewStatus); //Add the status
    }

    //Function to run the status functions per turn
    public void RunStatuses()
    {
        for (int status = 0; status < statuses.Count; status++)
        {
            statuses[status].StatusEffect();
        }
    }
}
