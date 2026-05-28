using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.5f;
    public Vector3 offset = new Vector3(0, 0, -10);
   
    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target == null) return;

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
            transform.position = smoothedPosition;

        
    }
}
