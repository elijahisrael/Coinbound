using UnityEngine;

public class PlayerMovement : BasicMovement
{
    public float speed = 5f;
    public float jumpForce = 10f;

    // Coyote time (challenge slide)
    public float coyoteTime = 0.15f;   // NEW!
    float timeSinceGrounded = 0f;      // NEW!

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    bool jumpRequested = false;        // NEW!

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update coyote time tracker
        if (isGrounded())
            timeSinceGrounded = 0f;
        else
            timeSinceGrounded += Time.deltaTime;

        // Check for jump input every frame
        if (Input.GetKeyDown(KeyCode.UpArrow) && timeSinceGrounded <= coyoteTime)
        {
            jumpRequested = true;
        }

        // Variable jump height (release jump early cuts upward velocity)
        if (Input.GetKeyUp(KeyCode.UpArrow) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        // Sprite flip logic
        if (lastMovementDirection.x < 0)
            sr.flipX = true;
        else
            sr.flipX = false;
    }

    void FixedUpdate()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow))
            movement.x += speed * Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
            movement.x -= speed * Time.fixedDeltaTime;

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;
        } // end if

        bool isMoving = movement.magnitude > 0.01f;
        animator.SetBool("IsWalking", isMoving);

        setLastMovement(movement);

        // Convert movement-per-fixedframe into velocity
        rb.linearVelocity = new Vector2(movement.x / Time.fixedDeltaTime, rb.linearVelocity.y);
    }
}