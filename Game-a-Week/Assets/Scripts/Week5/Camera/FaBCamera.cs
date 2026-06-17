using UnityEngine;

public class FaBCamera : MonoBehaviour
{
    [SerializeField] GameObject objectToFollow;

    [SerializeField] Vector3 offsetPosition;

    [SerializeField] float camMovementSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offsetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Why does this error?
        //if (objectToFollow is null) return; //Return if the player doesn't exist

        //But this doesn't?
        if (objectToFollow == null) return; //Return if the player doesn exist

        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.transform.position + offsetPosition, camMovementSpeed); //Set the position as the player moves
    }
}
