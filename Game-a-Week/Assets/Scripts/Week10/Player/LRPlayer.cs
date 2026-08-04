using UnityEngine;
using UnityEngine.InputSystem;

public class LRPlayer : MonoBehaviour
{
    [Header("Controls")]

    private PlayerInput pInput;

    private InputAction anyKey; //Literally input for any key

    [SerializeField] float holdDuration; //How long has this key been held?


    private void Awake()
    {
        pInput = GetComponent<PlayerInput>(); //Get the player input component

        anyKey = pInput.actions.FindAction("AnyKey"); //Set the action
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Add to the held time while held down
        if (anyKey.IsPressed())
        {
            holdDuration += Time.deltaTime;
        }

        //Compare the time for the appropriate actions
        if (anyKey.WasReleasedThisFrame())
        {
            //Do things
            if(holdDuration < 1) LRRoundManager.instance.CommitPlayerAction(); //Player actions

            //Reset the input time
            holdDuration = 0;
        }


        //If at any time this threshhold is met, close the game
        if (holdDuration >= 2)
        {
            Debug.Log("Quit");
            //Application.Quit();
            SceneSwapper.instance.LoadUnityScene("Main Menu"); //Return to the main menu
        }
    }
}
