using GameFlow.General.ScriptableObjects;
using UnityEngine;

namespace GameFlow.General.Managers
{
    public class GameFlowManager : MonoBehaviour
    {
        #region "Public Properties"
            public static GameFlowManager Instance;
        #endregion

        #region "Serialized Properties"
            [Header("Game Default Parameters")]
            [Tooltip("Define the Game general configuration parameters")]
            [SerializeField] private GameGeneralConfiguration _gameGeneralConfig;
        #endregion

        #region "Monobehaviour Methods"
            private void Start()
            {
                if(GameFlowManager.Instance is null) GameFlowManager.Instance = this;
            }
        #endregion
    }
}