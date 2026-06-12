using TMPro;
using UnityEngine;

public class CCScoreManager : MonoBehaviour
{
    public static CCScoreManager instance;

    [SerializeField] int curScore;

    [SerializeField] float comboScoreMultiplier;
    [SerializeField] float[] comboValues;
    [SerializeField] int comboLevel;

    [SerializeField] TMP_Text CurScoreText;
    [SerializeField] TMP_Text CurComboText;

    [SerializeField] int counterToComboImprovement;
    [SerializeField] int[] comboIncrements; //Increments where combo will proceed

    [SerializeField] bool bRunTimer; //Timer to decay combo
    [SerializeField] float ComboRunout; //Time before combo is dropped
    [SerializeField] float[] ComboRunoutIncrements; //Levels for combo to drop

    [SerializeField] GameObject GameUI; //Display of the game UI
    [SerializeField] GameObject EndOfGameUI; //Display of the end of game UI;

    [SerializeField] TMP_Text finalScoreText; //Display of the players final score

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bRunTimer) return;

        ComboRunout -= Time.deltaTime;

        if (ComboRunout <= 0)
        {
            bRunTimer = false;

            HurtCombo();
        }
    }

    public void GainPoints()
    {
        int gainedScore = (int)Mathf.Round(100 * comboScoreMultiplier);

        curScore += gainedScore;

        CurScoreText.text = $"Current Score: {curScore}";

        if(comboLevel < comboIncrements.Length - 1) counterToComboImprovement++;

        if (counterToComboImprovement >= comboIncrements[comboLevel])
        {
            UpgradeCombo();
        }

        if (!bRunTimer) bRunTimer = !bRunTimer; //Enable timer

        ComboRunout = ComboRunoutIncrements[comboLevel]; //7 Second runout time
    }

    public void HurtCombo()
    {
        //Debug.Log("Wrong, hurt combo"); //Testing

        comboScoreMultiplier = 1f;

        comboLevel = 0;

        counterToComboImprovement = 0;

        CurComboText.text = $"Combo Score Multiplier: x{comboScoreMultiplier}";

        CCBoxSpawner.instance.AlterSpawnTimer(comboLevel);
    }

    private void UpgradeCombo()
    {
        comboLevel++;

        comboScoreMultiplier = comboValues[comboLevel];

        counterToComboImprovement = 0;

        CurComboText.text = $"Combo Score Multiplier: x{comboScoreMultiplier}";

        CCBoxSpawner.instance.AlterSpawnTimer(comboLevel);
    }

    public void StartScoring()
    {
        GameUI.SetActive(true); //Enable in game UI
    }

    public void DisplayEndScreen()
    {
        GameUI.SetActive(false); //Disable midgame UI

        EndOfGameUI.SetActive(true); //Enable end of game UI

        finalScoreText.text = $"{curScore}";
    }
}
