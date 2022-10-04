using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFlow.Application.External.Components
{
    /// <summary>
    /// Extends from MonoBehaviour. This component is capable of switching between scenes using name or index. 
    /// You can also define a scene name in the inspector and use this component to move for this specific scene. Works only in runtime.
    /// </summary>
    public class SimpleSceneSwitcher : MonoBehaviour
    {
        [Header("Define the scene name to move")]
        [Tooltip("Use this field to define a scene name to move when you need.")]
        public string DefinedSceneName;

        /// <summary>
        /// Move to a scene defined in the inspector. Logs a warning message if the scene name was not provided.
        /// </summary>
        public void MoveToDefinedScene()
        {
            if(!string.IsNullOrEmpty(this.DefinedSceneName)) SceneManager.LoadScene(this.DefinedSceneName);
            else Debug.LogWarning("No scene name was defined to move", this);
        }

        /// <summary>
        /// Move to a scene using the scene integer Id.
        /// </summary>
        /// <param name="SceneIndex">(int) the scene index</param>
        public void MoveToSceneByIndex(int SceneIndex)
        {
            SceneManager.LoadScene(SceneIndex);
        }

        /// <summary>
        /// Move to a scene using the scene name.
        /// </summary>
        /// <param name="SceneName">(string) the scene name</param>
        public void MoveToSceneByName(string SceneName)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}