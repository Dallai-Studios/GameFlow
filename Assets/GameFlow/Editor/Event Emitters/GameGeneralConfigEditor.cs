using GameFlow.App.Types.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace GameFlow.Editors.EventEmitters
{
    [CustomEditor(typeof(GameGeneralConfiguration))]
    public class GameGeneralConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GameGeneralConfiguration configs = target as GameGeneralConfiguration;
            
            EditorGUILayout.Space(30);            
            
            EditorGUILayout.LabelField("Use the buttons below to raise the events manually");
            
            if (GUILayout.Button("Raise Event: On change master volume")) configs.RaiseEventOnChangeMasterVolume();
            if (GUILayout.Button("Raise Event: On change music volume")) configs.RaiseEventOnChangeMusicVolume();
            if (GUILayout.Button("Raise Event: On change voice volume")) configs.RaiseEventOnChangeVoiceVolume();
            if (GUILayout.Button("Raise Event: On change sound effects volume")) configs.RaiseEventOnChangeSoundEffectsVolume();
            if (GUILayout.Button("Raise Event: On change target framerate")) configs.RaiseEventOnChangeTargetFrameRate();
            
            EditorGUILayout.Space(30);            
            
            EditorGUILayout.LabelField("Use the button below to reset the parameters");
            if (GUILayout.Button("Reset Parameters")) configs.ResetParameters();
        }
    }
}