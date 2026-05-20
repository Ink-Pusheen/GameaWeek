using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public ThrusterSO activeThruster; //Scriptable object for player sakes, can be applied to enemies too

    public float speed;

    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ApplyNewThruster()
    {
        speed = activeThruster.thrusterSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontalInput is 0 && verticalInput is 0) return;

        transform.Translate(new Vector3(horizontalInput * speed, 0, verticalInput * speed) * Time.deltaTime);
        //rb.AddForce(new Vector3(horizontalInput * speed, 0, verticalInput * speed) * Time.deltaTime, ForceMode.Force);
    }

    public void ChangeVelocity(float inHorizontal, float inVertical)
    {
        horizontalInput = inHorizontal;
        verticalInput = inVertical;

        if(horizontalInput is 0 && verticalInput is 0) rb.linearVelocity = Vector3.zero;
    }
}
