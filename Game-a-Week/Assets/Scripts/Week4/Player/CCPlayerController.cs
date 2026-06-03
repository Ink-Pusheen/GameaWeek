using UnityEngine;
using UnityEngine.InputSystem;

public class CCPlayerController : MonoBehaviour
{
    [Header("Input and Movement")]

    PlayerInput pInput;

    InputAction hInput; //Horizontal input
    InputAction vInput; //Vertical input

    private Rigidbody rb;

    [SerializeField] float movementSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pInput = GetComponent<PlayerInput>();

        hInput = pInput.actions.FindAction("Horizontal");
        vInput = pInput.actions.FindAction("Vertical");

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hInput.IsPressed() || vInput.IsPressed())
        {
            MovePlayer(hInput.ReadValue<float>(), vInput.ReadValue<float>());
        }

        if(hInput.WasReleasedThisFrame() || vInput.WasReleasedThisFrame())
        {
            MovePlayer(hInput.ReadValue<float>(), vInput.ReadValue<float>());
        }
    }

    void MovePlayer(float horizontal, float vertical)
    {
        rb.linearVelocity = new Vector3(horizontal * movementSpeed, 0, vertical * movementSpeed);
    }
}
