using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IJaSTypewriter : MonoBehaviour
{
    public static IJaSTypewriter Instance;

    [SerializeField] TMP_Text dialogue;

    public List<string> queuedDialogue;

    [SerializeField] bool bActionTaken; //To vary the next button in the typewriter panel
    [SerializeField] bool bStatusesRan; //Check for the statuses

    [SerializeField] GameObject continueButton; //The Continue Button

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void DisplayDialogue()
    {
        //Clear current text
        dialogue.text = "";

        if (bActionTaken && bStatusesRan && queuedDialogue.Count is 0) queuedDialogue.Add("No status effects to display");

        //Loop to display all lines
        for (int line = 0; line < queuedDialogue.Count; line++)
        {
            dialogue.text += queuedDialogue[line];

            if (line != queuedDialogue.Count - 1) dialogue.text += "\n"; //Line break
        }

        continueButton.SetActive(true); //Re-enable the button
    }

    public void ContinueButton()
    {
        continueButton.SetActive(false); //Disable the button in case of any delayed actions

        queuedDialogue.Clear(); //Clear the dialogue list

        if (!bActionTaken) //Step one that no action is taken yet
        {
            bActionTaken = !bActionTaken;

            IJaSAttackManager.instance.RandomAttack();
        }
        else if (bActionTaken && !bStatusesRan) //Step two, check for status effects
        {
            bStatusesRan = !bStatusesRan;

            IJaSAttackManager.instance.DisplayStatusEffects();
        }
        else if (bActionTaken && bStatusesRan) //Step three, reset
        {
            bActionTaken = !bActionTaken; //Reset both
            bStatusesRan = !bStatusesRan;

            SceneSwapper.instance.SwapState("PlayerChoices");
        }
    }

}
