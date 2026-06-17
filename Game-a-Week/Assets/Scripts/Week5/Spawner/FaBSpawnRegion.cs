using UnityEngine;

public class FaBSpawnRegion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Remove this spawn region if the player is in it
        if (collision.GetComponent<FaBPlayerController>()) FaBEnemySpawner.Instance.spawnRegions.Remove(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Add this spawn region if the player is leaving it
        if (collision.GetComponent<FaBPlayerController>()) FaBEnemySpawner.Instance.spawnRegions.Add(this);
    }
}
