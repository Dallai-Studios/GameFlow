using UnityEngine;

namespace GameFlow.Editors.AutoSave
{
    public class AutoSaveConfig : ScriptableObject
    {
        public bool EnableAutoSave = true;
        public int FrequencyInMinutes = 1;
        public bool LogWhenAutoSave = true;
    }
}