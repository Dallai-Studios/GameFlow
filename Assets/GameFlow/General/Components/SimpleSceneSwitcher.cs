using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFlow.General.Components
{
    public class SimpleSceneSwitcher : MonoBehaviour
    {
        [Header("Define the scene name to move")]
        [Tooltip("Use this field to define a scene name to move when you need.")]
        public string DefinedSceneName;

        public void MoveToDefinedScene()
        {
            if(!string.IsNullOrEmpty(this.DefinedSceneName)) SceneManager.LoadScene(this.DefinedSceneName);
            else Debug.LogWarning("No scene name was defined to move", this);
        }

        public void MoveToSceneByIndex(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void MoveToSceneByName(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}