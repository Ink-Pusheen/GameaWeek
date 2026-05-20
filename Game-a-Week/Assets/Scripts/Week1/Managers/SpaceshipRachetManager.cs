using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpaceshipRachetManager : MonoBehaviour
{
    public static SpaceshipRachetManager instance;

    [SerializeField] EnemyShipSpawner spawner;

    [Header("Instructions")]

    public GameObject directions;

    [Header("Player")]

    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] GameObject playerSpawnPosition;

    public GameObject activePlayer;

    [Header("Round attributes")]

    public int scoreGained;

    public bool bGameStarted;

    [SerializeField] TMP_Text finalScore; //Display of the final score after the game is over
    [SerializeField] TMP_Text cashEarnedText; //Display of how much cash the player earned this round

    [SerializeField] TMP_Text winFinalScore; //Same thing, but for win screen
    [SerializeField] TMP_Text winCashEarned;

    [Header("Active Ships")]

    public List<GameObject> activeEnemyShips = new();

    private void Awake()
    {
        instance = this; //Assign the instance, as this will only be applicable to this weeks game
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Game Functions

    public void StartGame()
    {
        //Reset score
        scoreGained = 0;

        //Indicate game started
        bGameStarted = true;

        //Spawn Directions
        Instantiate(directions, directions.transform.position, directions.transform.rotation);

        //Spawn the player
        GameObject player = PlayerPrefab;

        BlasterSO appliedBlaster = SpaceshipUpgradeManager.instance.chosenBlaster;
        ThrusterSO appliedThruster = SpaceshipUpgradeManager.instance.chosenThruster;
        ShipShellSO appliedShell = SpaceshipUpgradeManager.instance.chosenShell;

        player.GetComponent<PlayerScript>().Setup(appliedBlaster, appliedThruster, appliedShell);
        
        Instantiate(player, playerSpawnPosition.transform.position, Quaternion.identity);

        //Initiate the spawner
        spawner.StartSpawner();
    }

    public void GameWin()
    {
        //Stop the spawner and remove all active ships
        spawner.StopSpawner();

        foreach (var Ship in activeEnemyShips)
        {
            Ship.GetComponent<EnemyShipBase>().ForceDestroy();
        }

        activePlayer.GetComponent<PlayerScript>().pInput.enabled = false;

        //Swap the scene and unlock the mouse TODO
        SceneSwapper.instance.EnableSpecificState("GameWin");

        CalculateWinOutcome();
    }

    public void GameOver()
    {
        //Stop the spawner and force destruct all ships
        spawner.StopSpawner();

        foreach (var Ship in activeEnemyShips)
        {
            Ship.GetComponent<EnemyShipBase>().ForceDestroy();
        }

        //Indicate game over
        bGameStarted = false;

        activePlayer = null;

        //Display the game over screen
        SceneSwapper.instance.EnableSpecificState("GameOver");

        //Calculate final score
        CalculateLossOutcome();
    }

    public void CleanRemnants()
    {
        if (activePlayer != null)
        {
            Destroy(activePlayer);
            activePlayer = null;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); //Ensure in editing

        Application.Quit();
    }

    #endregion

    #region Score Functions

    //Gained score for the round
    public void GainScore(int inScoreGained)
    {
        if (!bGameStarted) return; //No points if the game is over or not running

        scoreGained += inScoreGained;

        activePlayer.GetComponent<PlayerScript>().UpdateScoreText(scoreGained);
    }

    //Calculate what was earned to a 2 decimal value
    public void CalculateWinOutcome()
    {
        float cashEarned = scoreGained / SpaceshipUpgradeManager.instance.scoreToCash;

        float totalCalculatedCash = Mathf.Round(cashEarned * 100f) / 100f;

        SpaceshipUpgradeManager.instance.AddCash(totalCalculatedCash);

        //Display this on the game over screen
        winFinalScore.text = scoreGained.ToString();

        winCashEarned.text = "$" + totalCalculatedCash.ToString();
    }

    public void CalculateLossOutcome()
    {
        float cashEarned = scoreGained / SpaceshipUpgradeManager.instance.scoreToCash;

        float totalCalculatedCash = Mathf.Round(cashEarned * 100f) / 100f;

        SpaceshipUpgradeManager.instance.AddCash(totalCalculatedCash);

        //Display this on the game over screen
        finalScore.text = scoreGained.ToString();

        cashEarnedText.text = "$" + totalCalculatedCash.ToString();
    }

    #endregion Score Functions
}
