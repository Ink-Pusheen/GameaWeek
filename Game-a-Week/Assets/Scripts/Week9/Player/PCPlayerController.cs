using UnityEngine;
using UnityEngine.InputSystem;

public class PCPlayerController : MonoBehaviour
{
    [Header("Controls")]

    private PlayerInput pInput; //Source of the player input mapping

    private InputAction interact; //Input action to interact with the environment
    private InputAction dash; //Input action for the player to dash

    private InputAction horizontal; //Input action for grabbing the horizontal input
    private InputAction vertical; //Input action for grabbing the vertical input

    [SerializeField] float MoveSpeed; //Movespeed of the player
    [SerializeField] GameObject RotateTowards; //Invisible guide to rotate the player
    [SerializeField] GameObject StartRotationPosition; //Where the guide will start

    private Rigidbody rb; //Rigidbody

    [Header("Holding Technicality")]

    public GameObject heldItem; //What is the currently held item?
    public GameObject holdItemSpot; //Where the item is being is held


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pInput = GetComponent<PlayerInput>(); //Get the player input

        if(pInput is not null)
        {
            //Get the input actions from the mapping
            interact = pInput.actions.FindAction("Interact");
            dash = pInput.actions.FindAction("Dash");

            horizontal = pInput.actions.FindAction("Horizontal");
            vertical = pInput.actions.FindAction("Vertical");
        }

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Functionality of getting the interaction
        if(interact.WasPressedThisFrame())
        {
            Debug.Log("Interaction");
        }

        //Functionality of movement
        if(horizontal.IsPressed() || vertical.IsPressed())
        {
            float horiz = horizontal.ReadValue<float>();
            float vert = vertical.ReadValue<float>();

            rb.linearVelocity = new Vector3(horiz * MoveSpeed, 0, vert * MoveSpeed);

            UpdateRotation(horiz, vert); //Update the user rotation
        }

        if(horizontal.WasReleasedThisFrame() || vertical.WasReleasedThisFrame())
        {
            rb.linearVelocity = new Vector3(horizontal.ReadValue<float>() * MoveSpeed, 0, vertical.ReadValue<float>() * MoveSpeed);
        }
    }

    void UpdateRotation(float horizontal, float vertical)
    {
        Vector3 Offset = StartRotationPosition.transform.position; //Set the initial position

        if (horizontal != 0) Offset += new Vector3(horizontal, 0, 0);

        if (vertical != 0) Offset += new Vector3(0, 0, vertical);

        RotateTowards.transform.position = Offset;

        Vector3 lookPosition = RotateTowards.transform.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(lookPosition);
        lookRotation.x = 0;
        lookRotation.z = 0;

        transform.rotation = lookRotation;
    }

}
