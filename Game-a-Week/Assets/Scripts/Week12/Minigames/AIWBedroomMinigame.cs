using UnityEngine;

public class AIWBedroomMinigame : MonoBehaviour
{
    [Header("Progress")]

    [SerializeField] float awakeAmount; //Float to display how awake the player is
    [SerializeField] float requiredAwakeAmount; //Awake amount needed to complete the minigame

    [SerializeField] float minIncrease; //Min random for when hitting the wakeup button
    [SerializeField] float maxIncrease; //Max random for when hitting the wakeup button

    [SerializeField] float[] spriteDisplayMilestones; //Float checks the indicate what stage of awakeness the player is in visually
    [SerializeField] int spriteStage; //Tracker for the current sprite

    [SerializeField] bool bRunTimer = false; //Should the decay be running?
    [SerializeField] float decayRate; //How fast the player would fall back asleep


    [Header("Button Moment")]

    [SerializeField] GameObject wakeupButton; //Reference to the wake up button

    private int positionClicks; //How many times has this button been pressed in this position?
    [SerializeField] private int clicksTillReposition = 4; //How many clicks until the button moves?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bRunTimer) return; //Don't run if this is false

        if (awakeAmount > 0) awakeAmount -= Time.deltaTime * decayRate; //If the awake amount is greater than 0, decay

        else awakeAmount = 0; //Default 0 else

        if (spriteStage == spriteDisplayMilestones.Length)
        {
            SwapTimerState(); //Stop the timer

            Debug.Log("Stage Win");

            Invoke("NextScene", 2); //Go to the next minigame after 2 seconds
        }

        //Change the sprite based on the value compared to milestone
        if (spriteStage != spriteDisplayMilestones.Length && awakeAmount > spriteDisplayMilestones[spriteStage])
        {
            spriteStage++; //Increase the stage
            decayRate += 0.45f; //Add to the decay rate

            clicksTillReposition--; //Decrement 1
            //Debug.Log("Increase"); //Test Debug
        }

        if (spriteStage >= 1 && awakeAmount < spriteDisplayMilestones[spriteStage - 1])
        {
            spriteStage--; //Decrease the stage
            decayRate -= 0.45f; //Remove from the decay rate

            clicksTillReposition++; //Increment 1
            //Debug.Log("Decrease"); //Test Debug

        }
    }


    //Public function to swap the timer state
    public void SwapTimerState()
    {
        bRunTimer = !bRunTimer;
    }


    //Public function for hitting the wake up button
    public void WakeUp()
    {
        if (!bRunTimer) return; //No more touchie when timer isn't running

        awakeAmount += Random.Range(minIncrease, maxIncrease); //Add to the wake amount

        positionClicks++; //Increment the amount of clicks

        //Check if the clicks are greater than the requirement, enact if so
        if (positionClicks >= clicksTillReposition)
        {
            RandomizeWakupPosition(); //Randomize the button position

            positionClicks = 0; //Reset the position clicks
        }
    }

    //Randomize the button position
    void RandomizeWakupPosition()
    {
        if (!bRunTimer) return; //No teleporting check

        wakeupButton.transform.localPosition = new Vector3(Random.Range(-900f, 900f), Random.Range(-450, 450), 0);
    }


    //Function to swap to the next minigame
    void NextScene()
    {
        SceneSwapper.instance.SwapState("Coffee");
    }
}
