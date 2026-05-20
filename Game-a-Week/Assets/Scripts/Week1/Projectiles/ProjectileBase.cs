using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public int damage;

    public float speed;

    public float lifeSpan;

    public string Target;

    public int durability; //How many bullets this can go through before diminishing


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        QueueDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( transform.TransformDirection(new Vector3(0, 0, speed) * Time.deltaTime), Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Target == "" || other.gameObject.CompareTag("Untagged")) return;

        if (other.gameObject.CompareTag(Target))
        {
            ShipHealth hitHealth = other.gameObject.GetComponent<ShipHealth>();

            if (hitHealth is null)
            {
                Destroy(gameObject);
                return;
            }

            PlayerScript player = other.GetComponent<PlayerScript>();

            if (player != null) player.takenDamageCoroutine(1);

            hitHealth.TakenDamage(damage);
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("SpaceshipProjectile"))
        {
            ProjectileBase otherProjectile = other.gameObject.GetComponent<ProjectileBase>();

            if (otherProjectile.Target == Target) return; //If it has the same target, ignore

            if (otherProjectile.damage > damage) Destroy(gameObject);

            if (otherProjectile.damage == damage)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }

            if (otherProjectile.damage < damage)
            {
                Destroy(other.gameObject);

                durability--;

                if (durability <= 0) Destroy(gameObject);
            }
        }
    }

    private void QueueDestroy()
    {
        Destroy(gameObject, lifeSpan);
    }
}
