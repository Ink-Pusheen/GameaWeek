using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SpaceshipUpgrade
{
    public TMP_Text costText;

    public Image[] upgradeBars;

    public TMP_Text upgradeVersion;
}

public class SpaceshipUpgradeManager : MonoBehaviour
{
    public static SpaceshipUpgradeManager instance;

    private AudioPlayer audPlayer;

    [Header("Variables")]

    public float currentCash;

    public float scoreToCash = 500; //500 points originally = 1 Cash

    [SerializeField] TMP_Text curCashText; //Display of cash

    [Header("Pricing")]

    public int[] blasterUpgradeCost; //Array for blaster upgrades
    public int[] thrusterUpgradeCost; //Array for thruster upgrades
    public int[] healthUpgradeCost; //Array for health upgrades

    [SerializeField] int curBlasterLevel; //Current level of the blaster
    [SerializeField] int curThrusterLevel; //Current level of the thrusters
    [SerializeField] int curHealthLevel; //Current level of the health

    [SerializeField] SpaceshipUpgrade blasterUpgrade;
    [SerializeField] SpaceshipUpgrade thrusterUpgrade;
    [SerializeField] SpaceshipUpgrade healthUpgrade;

    [Header("Attachments")]

    public BlasterSO[] blasterUpgrades;
    public BlasterSO chosenBlaster;

    public ThrusterSO[] thrusterUpgrades;
    public ThrusterSO chosenThruster;

    public ShipShellSO[] shellUpgrades;
    public ShipShellSO chosenShell;

    private void Awake()
    {
        instance = this; //Assign the instance, as this will only be applicable to this weeks game
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audPlayer = GetComponentInChildren<AudioPlayer>();
    }

    #region Cash Functions

    public void AddCash(float inAdded)
    {
        currentCash += inAdded;
    }

    public void DisplayCash()
    {
        curCashText.text = $"${currentCash}";
    }

    #endregion

    #region Upgrade Functions

    public void UpgradeBlasters()
    {
        if (curBlasterLevel == blasterUpgradeCost.Length) return; //Return if maxed

        if (currentCash < blasterUpgradeCost[curBlasterLevel])
        {
            if (audPlayer != null) audPlayer.PlayRandomSound(1); //Play error sound
            return; //Return if there is not enough funds
        }

        currentCash -= blasterUpgradeCost[curBlasterLevel]; //Subtract the upgrade cost

        //Apply the new upgrade to the manager
        chosenBlaster = blasterUpgrades[curBlasterLevel];

        curBlasterLevel++; //Increment the level

        //Update the cost text
        blasterUpgrade.costText.text = curBlasterLevel < blasterUpgradeCost.Length ? $"${blasterUpgradeCost[curBlasterLevel]}" : "Maxed Upgrade";

        //Display current upgrade and progression
        blasterUpgrade.upgradeVersion.text = chosenBlaster.name;

        for (int cell = 0; cell < blasterUpgradeCost.Length; cell++)
        {
            if (cell < curBlasterLevel) blasterUpgrade.upgradeBars[cell].color = Color.green;

            else blasterUpgrade.upgradeBars[cell].color = Color.white;
        }

        if (audPlayer != null) audPlayer.PlayRandomSound(0); //Play Purchase sound
        DisplayCash(); //Update the cash amount
    }

    public void UpgradeThrusters()
    {
        if(curThrusterLevel == thrusterUpgradeCost.Length) return; //Return if maxed

        if (currentCash < thrusterUpgradeCost[curThrusterLevel])
        {
            if (audPlayer != null) audPlayer.PlayRandomSound(1); //Play error sound
            return; //Return if there is not enough funds
        }

        currentCash -= thrusterUpgradeCost[curThrusterLevel]; //Subtract the upgrade cost

        //Apply the new ugprade to the manager
        chosenThruster = thrusterUpgrades[curThrusterLevel];

        curThrusterLevel++; //Increment the level

        //Update the cost text
        thrusterUpgrade.costText.text = curThrusterLevel < thrusterUpgradeCost.Length ? $"${thrusterUpgradeCost[curThrusterLevel]}" : "Maxed Upgrade";

        //Display current upgrade and progression
        thrusterUpgrade.upgradeVersion.text = chosenThruster.name;

        for (int cell = 0; cell < thrusterUpgradeCost.Length; cell++)
        {
            if (cell < curThrusterLevel) thrusterUpgrade.upgradeBars[cell].color = Color.green;

            else thrusterUpgrade.upgradeBars[cell].color = Color.white;
        }

        if (audPlayer != null) audPlayer.PlayRandomSound(0); //Play Purchase sound
        DisplayCash(); //Update the cash amount
    }

    public void UpgradeHealth()
    {
        if(curHealthLevel == healthUpgradeCost.Length) return; //Return if maxed

        if (currentCash < healthUpgradeCost[curHealthLevel])
        {
            if (audPlayer != null) audPlayer.PlayRandomSound(1); //Play error sound
            return; //Return if there is not enough funds
        }

        currentCash -= healthUpgradeCost[curHealthLevel]; //Subtract the upgrade cost

        //Apply the new upgrade to the manager
        chosenShell = shellUpgrades[curHealthLevel];

        curHealthLevel++; //Increment the level

        //Update the cost text
        healthUpgrade.costText.text = curHealthLevel < healthUpgradeCost.Length ? $"${healthUpgradeCost[curHealthLevel]}" : "Maxed Upgrade";

        //Display current upgrade and progression
        healthUpgrade.upgradeVersion.text = chosenShell.name;

        for (int cell = 0; cell < shellUpgrades.Length; cell++)
        {
            if(cell < curHealthLevel) healthUpgrade.upgradeBars[cell].color = Color.green;

            else healthUpgrade.upgradeBars[cell].color = Color.white;
        }

        if (audPlayer != null) audPlayer.PlayRandomSound(0); //Play Purchase sound
        DisplayCash(); //Update the cash amount
    }

    #endregion
}
