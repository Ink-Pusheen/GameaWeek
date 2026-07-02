using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class MMGameManager : MonoBehaviour
{
    [Header("Information Lists")]

    [SerializeField] List<string> names = new(); //List of state names

    [SerializeField] List<string> roundList = new(); //List for the current active rotation


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


    // Update is called once per frame
    void Update()
    {
        if (!bRunTimer) return; //Don't run the timer

        RemainingTime -= Time.deltaTime;

        UpdateTimer();
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

        roundList.Clear(); //Clear the prior list if any data still exists

        roundList = CreateRandomizedList(); //Create the new list

        neededInput = roundList[0]; //Assign the initial state

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

        bRunTimer = true; //Start the timer
    }

    List<string> CreateRandomizedList()
    {
        List<string> copyString = new(names); //Create a duplicate

        List<string> randomizedString = new();

        for (int state = names.Count; state > 0; state--)
        {
            int selection = Random.Range(0, copyString.Count);

            randomizedString.Add(copyString[selection]);

            copyString.RemoveAt(selection);
        }

        return randomizedString;
    }

    void ProceedList()
    {
        roundList.RemoveAt(0);

        inputField.text = ""; //Reset the input

        if (roundList.Count is 0)
        {
            bRunTimer = false; //Stop the timer

            //Display the ending screen
            SceneSwapper.instance.EnableSpecificState("Post-Game");
        }

        else neededInput = roundList[0];
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
        if(seconds <= 0 && minutes is 0)
        {
            bRunTimer = false; //Disable the timer

            timerText.text = "00.00"; //Cap timer to not go negative

            timerText.color = Color.red; //Ensure it is red

            SceneSwapper.instance.EnableSpecificState("Post-Game"); //Display the end screen
        }
    }

    //Function for whenever the player inputs anything into the text box
    public void OnInputTextBoxChanged()
    {
        if (!bRunTimer) return; //No input until the timer is tickin buddy

        inputField.text = inputField.text.ToUpper(); //Capitalize all characters

        if (inputField.text.Length is 1) inputField.text = inputField.text.Replace(" ", ""); //Replace starting spaces, screw this certain thing that has to be patched out

        //If these are equal, proceed
        if (inputField.text == neededInput)
        {
            ProceedList(); //Proceed to next index
        }
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
        }
    }

    //Function to reset the game
    public void ResetGame()
    {
        roundList.Clear(); //Clear the round list to ensure nothing remains

        //Swap the scenes
        SceneSwapper.instance.DisableAllStates();
        SceneSwapper.instance.EnableSpecificState("Pre-Game");

        //Reset the text fields
        timerText.text = "*Pending Game Start*";
        inputPlaceholderText.text = "PRESS TO PLAY";

        //Reset the timer
    }
}
