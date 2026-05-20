using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenClickInteraction : MonoBehaviour
{
    [Header("Input")]
    PlayerInput pInput;

    InputAction click;

    [Header("Player")]

    [SerializeField] FloralMagicPlayerScript player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pInput = GetComponent<PlayerInput>();

        click = pInput.actions.FindAction("Click");

        player = GetComponent<FloralMagicPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (click.WasPressedThisFrame())
        {
            Vector3 mos_Pos = Mouse.current.position.ReadValue();
            Debug.Log("Click");
            //Ray newRay = new(newRayVector, Vector3.forward);
            Ray newRay = Camera.main.ScreenPointToRay((Vector3)Mouse.current.position.ReadValue());

            RaycastHit hit;
            //Debug.DrawRay(Camera.main.ScreenPointToRay(Mouse.current.position.ReadDefaultValue()), Vector3.forward * 99, Color.red);
            if (Physics.Raycast(newRay, out hit, 900))
            {
                ClickInteractable interactable = hit.collider.GetComponent<ClickInteractable>();
                Debug.Log(hit.collider.name);



                if (interactable != null) //Ensure it isn't null
                {
                    if (interactable == player.ReturnCurrentInteractable()) //If the same, disable the selection and set the current selection to null and return
                    {
                        player.ChangeSelectedInteractable(null);

                        return;
                    }

                    if (player.ReturnCurrentInteractable() == null) //Check if there is currently anything selected
                    {
                        player.ChangeSelectedInteractable(interactable);
                    }
                    else
                    {
                        interactable.Interaction(player.ReturnCurrentInteractable());
                    }
                }
                
            }
            else Debug.Log("Nothing");
        }
    }
}
