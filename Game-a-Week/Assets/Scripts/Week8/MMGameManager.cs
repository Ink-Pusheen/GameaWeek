using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MMGameManager : MonoBehaviour
{

    [SerializeField] List<string> names = new(); //List of state names

    [SerializeField] List<string> roundList = new(); //List for the current active rotation

    [SerializeField] string neededInput; //What is the currently required state name

    [SerializeField] TMP_InputField inputField; //Player Input

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roundList = CreateRandomizedList();

        neededInput = roundList[0];
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (inputField.text == neededInput)
        {
            Debug.Log("Right");
            ProceedList(); //Proceed to next index
        }
    }
}
