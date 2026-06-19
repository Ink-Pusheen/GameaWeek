using UnityEngine;
using System;

public class IJaSMeteor : MonoBehaviour
{
    private IJaSHealth hitTarget;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb is null) throw new Exception("Where my Rigidbody");

        RandomDirection();
    }

    void RandomDirection()
    {
        if (transform.position.x > Vector3.zero.x) rb.linearVelocity = new Vector3(-4, -5, 0);

        else if(transform.position.x < Vector3.zero.x) rb.linearVelocity = new Vector3(4, -5, 0);

        else if(transform.position.x == Vector3.zero.x) rb.linearVelocity = new Vector3(UnityEngine.Random.Range(-4f, 4f), -5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitTarget = collision.gameObject.GetComponent<IJaSHealth>();

        if (hitTarget is not null)
        {
            hitTarget.TakeDamage(UnityEngine.Random.Range(15, 21)); //Take contact damage

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("EXPLOSION");

            //Create sphere effect

            Destroy(gameObject);
        }
    }
}
