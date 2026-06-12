using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CCPlayerController : MonoBehaviour
{
    [Header("Input and Movement")]

    PlayerInput pInput;

    InputAction hInput; //Horizontal input
    InputAction vInput; //Vertical input

    InputAction throwBox; //Throw box input

    private Rigidbody rb;

    [SerializeField] float movementSpeed;

    [Header("Boxes")]

    [SerializeField] GameObject heldBox; //The currently held box

    [SerializeField] GameObject holdPosition; //Position where boxes are held

    [SerializeField] GameObject throwDirection; //Arrow to indicate where a box is being thrown
    [SerializeField] float throwPower; //How strong the throw will be
    [SerializeField] float throwRotationSpeed; //Speed the throw rotation goes

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pInput = GetComponent<PlayerInput>();

        hInput = pInput.actions.FindAction("Horizontal");
        vInput = pInput.actions.FindAction("Vertical");

        throwBox = pInput.actions.FindAction("ThrowBox");

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (throwBox.WasPressedThisFrame() && heldBox != null)
        {
            //Display throw arrow
            throwDirection.SetActive(true);

            rb.isKinematic = true; //Temp kinematic
        }

        if (throwBox.IsPressed() && heldBox != null)
        {
            if(hInput.IsPressed()) RotateThrowDirection(hInput.ReadValue<float>() * throwRotationSpeed * Time.deltaTime);
        }

        if (throwBox.WasReleasedThisFrame() && heldBox != null)
        {
            //Throw the box in the designated direction
            Rigidbody boxRB = heldBox.GetComponent<Rigidbody>();

            boxRB.isKinematic = false;
            boxRB.AddForce(throwDirection.transform.forward * throwPower, ForceMode.Impulse);

            //Remove parent and reset to null
            heldBox.transform.SetParent(null);
            heldBox = null;

            //Disable the arrow and revert from kinematic
            throwDirection.transform.rotation = Quaternion.identity;
            throwDirection.SetActive(false);
            rb.isKinematic = false;
        }

        if ((hInput.IsPressed() || vInput.IsPressed()) && !throwBox.IsPressed())
        {
            MovePlayer(hInput.ReadValue<float>(), vInput.ReadValue<float>());
        }

        if((hInput.WasReleasedThisFrame() || vInput.WasReleasedThisFrame()) && !throwBox.IsPressed())
        {
            MovePlayer(hInput.ReadValue<float>(), vInput.ReadValue<float>());
        }
    }

    void MovePlayer(float horizontal, float vertical)
    {
        if (rb.isKinematic) return;

        rb.linearVelocity = new Vector3(horizontal * movementSpeed, 0, vertical * movementSpeed);
    }

    void RotateThrowDirection(float horizontal)
    {
        throwDirection.transform.Rotate(new Vector3(0, horizontal, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box") && heldBox == null)
        {
            heldBox = collision.gameObject;

            heldBox.transform.position = holdPosition.transform.position;
            heldBox.transform.SetParent(holdPosition.transform);

            heldBox.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    public void SwapControlState()
    {
        pInput.enabled = !pInput.enabled;
    }
}
