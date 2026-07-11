using UnityEngine;
using UnityEngine.InputSystem;

public class PCPlayerController : MonoBehaviour
{
    [Header("Controls")]

   [SerializeField] private PlayerInput pInput; //Source of the player input mapping

    private InputAction interact; //Input action to interact with the environment
    [SerializeField] GameObject InteractionPoint; //Gameobject the raycast will fire
    [SerializeField] float InteractionRange; //How far the interaction goes

    private InputAction dash; //Input action for the player to dash
    [SerializeField] float dashPower; //How much power to the dash
    [SerializeField] float dashDecayRate; //How fast player speed returns to normal

    private InputAction horizontal; //Input action for grabbing the horizontal input
    private InputAction vertical; //Input action for grabbing the vertical input

    [SerializeField] float MoveSpeed; //Movespeed of the player
    [SerializeField] float baseMoveSpeed; //Base speed of the player
    [SerializeField] GameObject RotateTowards; //Invisible guide to rotate the player
    [SerializeField] GameObject StartRotationPosition; //Where the guide will start

    [SerializeField] bool bDeactivateImmediately; //Does this player deactivate after initial setup?

    private Rigidbody rb; //Rigidbody

    [SerializeField] int playerIndex; //Index purposed of setting up the controls

    [Header("Holding Technicality")]

    public GameObject heldItem; //What is the currently held item?
    public GameObject holdItemSpot; //Where the item is being is held

    [SerializeField] GameObject leftHand; //Player left hand
    [SerializeField] GameObject rightHand; //Player right hand

    [SerializeField] GameObject leftHandOrigin; //Where the left hand usually floats
    [SerializeField] GameObject rightHandOrigin; //Where the right hand usually floats


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pInput = GetComponent<PlayerInput>(); //Get the player inputt
        SetupPInput(); //Testing

        rb = GetComponent<Rigidbody>();

        baseMoveSpeed = MoveSpeed; //Sets the initial movespeed

        //Add self to the Round Manager instance
        //PCRoundManager.instance.players.Add(this);

        //Deactivate the player input after everything is assigned
        pInput.enabled = false;

        //Deactivate the object if need be
        if (bDeactivateImmediately) gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(InteractionPoint.transform.position, InteractionPoint.transform.forward * InteractionRange, Color.red);

        if (!pInput.enabled) return;

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

        //Functionality of dash
        if(dash.WasPressedThisFrame())
        {
            MoveSpeed = dashPower; //Set movespeed to the dash power;

            rb.AddForce(transform.forward * dashPower, ForceMode.Impulse); //Add the force
        }

        //Once player dashes, this will have to be addressed
        if (MoveSpeed > baseMoveSpeed)
        {
            MoveSpeed -= dashDecayRate * Time.deltaTime; //Deteriorate the speed boost

            if (MoveSpeed <= baseMoveSpeed) MoveSpeed = baseMoveSpeed; //Reset the speed
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
        
        if (!horizontal.IsPressed() && !vertical.IsPressed() && MoveSpeed == baseMoveSpeed)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    void MovePlayer(float inHorizontal, float inVertical)
    {
        Vector3 MoveVector = new Vector3(inHorizontal, 0, inVertical).normalized * MoveSpeed;

        rb.AddForce(MoveVector * MoveSpeed, ForceMode.Force);

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, MoveSpeed);
        //rb.linearVelocity = MoveVector;
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

            PCItem itemInHand = heldItem.GetComponent<PCItem>(); //Get the component if it exists

            if (itemInHand is not null) //Ensure it isn't null
            {
                itemInHand.SetPlayerHandPosition(this); //Call to the new item to set the hand positions
            }
        }

        else if (heldItem == null) //If null, reset the player hand positions
        {
            ResetHandPositions();
        }
    }


    //Turn on the input
    public void EnableInput()
    {
        pInput.enabled = true;

        //SetupPInput(); //Setup the controls once this is set up
    }

    //Turn off the input
    public void DisableInput()
    {
        pInput.enabled = false;
    }

    //Setup player controls
    public void SetupPInput()
    {
        //pInput.defaultActionMap = $"Packin Crew P{playerIndex}";
        pInput.SwitchCurrentActionMap($"Packin Crew P{playerIndex}");

        if (pInput is not null)
        {
            //Get the input actions from the mapping
            interact = pInput.actions.FindAction("Interact");
            dash = pInput.actions.FindAction("Dash");

            horizontal = pInput.actions.FindAction("Horizontal");
            vertical = pInput.actions.FindAction("Vertical");
        }
    }


    //Hand Functions

    //Set the hand positions when an item is grabbed
    public void SetHandPositions(GameObject left, GameObject right)
    {
        leftHand.transform.position = left.transform.position;
        rightHand.transform.position = right.transform.position;
    }


    //Reset the hand positions
    public void ResetHandPositions()
    {
        leftHand.transform.position = leftHandOrigin.transform.position;
        rightHand.transform.position = rightHandOrigin.transform.position;
    }
}
