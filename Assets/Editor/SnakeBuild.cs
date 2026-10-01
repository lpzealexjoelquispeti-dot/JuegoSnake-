using System;
using System.IO;
using SnakeGame;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SnakeBuild
{
    public const string MenuScene = "Assets/Scenes/Menu.unity";
    public const string GameScene = "Assets/Scenes/Game.unity";

    [MenuItem("Snake/Preparar escenas (solo si faltan)")]
    public static void Setup()
    {
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
        Directory.CreateDirectory("Assets/Scenes");
        if (!File.Exists(GameScene)) CreateScene<GameScreen>(GameScene);
        // Persona A entrega Menu. La base funciona con Game mientras tanto.
        bool hasMenu = File.Exists(MenuScene);
        EditorBuildSettings.scenes = hasMenu
            ? new[] { new EditorBuildSettingsScene(MenuScene, true), new EditorBuildSettingsScene(GameScene, true) }
            : new[] { new EditorBuildSettingsScene(GameScene, true) };
        PlayerSettings.defaultScreenWidth = 960;
        PlayerSettings.defaultScreenHeight = 800;
        PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
        PlayerSettings.companyName = "Equipo Snake";
        PlayerSettings.productName = "Snake";
        AssetDatabase.SaveAssets();
        EditorSceneManager.OpenScene(hasMenu ? MenuScene : GameScene);
        Debug.Log(hasMenu ? "Snake: Menu y Game preparados." : "Snake: Game preparado; Menu pendiente del aporte A.");
    }

    private static void CreateScene<T>(string path) where T : MonoBehaviour
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var camera = new GameObject("Main Camera").AddComponent<Camera>();
        camera.tag = "MainCamera";
        camera.transform.position = new Vector3(0, 0, -10);
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0.04f, 0.07f, 0.05f);
        camera.orthographic = true;
        camera.gameObject.AddComponent<AudioListener>();
        new GameObject(typeof(T).Name).AddComponent<T>();
        EditorSceneManager.SaveScene(scene, path);
    }

    [MenuItem("Snake/Crear ejecutable Windows")]
    public static void Windows()
    {
        Setup();
        Directory.CreateDirectory("Builds/Windows");
        var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions {
            scenes = Array.ConvertAll(EditorBuildSettings.scenes, scene => scene.path),
            locationPathName = "Builds/Windows/Snake.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None
        });
        if (report.summary.result != BuildResult.Succeeded)
            throw new Exception("No se pudo crear Snake.exe: " + report.summary.result);
        Debug.Log("Snake: ejecutable creado en Builds/Windows/Snake.exe");
    }
}
