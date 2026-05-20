using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public ShipShellSO shipShell;

    [SerializeField] AudioPlayer audPlayer;

    [SerializeField] PlayerScript playerScript;

    [Header("Variables")]

    [SerializeField] private float curHealth;
    public float maxHealth;

    public GameObject deathParticles; //To display when the ship is destroyed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (shipShell is null) curHealth = maxHealth;

        audPlayer = GetComponentInChildren<AudioPlayer>();

        playerScript = GetComponent<PlayerScript>();

        if (playerScript != null) playerScript.UpdateHealthText(curHealth, maxHealth);
    }

    public void TakenDamage(float inDamage)
    {
        curHealth -= inDamage;

        if (curHealth <= 0)
        {
            Debug.Log("Death of " + gameObject.name);

            EnemyShipBase enemShip = GetComponent<EnemyShipBase>();

            if (enemShip != null) enemShip.GrantPoints();

            Death();
        }
        else
        {
            if (audPlayer != null) audPlayer.PlayRandomSound(1); //Play random damaged sound

            if (playerScript != null) playerScript.UpdateHealthText(curHealth, maxHealth);
        }
    }

    public void Death()
    {
        //Create particle effects
        if(deathParticles != null) Instantiate(deathParticles, transform.position, Quaternion.identity);

        if (audPlayer != null) audPlayer.PlayRandomSound(2); //Play random death sound

        audPlayer.transform.SetParent(null);
        audPlayer.QueueDestroy();

        Destroy(gameObject);

        if(shipShell != null && SpaceshipRachetManager.instance != null) SpaceshipRachetManager.instance.GameOver();
    }

    public void ApplyNewShell()
    {
        maxHealth = shipShell.shellHealth;

        curHealth = maxHealth;
    }
}
