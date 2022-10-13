using GameFlow.App.Types.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace GameFlow.Editors.EventEmitters
{
    [CustomEditor(typeof(ExplosiveObjectProperties))]
    public class ExplosiveObjectPropertiesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ExplosiveObjectProperties properties = target as ExplosiveObjectProperties;
            
            EditorGUILayout.Space(30);
            EditorGUILayout.LabelField("Use the button bellow to reset the properties to the default value", EditorStyles.boldLabel);
            if (GUILayout.Button("Reset Properties")) properties.ResetDefaultValues();
        }
    }
}