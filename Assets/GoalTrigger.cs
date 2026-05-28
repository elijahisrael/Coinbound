using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    public static int goalsReached = 0;
    public static bool userWon = false;
    public static int totalGoals = 0;

    private bool alreadyCounted = false;
    private static int guiOwnerId = 0;

    // Tracks which scene we initialized for, so Scene1/Scene2 don't share goal counts.
    private static int initializedBuildIndex = -999;

    private static bool IsFinalScene()
    {
        return SceneManager.GetActiveScene().name == "Final_Scene";
    }

    private static void EnsureSceneInit()
    {
        int idx = SceneManager.GetActiveScene().buildIndex;
        if (idx == initializedBuildIndex) return;

        initializedBuildIndex = idx;
        goalsReached = 0;
        userWon = false;
        guiOwnerId = 0;

        // Count goals in THIS scene, ignoring exit triggers to prevent deadlocks.
        int count = 0;
        GoalTrigger[] all = Object.FindObjectsOfType<GoalTrigger>(true);
        for (int i = 0; i < all.Length; i++)
        {
            if (all[i].GetComponent<SceneLoadTrigger>() != null) continue;
            if (all[i].GetComponent<SceneLoadTrigger2>() != null) continue;
            count++;
        }
        totalGoals = count;

        Debug.Log($"[GoalTrigger] Scene '{SceneManager.GetActiveScene().name}' totalGoals={totalGoals}");
    }

    public static bool AllGoalsCompleted()
    {
        EnsureSceneInit();
        return goalsReached >= totalGoals;
    }

    private void OnEnable()
    {
        EnsureSceneInit();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnsureSceneInit();
        if (!other.CompareTag("Player")) return;

        // Safety: if someone accidentally puts GoalTrigger on the exit trigger, ignore it.
        if (GetComponent<SceneLoadTrigger>() != null || GetComponent<SceneLoadTrigger2>() != null) return;

        if (alreadyCounted) return;
        alreadyCounted = true;

        Debug.Log($"Goal Reached! ({goalsReached + 1}/{totalGoals})");

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = Color.green;

        goalsReached++;

        if (AllGoalsCompleted())
        {
            Debug.Log("All goals reached!");

            // Only FINAL scene ends the game / shows win.
            if (IsFinalScene())
            {
                userWon = true;
                if (guiOwnerId == 0) guiOwnerId = GetInstanceID();

                PlayerMovement pm = other.GetComponent<PlayerMovement>();
                if (pm != null) pm.enabled = false;

                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.simulated = false;
                }
            }
        }
    }

    private void OnGUI()
    {
        if (!IsFinalScene()) return;
        if (!userWon) return;
        if (guiOwnerId != GetInstanceID()) return;

        GUIStyle style = new GUIStyle();
        style.fontSize = 48;
        style.normal.textColor = Color.yellow;

        GUI.Label(new Rect(10, 10, 600, 100), "You Win!", style);
    }
}