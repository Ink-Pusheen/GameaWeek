using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public int damage;

    public float speed;

    public float lifeSpan;

    public string Target;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        QueueDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( transform.TransformDirection(new Vector3(0, 0, speed) * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit? " + other.name);
        if (other.gameObject.CompareTag(Target))
        {
            ShipHealth hitHealth = other.gameObject.GetComponent<ShipHealth>();

            if (hitHealth is null)
            {
                Destroy(gameObject);
                return;
            }

            hitHealth.TakenDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Not Target: " + other.gameObject.tag);
        }
    }

    private void QueueDestroy()
    {
        Destroy(gameObject, lifeSpan);
    }
}
