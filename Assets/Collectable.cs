using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static int score = 0;
    public int value = 1;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            score += value;
            Debug.Log("Total coins: " + score);
            // Additional logic for when the player collects the item can be added here
            Destroy(gameObject);
        }
    }
}
