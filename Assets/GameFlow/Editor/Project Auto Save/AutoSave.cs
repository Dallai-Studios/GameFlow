using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GameFlow.Editors.AutoSave
{
    [CustomEditor(typeof(AutoSaveConfig))]
    public class AutoSave : Editor
    {
        private static AutoSaveConfig config;
        private static CancellationTokenSource tokenSource;
        private static Task task;

        [InitializeOnLoadMethod]
        private static void OnInitialize() {
            FetchConfig();
            CancelTask();
            tokenSource = new CancellationTokenSource();
            task = SaveInterval(tokenSource.Token);
        }

        private static void FetchConfig() {
            while (true) {
                if (config != null) return;

                string path = GetConfigPath();

                if (path == null) {
                    AssetDatabase.CreateAsset(CreateInstance<AutoSaveConfig>(), $"Assets/{nameof(AutoSaveConfig)}.asset");
                    Debug.Log("A config file has been created at the root of your project.<b> You can move this anywhere you'd like.</b>");
                    continue;
                }

                config = AssetDatabase.LoadAssetAtPath<AutoSaveConfig>(path);

                break;
            }
        }

        private static string GetConfigPath() {
            List<string> paths = AssetDatabase
                .FindAssets(nameof(AutoSaveConfig))
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(config => config.EndsWith(".asset"))
                .ToList();
            
            if (paths.Count > 1) Debug.LogWarning("Multiple auto save config assets found. Make sure to just have one.");
            return paths.FirstOrDefault();
        }

        private static void CancelTask() {
            if (task == null) return;
            tokenSource.Cancel();
            task.Wait();
        }

        private static async Task SaveInterval(CancellationToken token) {
            while (!token.IsCancellationRequested) {
                await Task.Delay(config.FrequencyInMinutes * 1000 * 60, token);
                if (config == null) FetchConfig();

                if (!config.EnableAutoSave || Application.isPlaying || BuildPipeline.isBuildingPlayer || EditorApplication.isCompiling) continue;
                if (!UnityEditorInternal.InternalEditorUtility.isApplicationActive) continue;

                EditorSceneManager.SaveOpenScenes();
                if (config.LogWhenAutoSave) Debug.Log($"Project auto saved at: {DateTime.Now:h:mm:ss tt}");
            }
        }
        
        [MenuItem("Game Flow/Auto save/Create Auto save Config File", priority = 1)]
        public static void ShowConfig() {
            FetchConfig();

            string path = GetConfigPath();
            EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<AutoSaveConfig>(path).GetInstanceID());
        }

        public override void OnInspectorGUI() {
            DrawDefaultInspector();
        }
    }
}