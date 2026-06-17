using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class FaBPlayerController : MonoBehaviour
{
    [Header("Input and Actions")]

    PlayerInput pInput;

    InputAction fire;
    InputAction reload;
    InputAction dropWeapon;

    InputAction horizontalInput;
    InputAction verticalInput;

    [SerializeField] float moveSpeed;

    Rigidbody2D rb;

    [Header("Rotation")]

    [SerializeField] GameObject armRotator;

    [SerializeField] SpriteRenderer pRenderer;
    [SerializeField] SpriteRenderer armRenderer;

    Camera cam;

    [Header("Firing")]

    public FaBWeapon heldWeapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        cam = Camera.main;

        pRenderer = GetComponent<SpriteRenderer>();

        pInput = GetComponent<PlayerInput>();

        fire = pInput.actions.FindAction("Fire");
        reload = pInput.actions.FindAction("Reload");
        dropWeapon = pInput.actions.FindAction("DropWeapon");

        horizontalInput = pInput.actions.FindAction("Horizontal");
        verticalInput = pInput.actions.FindAction("Vertical");

        heldWeapon = GetComponentInChildren<FaBWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pInput.enabled) return; //Return if the player controls aren't enabled

        //Run the fire function on the weapon
        if (fire.WasPerformedThisFrame() && heldWeapon != null)
        {
            //Debug.Log("Fire");
            heldWeapon.FireWeapon();
        }

        //Run the reload function on the weapon
        if (reload.WasPerformedThisFrame() && heldWeapon != null)
        {
            Debug.Log("Reload");
            heldWeapon.ReloadWeapon();
        }

        //Run the function to drop the currently held weapon if one exists
        if (dropWeapon.WasPerformedThisFrame() && heldWeapon != null)
        {
            Debug.Log("Drop weapon");

        }

        //Player movement
        if (horizontalInput.IsPressed() || verticalInput.IsPressed() || horizontalInput.WasReleasedThisFrame() || verticalInput.WasReleasedThisFrame())
        {
            UpdateMovement();
        }

        //Rotating the arm accordingly... the new input system is pain when it comes to this
        //Sincerely, translating it from new mouse to a camera w/ screen to world point
        //Like bruh. Nandeeeeeeeeeee.
        Vector2 mouse = Mouse.current.position.ReadValue();
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, Mathf.Abs(cam.transform.position.z)));
        mousePos.z = 0;
        Vector2 lookVector = mousePos - transform.position;

        float angle = Mathf.Atan2(lookVector.y, lookVector.x) * Mathf.Rad2Deg;
        armRotator.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void UpdateMovement()
    {
        float horizontal = horizontalInput.ReadValue<float>();
        float vertical = verticalInput.ReadValue<float>();

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        if (horizontal < 0)
        {
            pRenderer.flipX = true;
            //armRenderer.flipX = true;
        }

        else if (horizontal > 0)
        {
            pRenderer.flipX = false;
            //armRenderer.flipX = false;
        }
    }

    void DropWeapon()
    {

    }

    //Swap the state of player input
    public void SwapControlState()
    {
        pInput.enabled = !pInput.enabled;

        if (pInput.enabled && heldWeapon != null) heldWeapon.DisplayUI();

        else if (!pInput.enabled && heldWeapon != null) heldWeapon.HideUI();
    }

    private void OnDestroy()
    {
        Debug.Log("Player has died, display end of game screen");
        FaBGameManager.instance.GameOver();

        FaBEnemySpawner.Instance.StopSpawner();
    }
}
