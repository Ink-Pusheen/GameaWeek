using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LRRoundManager : MonoBehaviour
{
    public static LRRoundManager instance;

    [Header("Data Containers")]

    [SerializeField] string[] letters; //Array of letters

    [SerializeField] RandomizedLetter[] letterDisplays;

    [SerializeField] float slotDelaySpeed; //Speed in which rolls to the next slot rolls

    [SerializeField] int currentSlotIndex = -1; //Index of the slot for highlight purposes [Default -1, reason, don't run certain code at a given time]

    [SerializeField] bool bGameStarted = false; //Has the game started?

    [SerializeField] bool bGameOver = false; //Is the game over?

    [Header("Text Fields")]

    [SerializeField] TMP_Text SentenceDisplay; //Display of the currently selected sentence

    [SerializeField] TMP_Text PlayerInput; //Display of the choices the player made

    [SerializeField] TMP_Text endScore; //Final score


    private void Awake()
    {
        instance = this; //Set the instance
    }

    //Public function for the player to do actions
    public void CommitPlayerAction()
    {
        if (!bGameStarted)
        {
            //Grab a random sentence
            SentenceDisplay.text = LRGameManager.instance.ReturnRandomSentence();

            RandomizeLetters(); //Randomize the slots

            bGameStarted = true; //Set the game start to started

            //Loop through the slots to display and necessary information
            foreach (var slot in letterDisplays)
            {
                slot.DisplayGameMessage();
            }

            return; //Don't run further code for now
        }

        if (bGameOver) //Check if the game is over
        {
            letterDisplays[2].GoldifyText(); //Goldify replay

            SceneManager.LoadScene("Letter Roulette"); //Reload the scene

            return;
        }

        if (currentSlotIndex is -1) return; //Don't do anything while this is -1

        else ProceedSlot(); //Else try to proceed the slot
    }

    //Function for the selected slot to move to the next as long as it isn't the reroll index
    public void ProceedSlot()
    {
        if (currentSlotIndex == letterDisplays.Length - 1) return; //Don't proceed if on the reroll slot

        letterDisplays[currentSlotIndex].ResetSlotFunctionality(); //Reset and stop the currently highlighted slot

        currentSlotIndex++; //Increment the slot

        letterDisplays[currentSlotIndex].StartSlotFunctionality(); //Start the next slots functionality
    }

    //Function for the slots to add a letter
    public void AddLetter(string inLetter)
    {
        char comparedChar = SentenceDisplay.text[PlayerInput.text.Length];

        string newString = ""; //Default

        if(char.IsUpper(comparedChar)) newString = PlayerInput.text + inLetter.ToUpper(); //Set the initial updated string to an uppercase

        else if(!char.IsUpper(comparedChar)) newString = PlayerInput.text + inLetter.ToLower(); //Set the initial updated string to a lowercase

        if (newString.Length < SentenceDisplay.text.Length && SentenceDisplay.text[newString.Length] == ' ') newString = newString + ' '; //Add the space if the next index is empty

        PlayerInput.text = newString; //Set the new string

        if (PlayerInput.text.Length == SentenceDisplay.text.Length)
        {
            Debug.Log("Compare input");
            CompareInput();
        }

        else Invoke("RandomizeLetters", 1.25f); //Delay before next rolling
    }

    //Function to randomize the three slots
    public void RandomizeLetters()
    {
        StartCoroutine("Slots");
    }

    //Slots
    IEnumerator Slots()
    {
        currentSlotIndex = -1; //Reset to -1 so no input goes through

        int rotations = Random.Range(5, 16); //Random rotation amount between 5 and 15
        for (int loops = 0; loops < rotations; loops++)
        {
            //Loop through the displays and assign random letters
            for (int slot = 0; slot < letterDisplays.Length; slot++)
            {
                int letterIndex = Random.Range(0, letters.Length); //Select a random letter from the list

                letterDisplays[slot].SetLetter(letters[letterIndex]); //Update the letter
            }

            yield return new WaitForSeconds(slotDelaySpeed);
        }

        //Initialize the first slot to have an action

        currentSlotIndex = 0; //Set the index

        letterDisplays[currentSlotIndex].StartSlotFunctionality();
    }


    //After round functions for accuracy
    void CompareInput()
    {
        float correctPercent = 0; //How many the player got right
        float compareValue = SentenceDisplay.text.Length; //Length of the sentence display for comparison
        Debug.Log(compareValue); //Test
        for (int index = 0; index < SentenceDisplay.text.Length; index++)
        {
            if (SentenceDisplay.text[index] == ' ')
            {
                compareValue--; //Decrement by one due space
                continue; //Continue
            }

            if (SentenceDisplay.text[index] == PlayerInput.text[index]) correctPercent++;
        }
        Debug.Log($"Correnct value {correctPercent}, Compare Value {compareValue}");
        Debug.Log("Divided: " + correctPercent / compareValue);

        endScore.text = $"{((correctPercent / compareValue) * 100).ToString("F2")}% \n {correctPercent}/{compareValue}"; //Display the final score

        //Display the end messages
        foreach (var slot in letterDisplays)
        {
            slot.DisplayEndMessage();
        }

        bGameOver = true; //Indicate game over
    }
}
