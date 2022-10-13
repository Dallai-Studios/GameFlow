using UnityEngine;

namespace GameFlow.App.Types.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Game Debug Config", menuName = "Game Flow/General/Config/Game Debug Config")]
    public class GameDebugConfig : ScriptableObject
    {
        [Header("Define all the Debug options")]
        [Tooltip("Use this options if you want to enable the framework to display all the internal runtime messages")]
        public bool DisplayFrameworkLogs = true;

        [Tooltip("If this options is set to true, the game always start from the first scene")]
        public bool ShouldRunFromTheStartScene = true;

        [Tooltip("If you defined the game to run always from the first scene, you can override it to a designated scene instead")]
        public int StartSceneIndex = 0;

        public void ResetParameters()
        {
            this.DisplayFrameworkLogs = true;
            this.ShouldRunFromTheStartScene = true;
            this.StartSceneIndex = 0;
        }
    }
}