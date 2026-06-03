using UnityEngine;

public class SFCheckpointManager : MonoBehaviour
{
    public static SFCheckpointManager instance;

    public GameObject respawnPoint; //Where the player will respawn

    public GameObject player; //Player reference

    [SerializeField] AudioPlayer audPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        audPlayer = GetComponentInChildren<AudioPlayer>();
    }

    public void RespawnPlayer()
    {
        player.transform.position = respawnPoint.transform.position;

        //Play death sound
        audPlayer.PlayRandomSound(0);
    }

    public void NextScreen()
    {
        player.transform.position = respawnPoint.transform.position;

        audPlayer.PlayRandomSound(1);
    }
}
