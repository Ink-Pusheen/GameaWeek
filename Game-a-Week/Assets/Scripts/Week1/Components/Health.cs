using UnityEngine;

public class Health : MonoBehaviour
{

    [Header("Variables")]

    [SerializeField] private float curHealth;
    public float maxHealth;

    public GameObject deathParticles; //To display when the ship is destroyed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curHealth = maxHealth;
    }

    public void TakenDamage(float inDamage)
    {
        curHealth -= inDamage;

        if (curHealth <= 0)
        {
            Debug.Log("Death of " + gameObject.name);
        }
    }

    private void Death()
    {
        //Create particle effects
        if(deathParticles is not null) Instantiate(deathParticles, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
