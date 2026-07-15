using System.Collections;
using TMPro;
using UnityEngine;

public class LRRoundManager : MonoBehaviour
{
    [Header("Data Containers")]

    [SerializeField] string[] letters; //Array of letters

    [SerializeField] RandomizedLetter[] letterDisplays;

    [SerializeField] float slotDelaySpeed; //Speed in which rolls to the next slot rolls

    [Header("Text Fields")]

    [SerializeField] TMP_Text SentenceDisplay; //Display of the currently selected sentence


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("RandomizeLetters", 1); //Testing
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function to randomize the three slots
    void RandomizeLetters()
    {
        StartCoroutine("Slots");
    }

    //Slots
    IEnumerator Slots()
    {
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

        //Enable the player input

        Invoke("RandomizeLetters", 3);
    }
}
