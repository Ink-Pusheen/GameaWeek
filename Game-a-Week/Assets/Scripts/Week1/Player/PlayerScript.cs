using System;
using System.Collections;
using TMPro;
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
    [SerializeField] ThrusterSO pThrusterSO;

    [SerializeField] ShipBlasters pBlasters;
    [SerializeField] BlasterSO pBlasterSO;

    [SerializeField] ShipHealth pHealth;
    [SerializeField] ShipShellSO pShipShellSO;

    [Header("Text Fields")]

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text healthText;

    private void Awake()
    {
        pMovement = GetComponent<PlayerMovement>();
        if (pMovement != null && pThrusterSO != null)
        {
            pMovement.activeThruster = pThrusterSO;
            pMovement.ApplyNewThruster();
        }

        pBlasters = GetComponent<ShipBlasters>();
        if (pBlasters != null && pBlasterSO != null)
        {
            pBlasters.activeBlaster = pBlasterSO;
            pBlasters.ApplyNewBlaster();
        }

        pHealth = GetComponent<ShipHealth>();
        if (pHealth != null && pShipShellSO != null)
        {
            pHealth.shipShell = pShipShellSO;
            pHealth.ApplyNewShell();
        }
    }

    public void Setup(BlasterSO inPBlaster, ThrusterSO inPThrusters, ShipShellSO inPShell)
    {
        pBlasterSO = inPBlaster;

        pThrusterSO = inPThrusters;

        pShipShellSO = inPShell;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pInput = GetComponent<PlayerInput>();

        if (pInput is null) throw new ArgumentException("Player input should not be null");

        movementHorizontal = pInput.actions.FindAction("Horizontal");
        movementVertical = pInput.actions.FindAction("Vertical");

        fire = pInput.actions.FindAction("Fire");

        SpaceshipRachetManager.instance.activePlayer = this.gameObject;
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
        position.y = 0;
        position.z = Mathf.Clamp(position.z, -14, 14);

        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyShip"))
        {
            //Activate temporary I frames
            GetComponent<Collider>().enabled = false;

            takenDamageCoroutine(6);

            EnemyShipBase collidedShip = collision.gameObject.GetComponent<EnemyShipBase>();
            if(collidedShip != null) pHealth.TakenDamage(collidedShip.contactDamage);
        }
    }

    public void takenDamageCoroutine(int inBlinks)
    {
        StartCoroutine(IFrameBlink(inBlinks));
    }

    IEnumerator IFrameBlink(int blinks)
    {
        MeshRenderer rend = GetComponent<MeshRenderer>();

        for (int blink = 0; blink < 6; blink++)
        {
            rend.enabled = false;

            yield return new WaitForSeconds(0.15f);

            rend.enabled = true;

            yield return new WaitForSeconds(0.05f);
        }

        GetComponent<Collider>().enabled = true;
    }

    //UI Functions

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void UpdateHealthText(float newHealth, float maxHealth)
    {
        healthText.text = $"HP {newHealth}/{maxHealth}";
    }
}
