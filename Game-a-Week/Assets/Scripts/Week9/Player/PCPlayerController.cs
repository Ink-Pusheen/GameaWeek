using UnityEngine;
using UnityEngine.InputSystem;

public class PCPlayerController : MonoBehaviour
{
    [Header("Controls")]

    private PlayerInput pInput; //Source of the player input mapping

    private InputAction interact; //Input action to interact with the environment
    [SerializeField] GameObject InteractionPoint; //Gameobject the raycast will fire
    [SerializeField] float InteractionRange; //How far the interaction goes

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

        if (pInput is not null)
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
        Debug.DrawRay(InteractionPoint.transform.position, InteractionPoint.transform.forward * InteractionRange, Color.red);

        //Functionality of getting the interaction
        if (interact.WasPressedThisFrame())
        {
            // Debug.Log("Interaction"); // Testing

            Ray outRay = new Ray();
            outRay.origin = InteractionPoint.transform.position;
            outRay.direction = InteractionPoint.transform.forward;

            RaycastHit hit;

            if (Physics.Raycast(outRay, out hit, InteractionRange))
            {
                //Debug.Log($"Hit: {hit.collider.name}");

                PCInteractable hitInteractable = hit.collider.GetComponent<PCInteractable>();

                if (hitInteractable is null) return; //Don do anything if this is null

                hitInteractable.OnInteract(this); //Run the interaction script on the object
            }
            else
            {
                Debug.Log("No Hit");
            }
        }

        //Functionality of movement
        if (horizontal.IsPressed() || vertical.IsPressed())
        {
            float horiz = horizontal.ReadValue<float>();
            float vert = vertical.ReadValue<float>();

            MovePlayer(horiz, vert); //Move the player

            UpdateRotation(horiz, vert); //Update the user rotation
        }

        if (horizontal.WasReleasedThisFrame() || vertical.WasReleasedThisFrame())
        {
            float horiz = horizontal.ReadValue<float>();
            float vert = vertical.ReadValue<float>();

            MovePlayer(horiz, vert); //Move the player
        }
    }

    void MovePlayer(float inHorizontal, float inVertical)
    {
        Vector3 MoveVector = new Vector3(inHorizontal, 0, inVertical).normalized * MoveSpeed;

        rb.linearVelocity = MoveVector;
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

    public void ChangeHeldItem(GameObject inNewItem)
    {
        heldItem = inNewItem;

        if (heldItem != null)
        {
            heldItem.transform.position = holdItemSpot.transform.position; //Set the position and parent
            heldItem.transform.rotation = holdItemSpot.transform.rotation;
            heldItem.transform.SetParent(holdItemSpot.transform);
        }
    }
}
