using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger2 : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Only allow leaving after ALL goals in this scene are reached.
        if (!GoalTrigger.AllGoalsCompleted())
        {
            Debug.Log("[SceneLoadTrigger2] Hit all goals first!");
            return;
        }

        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogWarning("[SceneLoadTrigger2] sceneToLoad is empty.");
            return;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}