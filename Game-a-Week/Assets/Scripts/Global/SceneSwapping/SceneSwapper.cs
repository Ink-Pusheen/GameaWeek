using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameState
{
    public string name;

    public GameObject gameState;
}

public class SceneSwapper : MonoBehaviour
{
    public static SceneSwapper instance;

    [Header("States")]

    public GameState[] states; //Array of all the states in the specified weekly game

    public string startState; //What state should display first?

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableAllStates();

        EnableSpecificState(startState);
    }

    public virtual void OpenedStateFunction(string openedState)
    {
        //Any states that need something to run when opened
        if (openedState == "UpgradeMenu") SpaceshipUpgradeManager.instance.DisplayCash();
    }

    #region Scene Swapping

    public void DisableAllStates()
    {
        for (int state = 0; state < states.Length; state++)
        {
            states[state].gameState.SetActive(false);
        }
    }

    public void EnableSpecificState(string inStateToEnable)
    {
        for (int state = 0; state < states.Length; state++)
        {
            if (states[state].name == inStateToEnable) states[state].gameState.SetActive(true);
        }

        //Temp
        OpenedStateFunction(inStateToEnable);
    }

    public void DisableSpecificState(string inStateToDisable)
    {
        for (int state = 0; state < states.Length; state++)
        {
            if (states[state].name == inStateToDisable) states[state].gameState.SetActive(false);
        }
    }

    public void SwapState(string inNewState)
    {
        for(int state = 0; state < states.Length; state++)
        {
            if (states[state].name == inNewState) states[state].gameState.SetActive(true);

            else states[state].gameState.SetActive(false);
        }

        //Temp
        OpenedStateFunction(inNewState);
    }

    #endregion

    #region Level Swapping [Unity's Scene Swap]

    public void LoadUnityScene(string inNewState)
    {
        SceneManager.LoadScene(inNewState);
    }

    #endregion Level Swapping [Unity's Scene Swap]

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
