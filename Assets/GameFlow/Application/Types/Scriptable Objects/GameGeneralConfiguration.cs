using System;
using UnityEngine;

namespace GameFlow.Application.Types.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Game General Config", menuName = "Game Flow/Config/Game General Config")]
    public class GameGeneralConfiguration : ScriptableObject
    {
        [Header("Game basic definitions")] 
        [Tooltip("Define if the game must start with the cursor locked")]
        public bool startGameWithCursorLocked;
        
        [Space]
        
        [Header("Game General Configurations")]
        [Tooltip("Define the game master volume")]
        [Range(0,1)] public float masterVolume;

        [Tooltip("Define the game music volume")]
        [Range(0,1)] public float musicVolume;

        [Tooltip("Define the game sound effects volume")]
        [Range(0,1)] public float soundEffectsVolume;

        [Tooltip("Define the game voice volume in case of dialogs")]
        [Range(0,1)] public float voiceVolume;

        [Tooltip("Define the game target frame rate")]
        public int targetFrameRate;

        public event Action OnChangeTargetFrameRate;
        public event Action OnChangeMasterVolume;
        public event Action OnChangeMusicVolume;
        public event Action OnChangeSoundEffectsVolume;
        public event Action OnChangeVoiceVolume;

        public void RaiseEventOnChangeMasterVolume() => this.OnChangeMasterVolume?.Invoke();
        public void RaiseEventOnChangeMusicVolume() => this.OnChangeMusicVolume?.Invoke();
        public void RaiseEventOnChangeSoundEffectsVolume() => this.OnChangeSoundEffectsVolume?.Invoke();
        public void RaiseEventOnChangeVoiceVolume() => this.OnChangeVoiceVolume?.Invoke();
        public void RaiseEventOnChangeTargetFrameRate() => this.OnChangeTargetFrameRate?.Invoke();
    }
}