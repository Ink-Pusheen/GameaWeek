using TMPro;
using UnityEngine;

public class FaBGameManager : MonoBehaviour
{
    public static FaBGameManager instance;

    [SerializeField] FaBPlayerController pController;

    [Header("Scoring")]

    [SerializeField] GameObject scoreUI;
    [SerializeField] TMP_Text scoreText;

    [SerializeField] TMP_Text finalScoreText;

    [SerializeField] int killScore;

    [Header("Menus")]

    [SerializeField] GameObject startMenu;

    [SerializeField] GameObject endScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        scoreUI.SetActive(true);

        pController.SwapControlState(); //Enable player controls

        FaBEnemySpawner.Instance.StartSpawner(); //Start the spawner
    }

    public void GameOver()
    {
        scoreUI.SetActive(false); //Disable main game UI

        endScreen.SetActive(true); //Enable end of game UI
        finalScoreText.text = killScore.ToString(); //Disable the final score
    }

    public void GainScore()
    {
        killScore++; //1 to the score

        scoreText.text = $"Kills: {killScore}";
    }
}
