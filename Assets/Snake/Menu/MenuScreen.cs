using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakeGame
{
    // Persona 1: cambia solamente este archivo para personalizar el menu.
    public sealed class MenuScreen : MonoBehaviour
    {
        private void OnGUI()
        {
            var oldMatrix = GUI.matrix;
            float scale = Mathf.Min(Screen.width / 960f, Screen.height / 800f);
            GUI.matrix = Matrix4x4.TRS(
                new Vector3((Screen.width - 960 * scale) / 2, (Screen.height - 800 * scale) / 2, 0),
                Quaternion.identity, Vector3.one * scale);

            var title = new GUIStyle(GUI.skin.label)
            {
                fontSize = 64, alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(0.45f, 0.87f, 0.39f) }
            };
            GUI.Label(new Rect(180, 240, 600, 100), "SNAKE", title);
            var button = new GUIStyle(GUI.skin.button) { fontSize = 28 };
            if (GUI.Button(new Rect(330, 380, 300, 80), "Iniciar", button))
                SceneManager.LoadScene("Game");

            GUI.matrix = oldMatrix;
        }
    }
}
