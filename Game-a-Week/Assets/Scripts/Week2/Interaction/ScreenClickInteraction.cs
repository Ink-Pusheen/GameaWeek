using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenClickInteraction : MonoBehaviour
{
    [Header("Input")]
    PlayerInput pInput;

    InputAction click;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pInput = GetComponent<PlayerInput>();

        click = pInput.actions.FindAction("Click");
    }

    // Update is called once per frame
    void Update()
    {
        if (click.WasPerformedThisFrame())
        {
            Ray newRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadDefaultValue());

            RaycastHit hit;

            if (Physics.Raycast(newRay, out hit, 900))
            {
                ClickInteractable interactable = hit.collider.GetComponent<ClickInteractable>();

                if (interactable != null)
                {
                    interactable.Interaction();
                }
            }
        }
    }
}
