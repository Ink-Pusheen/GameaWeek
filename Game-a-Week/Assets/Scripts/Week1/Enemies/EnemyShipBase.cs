using UnityEngine;

public class EnemyShipBase : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] ShipHealth eHealth;
    [SerializeField] ShipBlasters eBlasters;

    [Header("Variables")]

    public int pointsGranted; //How many points will be added to the players score

    public float shipSpeed;

    public int contactDamage;

    private void Awake()
    {
        eHealth = GetComponent<ShipHealth>();

        eBlasters = GetComponent<ShipBlasters>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpaceshipRachetManager.instance.activeEnemyShips.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * shipSpeed * Time.deltaTime, Space.World);

        if (eBlasters is not null)
        {
            eBlasters.Fire();
        }
    }

    public void GrantPoints()
    {
        //Give points to the player
        SpaceshipRachetManager.instance.GainScore(pointsGranted);
    }

    private void OnDestroy()
    {
        //Remove this ship from the list
        SpaceshipRachetManager.instance.activeEnemyShips.Remove(gameObject);
    }

    public void ForceDestroy()
    {
        eHealth.Death();
    }
}
