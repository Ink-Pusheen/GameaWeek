using UnityEngine;

public class PCConveyorSendout : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints; //Waypoints for the item to move towards

    [SerializeField] int pointIndex; //Index of where this item currently is

    [SerializeField] float movespeed; //How fast the object moves across the conveyor

    private bool bMarkedForDeletion; //Is this box marked for deletion?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length is 0) return;

        if (pointIndex == waypoints.Length)
        {
            bMarkedForDeletion = true;
            Destroy(gameObject); //Destroy this gameobject when reaching the end
        }

        if (bMarkedForDeletion) return; //Don't proceed if marked for deletion

        transform.position = Vector3.MoveTowards(transform.position, waypoints[pointIndex].transform.position, movespeed * Time.deltaTime);

        //If within distance, increment to move to the next point
        if (Vector3.Distance(transform.position, waypoints[pointIndex].transform.position) < 0.2f)
        {
            pointIndex++;
        }
    }

    //Function to set the path of the sent box
    public void SetWaypoints(GameObject[] inWayPoints)
    {
        waypoints = inWayPoints;
    }
}
