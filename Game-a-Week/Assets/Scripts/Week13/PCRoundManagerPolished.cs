using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PCRoundManagerPolished : MonoBehaviour
{
    public static PCRoundManagerPolished instance;

    [SerializeField] float timeRemaining; //Float representation of time remaining

    [SerializeField] TMP_Text timerText; //Display of how much time is left

    public bool bRunTimer; //Should this timer be running?

    public List<PCPlayerController> players = new(); //List of active players

    [SerializeField] GameObject MainMenuUIToggle; //Gameobject reference to hide once the game starts
    [SerializeField] GameObject MidRoundUIToggle; //Gameobject to display during gameplay
    [SerializeField] GameObject EndGameUIToggle; //Gameobject reference to display after the game ends
    [SerializeField] TMP_Text EndGameScore; //Display of the final score


    [Header("Players")]

    [SerializeField] bool bTwoPlayer; //Should the game be two player?

    private void Awake()
    {
        instance = this; //Set the instance
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set the initial display
        int minutes = (int)timeRemaining / 60; //Get the minutes

        float remainingSeconds = timeRemaining - (minutes * 60); //Get the seconds

        if (remainingSeconds > 10) timerText.text = $"{minutes}:{remainingSeconds}";

        else timerText.text = $"{minutes}:0{remainingSeconds}";
    }

    // Update is called once per frame
    void Update()
    {
        if (bRunTimer)
        {
            timeRemaining -= Time.deltaTime;

            UpdateTimer();

            if (timeRemaining <= 0)
            {
                bRunTimer = false; //Stop the timer and hardline the timer to 0:00
                timerText.text = "0:00";

                PCOrderManagerPolished.Instance.bRunTimer = false; //Stop the ordering timer

                //Stop input from all players
                foreach (var player in players)
                {
                    player.DisableInput();
                }

                //Display the final screen
                MidRoundUIToggle.SetActive(false);
                EndGameUIToggle.SetActive(true);
                EndGameScore.text = $"Final Score: {PCOrderManagerPolished.Instance.CurrentScore()}"; //Display the final score
            }
        }
    }

    //Update the timer
    void UpdateTimer()
    {
        int minutes = (int)timeRemaining / 60; //Get the minutes

        int remainingSeconds = (int)timeRemaining - (minutes * 60); //Get the seconds

        if (remainingSeconds < 10)
        {
            timerText.text = $"{minutes}:0{remainingSeconds}";
        }
        else timerText.text = $"{minutes}:{remainingSeconds}";
    }


    //Function to start everything within the game
    public void StartGame()
    {
        //Swap the UI Menus
        MainMenuUIToggle.SetActive(false); //Disable front screen
        MidRoundUIToggle.SetActive(true); //Enable mid round UI

        PCOrderManagerPolished.Instance.StartGameFunctionality(); //Start the ordering manager functionality

        //Start the round timer
        bRunTimer = true;

        //Enable all player input based on bTwoPlayer bool

        if (bTwoPlayer)
        {
            foreach (var player in players)
            {
                player.EnableInput();
            }
        }
        else
        {
            players[0].EnableInput();
        }

    }

    //Function to toggle multiplayer
    public void ToggleMultiplayer(bool bSwapState)
    {
        if (bSwapState) bTwoPlayer = !bTwoPlayer; //Flip the state if input is true

        if (bTwoPlayer) //Case for two player
        {
            foreach (var player in players)
            {
                player.gameObject.SetActive(true);
            }
        }
        else //Case for not two player
        {
            players[0].gameObject.SetActive(true); //Enable player one
            players[1].gameObject.SetActive(false); //Disable player two
        }
    }
}
