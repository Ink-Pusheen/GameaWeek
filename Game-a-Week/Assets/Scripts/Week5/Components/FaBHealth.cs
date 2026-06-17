using UnityEngine;

public class FaBHealth : MonoBehaviour
{
    [SerializeField] int curHealth;
    [SerializeField] int maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(int inDamageTaken)
    {
        curHealth -= inDamageTaken;

        //Kill object if health is below or equal to threshold
        if (curHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
