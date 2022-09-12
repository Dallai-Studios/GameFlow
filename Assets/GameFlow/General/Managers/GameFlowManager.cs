using GameFlow.General.Scriptable_Objects;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameFlow.General.Managers
{
    public class GameFlowManager : MonoBehaviour
    {
        public static GameFlowManager Instance;

        [Header("Game Default Parameters")]
        [Tooltip("Define the Game general configuration parameters")]
        [SerializeField] private GameGeneralConfiguration gameGeneralConfig;

        private void Start()
        {
            GameFlowManager.Instance ??= this;
            this.UnlockCursor();
            if (this.gameGeneralConfig.startGameWithCursorLocked) this.LockCursor();
        }

        public void LockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void UnlockCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}