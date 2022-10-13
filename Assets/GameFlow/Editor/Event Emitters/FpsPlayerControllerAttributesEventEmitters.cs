using GameFlow.App.Types.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace GameFlow.Editors.Event_Emitters
{
    [CustomEditor(typeof(FpsPlayerControllerParameters))]
    public class FpsPlayerControllerAttributesEventEmitters : Editor 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            FpsPlayerControllerParameters parameters = target as FpsPlayerControllerParameters;

            EditorGUILayout.Space(30);

            EditorGUILayout.LabelField("Use the buttons below to raise the events manually");
            if (GUILayout.Button("Raise Event: On change aim sensitivity")) parameters.RaiseEventOnChangeAimSensitivity();
            if (GUILayout.Button("Raise Event: On invert X axis")) parameters.RaiseEventOnInvertXAxis();
            if (GUILayout.Button("Raise Event: On invert Y axis")) parameters.RaiseEventOnInvertYAxis();
            
            EditorGUILayout.Space(30);
            
            EditorGUILayout.LabelField("Use the button below to reset all the parameters");
            if (GUILayout.Button("Reset Parameters")) parameters.ResetParamenters();
        }
    }
}