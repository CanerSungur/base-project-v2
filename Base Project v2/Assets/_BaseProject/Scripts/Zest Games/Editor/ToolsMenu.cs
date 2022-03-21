using UnityEngine;
using UnityEditor;
using System.IO;

namespace ZestGames.Editor
{
    public static class ToolsMenu
    {
        [MenuItem("Tools/File Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            Dir("_Project", "Animations", "Graphics", "Scenes", "Scripts", "Imports", "Prefabs");
            AssetDatabase.Refresh();
        }

        private static void Dir(string root, params string[] dir)
        {
            var fullPath = Path.Combine(Application.dataPath, root);
            foreach (var newDirectory in dir)
            {
                Directory.CreateDirectory(Path.Combine(fullPath, newDirectory));
            }
        }
    }
}
