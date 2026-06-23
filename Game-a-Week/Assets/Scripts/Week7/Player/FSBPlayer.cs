using UnityEngine;
using UnityEngine.InputSystem;

public class FSBPlayer : MonoBehaviour
{
    [SerializeField] FSBInteractable heldInteractable; //Currently held interactable in hand

    public FSBInteractable ReturnHeldItem() { return heldInteractable; }

    public GameObject HoldSpot; //Where held items will be placed

    [Header("Input")]

    private PlayerInput pInput; //Action map

    private InputAction leftClick; //Left click input

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pInput = GetComponent<PlayerInput>();

        if (pInput is null) return;

        leftClick = pInput.actions.FindAction("LeftClick");
    }

    // Update is called once per frame
    void Update()
    {
        if (leftClick.WasPerformedThisFrame())
        {
            Vector3 mos_Pos = Mouse.current.position.ReadValue();
            Ray newRay = Camera.main.ScreenPointToRay((Vector3)Mouse.current.position.ReadValue());

            RaycastHit hit;

            if (Physics.Raycast(newRay, out hit, 900))
            {
                if (hit.collider.gameObject.GetComponent<FSBInteractable>())
                {
                    FSBInteractable hitObject = hit.collider.GetComponent<FSBInteractable>();

                    hitObject.OnClickInteraction(this);
                }
                else
                {
                    Debug.Log($"No interaction scipt detected on {hit.collider.name}");
                }
            }
        }
    }

    public void ChangeHeldInteractable(FSBInteractable inInteractable)
    {
        heldInteractable = inInteractable;
    }
}
