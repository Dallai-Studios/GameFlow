using GameFlow.General.ScriptableObjects;
using UnityEngine;

namespace GameFlow.General.Managers
{
    public class GameFlowManager : MonoBehaviour
    {
        public static GameFlowManager Instance;

        [Header("Game Default Parameters")]
            [Tooltip("Define the Game general configuration parameters")]
            [SerializeField] private GameGeneralConfiguration _gameGeneralConfig;

        private void Start()
            {
                if(GameFlowManager.Instance is null) GameFlowManager.Instance = this;
            }
    }
}