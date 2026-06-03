using UnityEngine;
using UnityEngine.InputSystem;

public class SFPlayer : MonoBehaviour
{
    [Header("Player Input System")]

    PlayerInput pInput;

    InputAction horizontal;
    InputAction fly;

    Rigidbody2D rb;

    [SerializeField] GameObject FloorCheck;
    [SerializeField] bool bIsGrounded;
    [SerializeField] LayerMask whatIsGround;


    [SerializeField] float moveSpeed;
    [SerializeField] float flySpeed;

    [SerializeField] float flyTime; //Max amount of flying time
    [SerializeField] float lastFlyInput;

    [Header("Visuals and Audio")]

    [SerializeField] SpriteRenderer pRender;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pInput = GetComponent<PlayerInput>();

        horizontal = pInput.actions.FindAction("Horizontal");
        fly = pInput.actions.FindAction("Fly");

        rb = GetComponent<Rigidbody2D>();
        pRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fly.IsPressed())
        {
            flyTime += Time.deltaTime;

            if (flyTime > lastFlyInput + 5)
            {
                Debug.Log("No more flying");
                return;
            }

            rb.AddForce(Vector2.up * flySpeed * Time.deltaTime, ForceMode2D.Force);
        }

        if (horizontal.IsPressed())
        {
            float input = horizontal.ReadValue<float>();

            rb.linearVelocityX = input * moveSpeed;

            if (input < 0) pRender.flipX = true;

            else if (input > 0) pRender.flipX = false;
        }

        if (IsGrounded() && !fly.IsPressed())
        {
            rb.linearVelocityY = 0;
            //Debug.Log("Grounded");
            lastFlyInput = Time.time; //Get the last time player was on the ground for both variables
            flyTime = Time.time;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(FloorCheck.transform.position, 0.025f, whatIsGround);
    }

}
