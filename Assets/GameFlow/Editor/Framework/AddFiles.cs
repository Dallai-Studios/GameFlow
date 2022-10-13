using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GameFlow.App.Types.ScriptableObjects;
using GameFlow.Editors.AutoSave;
using UnityEditor;
using UnityEngine;

namespace GameFlow.Editors.Framework
{
    public class AddFiles : Editor
    {
        private static Dictionary<string, string> definitions = new();

        [MenuItem("Game Flow/Debug/Create Debug Config File")]
        public static void CreateGameDebugConfileAsset()
        {
            string file = "Game Debug Configurations";
            if (!ValidateUniqueFile(file)) return;
            string path = $"Assets/{file}.asset";
            CreateAsset<GameDebugConfig>(path);
            Debug.Log($"New Game Debug Configurations created at {path}. <b>You can move it to where you like.</b>");
        }
        
        [MenuItem("Game Flow/General/Create Game General Config File")]
        public static void CreateGameGeneralConfigAsset()
        {
            string file = "Game General Configurations";
            if (!ValidateUniqueFile(file)) return;
            string path = $"Assets/{file}.asset";
            CreateAsset<GameGeneralConfiguration>(path);
            Debug.Log($"New Game General Configuration created at {path}. <b>You can move it to where you like.</b>");
        }
        
        [MenuItem("Game Flow/Managers/Create Managers Definition File")]
        public static void CreateManagersDefinitionFile()
        {
            ReadGameFlowMainDefinitionFile();
            CreateManagersDirectory(definitions["managers-folder"]);
            string path = definitions["managers-folder"] + definitions["managers-file"];
            CreateGameFlowFile(path);
            Debug.Log($"New managers definition file created at: {path}");
        }

        private static bool ValidateUniqueFile(string assetName)
        {
            List<string> paths = AssetDatabase
                .FindAssets(assetName)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(file => file.EndsWith(".asset"))
                .ToList();

            if (paths.Count > 1)
            {
                Debug.LogWarning("Multiple files found for this asset. Make sure to just have one.");
                return false;
            }

            return true;
        }

        private static void CreateAsset<T>(string path) where T : ScriptableObject
        {
            AssetDatabase.CreateAsset(CreateInstance<T>(), path);
            EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<T>(path).GetInstanceID());
        }

        private static void CreateManagersDirectory(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
        
        private static void CreateGameFlowFile(string path)
        {
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes("");
                fs.Write(info);
                fs.Close();
            };
        }

        private static void ReadGameFlowMainDefinitionFile()
        {
            string basePath = Application.dataPath;
            using (StreamReader streamReader = File.OpenText($"{basePath}/GameFlow/Editor/Framework/game-flow.gfd"))
            {
                string content = string.Empty;
                while ((content = streamReader.ReadLine()) != null)
                {
                    if (content.StartsWith("@@")) continue;
                    if (string.IsNullOrEmpty(content)) continue;
                    string[] stringArray = content.Split(":");
                    definitions.Add(stringArray[0].Trim(), stringArray[1].Trim());
                }
                streamReader.Close();
            }
        }
    }
}