using TMPro;
using UnityEngine;

public class RandomizedLetter : MonoBehaviour
{
    [SerializeField] TMP_Text letterDisplay;

    [SerializeField] string endText; //Text to display when the game ends

    [SerializeField] string startText; //Text to display when the game starts

    public bool bRandomize = true; //Should this slot be randomized?

    [SerializeField] float timerTillAction; //Time remaining before the action occurs
    [SerializeField] float resetTimerTime; //Time which the prior reset to

    [SerializeField] bool bRunActionTimer; //Does the action time need to be ran?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bRunActionTimer)
        {
            timerTillAction -= Time.deltaTime; //Subtract the time

            if (timerTillAction <= 0)
            {
                ConductInput(); //Conduct the input

                //Reset the slot
                ResetSlotFunctionality();
            }
        }
    }

    //Public function to set the letter of the display
    public void SetLetter(string inLetter)
    {
        if (!bRandomize) return;

        letterDisplay.text = inLetter;
    }

    void ConductInput()
    {
        if (bRandomize) //The letter will be added to the sentence
        {
            LRRoundManager.instance.AddLetter(letterDisplay.text);
        }
        else if (!bRandomize) //The slots will be rerolled
        {
            LRRoundManager.instance.RandomizeLetters();
        }
    }

    //Function to start the slots functionality
    public void StartSlotFunctionality()
    {
        bRunActionTimer = true; //Start the timer

        letterDisplay.color = Color.yellow; //Turn the text yellow to display the highlight
    }

    //Function to stop the functionality and reset the timer
    public void ResetSlotFunctionality()
    {
        bRunActionTimer = false; // Stop the timer

        timerTillAction = resetTimerTime; //reset the timer

        letterDisplay.color = Color.black; //Reset the text color to express it not highlighted
    }


    //Display mid game text
    public void DisplayGameMessage()
    {
        if (startText == "") return; //Don't effect if this is empty

        letterDisplay.text = startText;
    }


    //Reset the tile to the specified message
    public void DisplayEndMessage()
    {
        if (endText == "") return; //Don't effect if this is empty

        letterDisplay.text = endText; //Set the ending text
    }

    //Function to force goldify text
    public void GoldifyText()
    {
        letterDisplay.color = Color.gold;
    }


}
