using UnityEngine;

public class CoinAnimator : MonoBehaviour
{
    public Sprite[] sprite;  // Array of animation frames
    public float frameRate = 0.1f;  // Time between frames
    
    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float timer = 0f;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer = 0;
            spriteRenderer.sprite = sprite[currentFrame];
            currentFrame++;
            if (currentFrame == sprite.Length)
            {
                currentFrame = 0; // Loop back to the first frame
            }
        }
    }//end method
}//end class