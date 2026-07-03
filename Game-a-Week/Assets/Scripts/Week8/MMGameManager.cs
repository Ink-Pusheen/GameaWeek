using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

[Serializable]
class State
{
    public string name;

    public float imageWidth;

    public Sprite imageSprite;
}

public class MMGameManager : MonoBehaviour
{
    [Header("Hotkeys")]

    private PlayerInput pInput;

    private InputAction clearInput;

    [Header("Information Lists")]

    [SerializeField] List<State> States = new(); //List of states

    [SerializeField] List<State> roundStates = new();//List for the current active rotation

    [SerializeField] Image curState; //Display of the current state


    [Header("Player Input")]

    [SerializeField] string neededInput; //What is the currently required state name

    [SerializeField] TMP_InputField inputField; //Player Input


    [Header("Timer and Run State")]

    [SerializeField] float RemainingTime; //The current time
    [SerializeField] float ResetTime; //Time that will be defaulted to afterwards
    [SerializeField] TMP_Text timerText; //Display of the timer

    [SerializeField] bool bRunTimer; //Should the timer be running?
    [SerializeField] bool bInGame = false; //Is the player currently in a round [Default false]


    [Header("Text Fields")]

    [SerializeField] TMP_Text inputPlaceholderText; //Grey text that displays in the text box

    [SerializeField] TMP_Text remainingText; //How many inputs remain during the ronud

    [SerializeField] TMP_Text endText; //Text to display how the player did

    [SerializeField] TMP_Text hintText; //Text to display the state to the player, will also be used as the final reveal when the game is lost
    [SerializeField] Button hintButton; //The hint button


    private void Start()
    {
        pInput = GetComponent<PlayerInput>();

        if (pInput is null) return;

        clearInput = pInput.actions.FindAction("Clear");
    }

    // Update is called once per frame
    void Update()
    {
        if (!bRunTimer) return; //Don't run the timer

        RemainingTime -= Time.deltaTime;

        UpdateTimer();

        //Change the hint button state based on how much time the player has remaining
        if (RemainingTime > 15) hintButton.interactable = true;

        else hintButton.interactable = false;

        if (clearInput.WasPerformedThisFrame()) inputField.text = ""; //Clear the input field
    }

    public void StartGame()
    {
        if (bInGame) return; //Don't allow for the game to start twice

        SceneSwapper.instance.DisableSpecificState("Pre-Game");

        bInGame = !bInGame; //Swap state

        SetupRound(); //Setup the round and queue the timer
    }

    private void SetupRound()
    {
        inputPlaceholderText.text = ""; //Keep this empty while the game is going

        roundStates.Clear(); //Clear the prior list if any data still exists

        roundStates = CreateRandomizedList(); //Create the new lists

        remainingText.text = roundStates.Count.ToString(); //Initialize the remaining text

        neededInput = roundStates[0].name; //Assign the initial state

        StartCoroutine("CountDownTimer");
    }

    //Courotine for the timer and game start
    IEnumerator CountDownTimer()
    {
        //Timer countdown
        timerText.text = "3";

        yield return new WaitForSeconds(1f);

        timerText.text = "2";

        yield return new WaitForSeconds(1f);

        timerText.text = "1";

        yield return new WaitForSeconds(1f);

        SceneSwapper.instance.SwapState("Main-Game"); //Swap to the main game state

        bRunTimer = true; //Start the timer

        curState.enabled = true; //Enable the image
        curState.sprite = roundStates[0].imageSprite; //Display the next sprite
        curState.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, roundStates[0].imageWidth); //Adjust the width
    }

    List<State> CreateRandomizedList()
    {
        List<State> copyStates = new(States); //Create a duplicate of each list

        List<State> randomizedStates = new();

        for (int state = States.Count; state > 0; state--)
        {
            int selection = UnityEngine.Random.Range(0, copyStates.Count);

            randomizedStates.Add(copyStates[selection]);

            copyStates.RemoveAt(selection);
        }

        return randomizedStates;
    }

    void ProceedList()
    {
        roundStates.RemoveAt(0); //Remove the index

        remainingText.text = roundStates.Count.ToString(); //Update the remaining text

        inputField.text = ""; //Reset the input

        hintText.text = ""; //Reset the hint text

        if (roundStates.Count is 0)
        {
            bRunTimer = false; //Stop the timer
            curState.enabled = false; //Hide the sprite

            //Display the ending screen
            SceneSwapper.instance.SwapState("Post-Game");

            //endText.text = $"Congradulations, you named all the states within {ResetTime} seconds!";
            endText.text = $"Congradulations, you named all the states in {(ResetTime - RemainingTime).ToString("F2")} seconds! Starting at {ResetTime} seconds.";
            endText.color = Color.aquamarine;
        }
        
        else
        {
            neededInput = roundStates[0].name;
            curState.sprite = roundStates[0].imageSprite; //Display the next sprite
            curState.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, roundStates[0].imageWidth); //Adjust the width
        }
    }

    void UpdateTimer()
    {
        int minutes = (int)RemainingTime / 60; //Get the minutes

        float seconds = RemainingTime - (minutes * 60); //Get the remaining seconds

        if(seconds < 10 && minutes > 0) timerText.text = $"{minutes}:0{seconds.ToString("F2")}"; //Display if there are at least minutes remaining

        else if(minutes > 0) timerText.text = $"{minutes}:{seconds.ToString("F2")}"; //Display if there are minutes but less than 10 seconds

        else if(seconds < 10 && minutes is 0) timerText.text = $"0{seconds.ToString("F2")}"; //Display if there are no minutes and less than 10 seconds

        else timerText.text = $"{seconds.ToString("F2")}"; //Display if there are no minutes

        if (minutes == 0) timerText.color = Color.red; //Red text to make user panic

        else timerText.color = Color.black; //Else black text to keep em calm

        //Game over condition
        if(seconds <= 0 && minutes is 0 || RemainingTime < 0)
        {
            bRunTimer = false; //Disable the timer

            timerText.text = "00.00"; //Cap timer to not go negative

            timerText.color = Color.red; //Ensure it is red

            SceneSwapper.instance.SwapState("Post-Game"); //Display the end screen

            endText.text = $"Unfortunate, you ran out of time. You can always try again.";
            endText.color = Color.red;

            hintText.text = $"{roundStates[0].name}";
        }
    }

    //Function for whenever the player inputs anything into the text box
    public void OnInputTextBoxChanged()
    {
        if (!bRunTimer) //No input until the timer is tickin buddy
        {
            inputField.text = ""; //Clear the input
            return;
        }

        inputField.text = inputField.text.ToUpper(); //Capitalize all characters

        if (inputField.text.Length is 1) inputField.text = inputField.text.Replace(" ", ""); //Replace starting spaces, screw this certain thing that has to be patched out

        //If these are equal, proceed
        if (inputField.text == neededInput)
        {
            ProceedList(); //Proceed to next index
        }
    }

    //Function to forfeit the game
    public void Forfeit()
    {
        if (!bRunTimer) return; //No click without timer

        RemainingTime = -999; //Remove all time, womp womp
    }

    //Function to display a hint to the player [as long as they have enough time
    public void DisplayHint()
    {
        if (!bRunTimer) return; //I said no timer, no click

        if (hintText.text != "") return; //Don't allow action if it is already revealed

        hintText.text = $"{roundStates[0].name}";

        RemainingTime -= 15; //Remove 15 seconds from the timer
    }

    //Function to update the allocated amount of time the player has in their round
    public void OnTimeSelectionChanged(TMP_Dropdown inDropdown)
    {
        switch (inDropdown.value)
        {
            case 0: //Case of 5 minutes

                RemainingTime = 300;
                ResetTime = 300;

                break;

            case 1: //Case of 4 minutes

                RemainingTime = 240;
                ResetTime = 240;

                break;

            case 2: //Case of 3 minutes

                RemainingTime = 180;
                ResetTime = 180;

                break;

            case 3: //Case of 2 Minutes

                RemainingTime = 120;
                ResetTime = 120;

                break;

            case 4: //Case of 1 Minute

                RemainingTime = 60;
                ResetTime = 60;

                break;

            case 5: //Baby mode (1 hr)

                RemainingTime = 3600;
                ResetTime = 3600;

                break;
        }
    }

    //Function to reset the game
    public void ResetGame()
    {
        roundStates.Clear(); //Clear the round list to ensure nothing remains

        //Swap the scenes
        SceneSwapper.instance.DisableAllStates();
        SceneSwapper.instance.EnableSpecificState("Pre-Game");

        //Reset the text fields
        timerText.text = "*Pending Game Start*";
        timerText.color = Color.black;
        inputPlaceholderText.text = "PRESS TO PLAY";
        remainingText.text = "-";
        inputField.text = "";
        hintText.text = "";

        //Reset the display image
        curState.enabled = false; //Hide the sprite

        //Reset the timer
        RemainingTime = ResetTime;

        //Reset game state
        bInGame = false;
    }
}
