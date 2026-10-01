using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SnakeGame
{
    public sealed class GameScreen : MonoBehaviour
    {
        private SnakeModel model;
        private float elapsed;
        private const float StepSeconds = 0.15f;
        private Texture2D head, body, tail;
        private AudioSource sound;
        private AudioClip eatSound, loseSound;
        private static bool MenuAvailable => Application.CanStreamedLevelBeLoaded("Menu");

        private void Awake()
        {
            model = new SnakeModel();
            // Persona 2: estos nombres son el contrato de integracion.
            head = Resources.Load<Texture2D>("Sprites/Snake/snake_head");
            body = Resources.Load<Texture2D>("Sprites/Snake/snake_body");
            tail = Resources.Load<Texture2D>("Sprites/Snake/snake_tail");
            sound = gameObject.AddComponent<AudioSource>();
            sound.playOnAwake = false;
            sound.volume = 0.15f;
            eatSound = Tone(660, 0.08f);
            loseSound = Tone(180, 0.22f);
        }

        private void Update()
        {
            var keys = Keyboard.current;
            if (keys != null)
            {
                if (keys.escapeKey.wasPressedThisFrame && MenuAvailable) { SceneManager.LoadScene("Menu"); return; }
                if (keys.upArrowKey.wasPressedThisFrame || keys.wKey.wasPressedThisFrame) model.Turn(Vector2Int.up);
                else if (keys.downArrowKey.wasPressedThisFrame || keys.sKey.wasPressedThisFrame) model.Turn(Vector2Int.down);
                else if (keys.leftArrowKey.wasPressedThisFrame || keys.aKey.wasPressedThisFrame) model.Turn(Vector2Int.left);
                else if (keys.rightArrowKey.wasPressedThisFrame || keys.dKey.wasPressedThisFrame) model.Turn(Vector2Int.right);
            }
            if (model.IsOver) return;
            elapsed += Time.deltaTime;
            // Limita a un paso por frame para evitar saltos despues de un bloqueo.
            if (elapsed >= StepSeconds)
            {
                elapsed %= StepSeconds;
                if (model.Step()) sound.PlayOneShot(eatSound);
                if (model.IsOver && !model.Won) sound.PlayOneShot(loseSound);
            }
        }

        private void OnGUI()
        {
            var oldMatrix = GUI.matrix;
            float scale = Mathf.Min(Screen.width / 960f, Screen.height / 800f);
            GUI.matrix = Matrix4x4.TRS(
                new Vector3((Screen.width - 960 * scale) / 2, (Screen.height - 800 * scale) / 2, 0),
                Quaternion.identity, Vector3.one * scale);

            var label = new GUIStyle(GUI.skin.label) { fontSize = 24, alignment = TextAnchor.MiddleCenter };
            GUI.Label(new Rect(160, 20, 640, 40), "Puntos: " + model.Score, label);
            GUI.Label(new Rect(100, 755, 760, 35), MenuAvailable
                ? "Flechas / WASD: mover     Esc: menu"
                : "Flechas / WASD: mover     Menu pendiente de integrar", label);
            for (int y = 0; y < SnakeModel.Size; y++)
                for (int x = 0; x < SnakeModel.Size; x++)
                    Fill(Cell(new Vector2Int(x, y)), (x + y) % 2 == 0
                        ? new Color(0.10f, 0.16f, 0.12f) : new Color(0.12f, 0.19f, 0.14f));

            var food = Cell(model.Food);
            food = new Rect(food.x + 6, food.y + 6, 20, 20);
            Fill(food, new Color(0.94f, 0.29f, 0.24f));

            for (int i = model.Cells.Count - 1; i >= 0; i--)
            {
                var texture = i == 0 ? head : i == model.Cells.Count - 1 ? tail : body;
                var rect = Cell(model.Cells[i]);
                var facing = i == 0 ? model.Direction
                    : model.Cells[i - 1] - model.Cells[i];
                if (texture != null)
                {
                    var matrix = GUI.matrix;
                    float angle = facing == Vector2Int.up ? -90 : facing == Vector2Int.down ? 90
                        : facing == Vector2Int.left ? 180 : 0;
                    GUIUtility.RotateAroundPivot(angle, rect.center);
                    GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit, true);
                    GUI.matrix = matrix;
                }
                else
                {
                    Fill(new Rect(rect.x + 2, rect.y + 2, 28, 28),
                        i == 0 ? new Color(0.45f, 0.87f, 0.39f) : new Color(0.25f, 0.66f, 0.31f));
                    if (i == 0)
                    {
                        var matrix = GUI.matrix;
                        float angle = facing == Vector2Int.up ? -90 : facing == Vector2Int.down ? 90
                            : facing == Vector2Int.left ? 180 : 0;
                        GUIUtility.RotateAroundPivot(angle, rect.center);
                        Fill(new Rect(rect.x + 22, rect.y + 6, 4, 4), Color.black);
                        Fill(new Rect(rect.x + 22, rect.y + 22, 4, 4), Color.black);
                        GUI.matrix = matrix;
                    }
                }
            }
            if (model.IsOver)
            {
                Fill(new Rect(220, 290, 520, 220), new Color(0.04f, 0.07f, 0.05f, 0.97f));
                GUI.Label(new Rect(240, 315, 480, 45), model.Won ? "¡Ganaste!" : "Fin del juego", label);
                GUI.Label(new Rect(240, 360, 480, 35), "Puntos: " + model.Score, label);
                if (GUI.Button(new Rect(MenuAvailable ? 270 : 385, 425, 190, 50), "Reiniciar")) SceneManager.LoadScene("Game");
                if (MenuAvailable && GUI.Button(new Rect(500, 425, 190, 50), "Menu")) SceneManager.LoadScene("Menu");
            }
            GUI.matrix = oldMatrix;
        }

        private static Rect Cell(Vector2Int cell) => new Rect(160 + cell.x * 32, 100 + (19 - cell.y) * 32, 32, 32);

        private static void Fill(Rect rect, Color color)
        {
            Color previous = GUI.color;
            GUI.color = color;
            GUI.DrawTexture(rect, Texture2D.whiteTexture);
            GUI.color = previous;
        }

        private static AudioClip Tone(float frequency, float seconds)
        {
            const int sampleRate = 44100;
            var samples = new float[Mathf.RoundToInt(sampleRate * seconds)];
            for (int i = 0; i < samples.Length; i++)
                samples[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / sampleRate)
                    * Mathf.Sin(Mathf.PI * i / samples.Length);
            var clip = AudioClip.Create("Efecto", samples.Length, 1, sampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }

        private void OnDestroy()
        {
            Destroy(eatSound);
            Destroy(loseSound);
        }
    }
}
