using UnityEngine;

public class FaBBullet : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float bulletSpeed; //How fast the bullet will go

    [SerializeField] float bulletLifetime; //How long the bullet is active for

    [SerializeField] int bulletDamage; //How much damage is dealt

    public string firingUser;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null) rb.linearVelocity = transform.TransformDirection(new Vector2(bulletSpeed, 0));

        QueueDestroy(); //Queue bullet destruction to ensure a million bullets don't exist
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if colliding with player and the weapon said bullet fired from is the player and vice versa
        if (collision.collider.GetComponent<FaBPlayerController>() && firingUser == "Player"
            || firingUser == "Enemy" && collision.collider.GetComponent<FaBEnemy>()
            || collision.collider.GetComponent<FaBBullet>()) return; //Return if so

        if (collision.collider.GetComponent<FaBBullet>()) return; //Also return if colliding with another bullet

        //Check if item has health component
        FaBHealth health = collision.gameObject.GetComponent<FaBHealth>();

        //Do stuff if it does
        if (health != null)
        {
            health.TakeDamage(bulletDamage);
        }

        Destroy(gameObject);
    }

    //Function to delete the bullet after a set amount of time
    void QueueDestroy()
    {
        Destroy(gameObject, bulletLifetime);
    }
}
