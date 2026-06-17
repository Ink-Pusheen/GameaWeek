using UnityEngine;

public class BulletPack : MonoBehaviour
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
        if (collision.GetComponent<FaBPlayerController>())
        {
            FaBPlayerController player = collision.GetComponent<FaBPlayerController>();

            if (player.heldWeapon != null) player.heldWeapon.AddAmmo(12);

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Player?");
        }

        Debug.Log("Overlap");
    }
}
