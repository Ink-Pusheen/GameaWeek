using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [Header("Input")]

    public PlayerInput pInput;

    private InputAction movementHorizontal;
    private InputAction movementVertical;

    private InputAction fire;

    [Header("Components")]

    [SerializeField] PlayerMovement pMovement;

    [SerializeField] ShipBlasters pBlasters;

    [SerializeField] ShipHealth pHealth;

    private void Awake()
    {
        pMovement = GetComponent<PlayerMovement>();

        pBlasters = GetComponent<ShipBlasters>();

        pHealth = GetComponent<ShipHealth>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pInput = GetComponent<PlayerInput>();

        if (pInput is null) throw new ArgumentException("Player input should not be null");

        movementHorizontal = pInput.actions.FindAction("Horizontal");
        movementVertical = pInput.actions.FindAction("Vertical");

        fire = pInput.actions.FindAction("Fire");

        Debug.Log($"{movementHorizontal} + {movementVertical} + {fire}"); //Check that it all exists
    }

    // Update is called once per frame
    void Update()
    {
        if (pMovement is null) return;

        if (movementHorizontal.IsPressed() || movementVertical.IsPressed())
        {
            pMovement.ChangeVelocity(movementHorizontal.ReadValue<float>(), movementVertical.ReadValue<float>());
        }

        if (movementHorizontal.WasReleasedThisFrame() || movementVertical.WasReleasedThisFrame())
        {
            pMovement.ChangeVelocity(movementHorizontal.ReadValue<float>(), movementVertical.ReadValue<float>());
        }

        if (fire.IsInProgress() && pBlasters is not null)
        {
            pBlasters.Fire();
        }
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;

        position.x = Mathf.Clamp(position.x, -26, 26);
        position.z = Mathf.Clamp(position.z, -14, 14);

        transform.position = position;
    }
}
