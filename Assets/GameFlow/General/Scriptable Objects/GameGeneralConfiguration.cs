using System;
using UnityEngine;

namespace GameFlow.General.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Game General Config", menuName = "Game Flow/General/Config/Game General Config")]
    public class GameGeneralConfiguration : ScriptableObject
    {
        [Header("Game General Configurations")]
        [Tooltip("Define the game master volume")]
        [Range(0,1)] public float MasterVolume;
        public event Action OnChangeMasterVolume;

        [Tooltip("Define the game music volume")]
        [Range(0,1)] public float MusicVolume;
        public event Action OnChangeMusicVolume;

        [Tooltip("Define the game sound effects volume")]
        [Range(0,1)] public float SoundEffectsVolume;
        public event Action OnChangeSoundEffectsVolume;

        [Tooltip("Define the game voice volume in case of dialogs")]
        [Range(0,1)] public float VoiceVolume;
        public event Action OnChangeVoiceVolume;

        [Tooltip("Define the game target frame rate")]
        public int TargetFrameRate;
        public event Action OnChangeTargetFrameRate;

        public void RaiseEventOnChangeMasterVolume() => this.OnChangeMasterVolume?.Invoke();
        public void RaiseEventOnChangeMusicVolume() => this.OnChangeMusicVolume?.Invoke();
        public void RaiseEventOnChangeSoundEffectsVolume() => this.OnChangeSoundEffectsVolume?.Invoke();
        public void RaiseEventOnChangeVoiceVolume() => this.OnChangeVoiceVolume?.Invoke();
        public void RaiseEventOnChangeTargetFrameRate() => this.OnChangeTargetFrameRate?.Invoke();
    }
}