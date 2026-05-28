using UnityEngine;

public class LookaheadCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.5f;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float lookAheadDistance = 0.4f;
    public float lookAheadSpeed = 1f;

    Vector3 currentLookahead;
    BasicMovement move;

    void Awake()
    {
        if (target == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) target = p.transform;
        }
        if (target != null)
            move = target.GetComponent<BasicMovement>();
    }

    void FixedUpdate()
    {
        if (target == null) return;
        if (move == null)
        {
            move = target.GetComponent<BasicMovement>();
            if (move == null) return; 
        }

        Vector3 targetLookahead =
            new Vector3(move.lastMovementDirection.x, move.lastMovementDirection.y, 0) * lookAheadDistance;

        currentLookahead = Vector3.Lerp(currentLookahead, targetLookahead, lookAheadSpeed * Time.fixedDeltaTime);

        Vector3 desiredPosition = target.position + offset + currentLookahead;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}