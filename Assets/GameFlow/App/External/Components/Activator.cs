using UnityEngine;

namespace GameFlow.App.External.Components
{
    /// <summary>
    /// Class Component that can handle activation and deactivation of game objects
    /// </summary>
    public class Activator : MonoBehaviour
    {
        /// <summary>
        /// Deactivate the game object
        /// </summary>
        public void HiddeSelf() => this.gameObject.SetActive(false);
        
        /// <summary>
        /// Activate the game object
        /// </summary>
        public void ShowSelf() => this.gameObject.SetActive(true);
    }
}