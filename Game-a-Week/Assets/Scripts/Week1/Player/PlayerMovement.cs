using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVelocity(float inHorizontal, float inVertical)
    {
        rb.linearVelocity = new Vector3(inHorizontal * 5, 0, inVertical * 5);

        Debug.Log($"MOVEEE " + rb.linearVelocity);
    }
}
