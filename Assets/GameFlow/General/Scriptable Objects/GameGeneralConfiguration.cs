using UnityEngine;

namespace GameFlow.General.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Game General Config", menuName = "Game Flow/General/Config/Game General Config")]
    public class GameGeneralConfiguration : ScriptableObject
    {
        [Header("Game General Configurations")]
        [Tooltip("Define the game master volume")]
        [Range(0,1)] public float MasterVolume;

        [Tooltip("Define the game music volume")]
        [Range(0,1)] public float MusicVolume;

        [Tooltip("Define the game sound effects volume")]
        [Range(0,1)] public float SoundEffectsVolume;

        [Tooltip("Define the game voice volume in case of dialogs")]
        [Range(0,1)] public float VoiceVolume;

        [Tooltip("Define the game target frame rate")]
        public int TargetFrameRate;
    }
}