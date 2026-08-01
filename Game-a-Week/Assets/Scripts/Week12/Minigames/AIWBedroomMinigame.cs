using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] float totalTimeTaken = 0; //How much time the player taken to wake up


    [Header("Button Moment")]

    [SerializeField] GameObject wakeupButton; //Reference to the wake up button

    private int positionClicks; //How many times has this button been pressed in this position?
    [SerializeField] private int clicksTillReposition = 4; //How many clicks until the button moves?


    [Header("Visuals")]

    [SerializeField] Image displayImage; //Image to display the current state the player is in
    [SerializeField] Sprite[] displaySprites; //Sprites to change between

    [SerializeField] GameObject tooltip; //Tool tip to let the player what to know

    [SerializeField] GameObject endScreen; //Display of the end screen
    [SerializeField] TMP_Text finalScore; //Display of the total time taken

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bRunTimer) return; //Don't run if this is false

        totalTimeTaken += Time.deltaTime;

        if (awakeAmount > 0) awakeAmount -= Time.deltaTime * decayRate; //If the awake amount is greater than 0, decay

        else awakeAmount = 0; //Default 0 else

        if (spriteStage == spriteDisplayMilestones.Length)
        {
            SwapTimerState(); //Stop the timer

            Debug.Log("Stage Win");

            //Invoke("NextScene", 2); //Go to the next minigame after 2 seconds

            //Deactivate the clock wakeup
            wakeupButton.SetActive(false);

            //Swap the visual
            displayImage.sprite = displaySprites[displaySprites.Length - 1];

            //Display ending screen
            endScreen.SetActive(true);

            finalScore.text = $"{totalTimeTaken.ToString("F2")}";
        }

        //Change the sprite based on the value compared to milestone
        if (spriteStage != spriteDisplayMilestones.Length && awakeAmount > spriteDisplayMilestones[spriteStage])
        {
            spriteStage++; //Increase the stage
            decayRate += 0.45f; //Add to the decay rate

            clicksTillReposition--; //Decrement 1

            if (spriteStage < displaySprites.Length - 1) displayImage.sprite = displaySprites[spriteStage]; //Display the new sprite
            //Debug.Log("Increase"); //Test Debug
        }

        if (spriteStage >= 1 && awakeAmount < spriteDisplayMilestones[spriteStage - 1])
        {
            spriteStage--; //Decrease the stage
            decayRate -= 0.45f; //Remove from the decay rate

            clicksTillReposition++; //Increment 1

            displayImage.sprite = displaySprites[spriteStage]; //Display the new sprite
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

        if(tooltip.activeInHierarchy) tooltip.SetActive(false); //Deactivate the tooltip accordingly

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
