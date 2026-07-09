using TMPro;
using UnityEngine;

public class PCRoundManager : MonoBehaviour
{
    [SerializeField] float timeRemaining; //Float representation of time remaining

    [SerializeField] TMP_Text timerText; //Display of how much time is left

    public bool bRunTimer; //Should this timer be running?

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
        }
    }

    //Update the timer
    void UpdateTimer()
    {
        int minutes = (int)timeRemaining / 60; //Get the minutes

        int remainingSeconds = (int)timeRemaining - (minutes * 60); //Get the seconds

        //if (remainingSeconds > 10) timerText.text = $"{minutes}:{remainingSeconds.ToString("F0")}";

        //else timerText.text = $"{minutes}:0{remainingSeconds.ToString("F0")}";

        if (remainingSeconds < 10)
        {
            timerText.text = $"{minutes}:0{remainingSeconds}";
        }
        else timerText.text = $"{minutes}:{remainingSeconds}";
    }
}
