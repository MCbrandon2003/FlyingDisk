using UnityEngine;

public class UserGUI : MonoBehaviour
{
    public GUIStyle textStyle; // 自定义的GUIStyle，用于设置字体颜色

    void OnGUI()
    {
        textStyle.normal.textColor = Color.black; // 将字体颜色设置为黑色
        GUI.Label(new Rect(10, 10, 200, 20), "Score: " + Controller.score, textStyle);

        if (Controller.gameover)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 10, 100, 20), "Game Over", textStyle);
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 10, 100, 20), "Final Score: " + Controller.score, textStyle);
        }
    }
}