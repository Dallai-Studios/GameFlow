using System;
using UnityEngine;

namespace GameFlow.App.Types.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Game General Config", menuName = "Game Flow/Config/Game General Config")]
    public class GameGeneralConfiguration : ScriptableObject
    {
        [Header("Game basic definitions")] 
        [Tooltip("Define if the game must start with the cursor locked")]
        public bool startGameWithCursorLocked = false;
        
        [Space]
        
        [Header("Game General Configurations")]
        [Tooltip("Define the game master volume")]
        [Range(0,1)] public float masterVolume = 1;

        [Tooltip("Define the game music volume")]
        [Range(0,1)] public float musicVolume = 1;

        [Tooltip("Define the game sound effects volume")]
        [Range(0,1)] public float soundEffectsVolume = 1;

        [Tooltip("Define the game voice volume in case of dialogs")]
        [Range(0,1)] public float voiceVolume = 1;

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

        public void ResetParameters()
        {
            this.masterVolume = 1;
            this.musicVolume = 1;
            this.voiceVolume = 1;
            this.soundEffectsVolume = 1;
            this.startGameWithCursorLocked = false;
        }
        
        public float GetGameMasterVolume() => this.masterVolume;

        public float GetGameSoundEffectsVolume()
        {
            return this.GetGameVolumeAgainstMasterVolume(this.soundEffectsVolume);
        }

        public float GetGameMusicVolume()
        {
            return this.GetGameVolumeAgainstMasterVolume(this.musicVolume);
        }
        
        public float GetGameVoiceVolume()
        {
            return this.GetGameVolumeAgainstMasterVolume(this.voiceVolume);
        }

        private float GetGameVolumeAgainstMasterVolume(float volume)
        {
            return this.masterVolume <= volume ? this.masterVolume : volume;
        }
    }
}