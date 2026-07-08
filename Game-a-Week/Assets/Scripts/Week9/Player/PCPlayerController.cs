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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
