using System.Collections.Generic;
using UnityEngine;

public class CCConveyorScript : MonoBehaviour
{
    [SerializeField] Vector3 pushDirection; //Where the box is being pushed

    List<Rigidbody> activeRigidbodies = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var rb in activeRigidbodies)
        {
            rb.linearVelocity = pushDirection;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb is null) return; //Return if it doesn't have a rigidbody

        activeRigidbodies.Add(rb); //Add the rigidbody to the list
        Debug.Log("Set direction added");
    }

    private void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb is null) return; //Return if it doesn't have a rigidbody

        activeRigidbodies.Remove(rb); //Remove the rigidbody from the list
        Debug.Log("Set direction removed");
    }
}
