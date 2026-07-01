using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MMGameManager : MonoBehaviour
{

    [SerializeField] List<string> names = new(); //List of state names

    [SerializeField] List<string> roundList = new(); //List for the current active rotation

    [SerializeField] string neededInput; //What is the currently required state name

    [SerializeField] TMP_InputField inputField; //Player Input

    [SerializeField] float RemainingTime; //The current time
    [SerializeField] TMP_Text timerText; //Display of the timer

    [SerializeField] bool bRunTimer; //Should the timer be running?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roundList = CreateRandomizedList();

        neededInput = roundList[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!bRunTimer) return; //Don't run the timer

        RemainingTime -= Time.deltaTime;

        UpdateTimer();
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

        if (roundList.Count is 0) Debug.Log("Game Win");

        else neededInput = roundList[0];
    }


    public void OnInputTextBoxChanged()
    {
        inputField.text = inputField.text.ToUpper(); //Capitalize all characters

        if (inputField.text.Length is 1) inputField.text = inputField.text.Replace(" ", ""); //Replace starting spaces

        if (inputField.text == neededInput)
        {
            Debug.Log("Right");
            ProceedList(); //Proceed to next index
        }
    }

    void UpdateTimer()
    {
        int minutes = (int)RemainingTime / 60;

        float seconds = RemainingTime - (minutes * 60);

        if(seconds < 10 && minutes > 0) timerText.text = $"{minutes}:0{seconds.ToString("F2")}";

        else if(minutes > 0) timerText.text = $"{minutes}:{seconds.ToString("F2")}";

        else if(seconds < 10 && minutes is 0) timerText.text = $"0{seconds.ToString("F2")}";

        else timerText.text = $"{seconds.ToString("F2")}";

        if (minutes == 0) timerText.color = Color.red;

        else timerText.color = Color.black;

        if(seconds <= 0 && minutes is 0)
        {
            bRunTimer = false;

            timerText.text = "00.00";

            timerText.color = Color.red;
        }
    }
}
