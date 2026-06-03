using System;
using UnityEngine;

[Serializable]
public class LevelState
{
    public GameObject currentLevel;
    public GameObject nextLevel;

    public GameObject nextRespawn;
}

public class SFEndFlagManager : MonoBehaviour
{
    public LevelState levelState;

    public void GoToNextScreen()
    {
        levelState.currentLevel.SetActive(false);
        levelState.nextLevel.SetActive(true);

        SFCheckpointManager.instance.respawnPoint = levelState.nextRespawn;

        SFCheckpointManager.instance.NextScreen();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GoToNextScreen();
        }
    }
}
