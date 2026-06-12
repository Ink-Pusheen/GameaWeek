using TMPro;
using UnityEngine;

public class CCGameManager : MonoBehaviour
{
    public static CCGameManager instance;

    [SerializeField] CCPlayerController player;

    [SerializeField] GameObject startUI; //Display of the starting UI

    [SerializeField] float secondsRemaining = 180; //3 minute excursion
    [SerializeField] TMP_Text timeRemainingText;

    [SerializeField] float timeScalar = 1; //How fast to deteriorate the time
    [SerializeField] bool bRunTimer;
    [SerializeField] GameObject timerUI; //Object for the timer UI

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bRunTimer) return;

        secondsRemaining -= Time.deltaTime * timeScalar;

        int minutes = (int)Mathf.Floor(secondsRemaining / 60);
        int seconds = (int)(secondsRemaining - (minutes * 60));

        if (seconds < 10) timeRemainingText.text = $"Time Remaining {minutes}:0{seconds}";

        else timeRemainingText.text = $"Time Remaining {minutes}:{seconds}";

        if (secondsRemaining <= 0) //Stop the timer
        {
            bRunTimer = false;

            timeRemainingText.text = $"Time Remaining 0:00";

            //Stop player movement
            player.SwapControlState();

            //Stop all sorting and box spawning
            SorterManager.instance.StopSorting();
            CCBoxSpawner.instance.SwapSpawnerState();

            //Hide timer
            timerUI.SetActive(false);

            //Display game over screen
            CCScoreManager.instance.DisplayEndScreen();
        }
    }

    public void StartGame()
    {
        player.SwapControlState();

        CCBoxSpawner.instance.SwapSpawnerState();

        SorterManager.instance.StartSorter();

        CCScoreManager.instance.StartScoring();

        startUI.SetActive(false); //Hide start UI
        timerUI.SetActive(true); //Enable the timer

        bRunTimer = true;
    }
}
