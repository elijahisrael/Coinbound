using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 48;
        style.normal.textColor = Color.white;

        float paddingW = Screen.width * 0.02f;
        float paddingH = Screen.width * 0.02f;

        GUI.Label(new Rect(paddingW + 10, paddingH + 10, 200, 30), "Coins: " + Collectable.score, style);
    }
}
