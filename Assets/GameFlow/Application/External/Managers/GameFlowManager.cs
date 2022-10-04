using System;
using UnityEngine;
using UnityEngine.Serialization;
using GameFlow.Application.Types.Enums;
using GameFlow.Application.Types.ScriptableObjects;

namespace GameFlow.Application.External.Managers
{
    public class GameFlowManager : MonoBehaviour
    {
        public static GameFlowManager Instance;

        public GameState actualGameState;
        public event Action OnGameStateChange;

        [Header("Game Default Parameters")]
        [Tooltip("Define the Game general configuration parameters")]
        [SerializeField] private GameGeneralConfiguration gameGeneralConfig;
        
        [Header("Define the GameFlow Debug Options")]
        [SerializeField] private GameDebugConfig gameDebugConfig; 

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