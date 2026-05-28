using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Vector2 lastMovementDirection = Vector2.zero;
    public float groundCheckDistance = 0.1f; // NEW!


    public void setLastMovement(Vector2 movement)
    {
        if (movement.magnitude > 0.01f)
            lastMovementDirection = movement;
    }

    public bool isGrounded()
    {
        Collider2D col = GetComponent<Collider2D>();

        // Start the ray BELOW the collider, not AT the bottom edge
        Vector2 rayStart = new Vector2(transform.position.x, col.bounds.min.y - 0.01f);

        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, groundCheckDistance);
        bool grounded = hit.collider != null && hit.collider.gameObject != gameObject;

        // Draw the ray in Scene view for debugging
        Debug.DrawRay(rayStart, Vector2.down * groundCheckDistance, grounded ? Color.green : Color.red);

        return grounded;
    }
}