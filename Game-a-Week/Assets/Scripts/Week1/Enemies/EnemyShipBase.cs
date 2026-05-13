using UnityEngine;

public class EnemyShipBase : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] ShipHealth eHealth;
    [SerializeField] ShipBlasters eBlasters;

    [Header("Variables")]

    public int pointsGranted; //How many points will be added to the players score

    private void Awake()
    {
        eHealth = GetComponent<ShipHealth>();

        eBlasters = GetComponent<ShipBlasters>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eBlasters is not null)
        {
            eBlasters.Fire();
        }
    }

    private void OnDestroy()
    {
        //Give points to the player (TODO)
    }
}
