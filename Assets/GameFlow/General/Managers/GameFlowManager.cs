using System;
using GameFlow.General.Scriptable_Objects;
using GameFlow.General.Types;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameFlow.General.Managers
{
    public class GameFlowManager : MonoBehaviour
    {
        public static GameFlowManager Instance;

        public GameState actualGameState;
        public event Action OnGameStateChange;

        [Header("Game Default Parameters")]
        [Tooltip("Define the Game general configuration parameters")]
        [SerializeField] private GameGeneralConfiguration gameGeneralConfig;

        private void Start()
        {
            Instance ??= this;
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

        public void SetGameState(GameState NewGameState) 
        {
            this.actualGameState = NewGameState;
            this.RaiseEventOnGameStateChange();
        }

        private void RaiseEventOnGameStateChange() => this.OnGameStateChange?.Invoke();
    }
}