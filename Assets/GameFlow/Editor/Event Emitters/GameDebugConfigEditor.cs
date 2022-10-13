using GameFlow.App.Types.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace GameFlow.Editors.EventEmitters
{
    [CustomEditor(typeof(GameDebugConfig))]
    public class GameDebugConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameDebugConfig configs = target as GameDebugConfig;
            
            EditorGUILayout.Space(30);
            EditorGUILayout.LabelField("Use the button below to reset the properties");
            if (GUILayout.Button("Reset parameters")) configs.ResetParameters();
        }
    }
}