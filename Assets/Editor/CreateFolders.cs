using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Directory = System.IO.Directory;

public class CreateFolders : EditorWindow
{
    private static string projectName = "!Project";

    [MenuItem("Assets/Create Default Folders")]
    private static void SetUpFolders()
    {
        var window = CreateInstance<CreateFolders>();
        window.position = new(Screen.width * 0.5f, Screen.height * 0.5f, 400, 150);
        window.ShowPopup();
    }

    private static void CreateAllFolders()
    {
        var folders = new List<string>
        {
            "Animations",
            "Audio",
            "Editor",
            "Materials",
            "Meshes",
            "Prefabs",
            "Scripts",
            "Scenes",
            "Shaders",
            "Textures",
            "UI"
        };

        foreach (var folder in folders)
        {
            if (!Directory.Exists($"Assets/{projectName}/{folder}"))
            {
                Directory.CreateDirectory($"Assets/{projectName}/{folder}");
            }
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Generate"))
        {
            CreateAllFolders();
            this.Close();
        }
    }
}