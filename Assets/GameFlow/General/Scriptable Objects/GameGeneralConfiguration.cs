using UnityEngine;

namespace GameFlow.General.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Game General Config", menuName = "Game Flow/General/Config/Game General Config")]
    public class GameGeneralConfiguration : ScriptableObject
    {
        public float MasterVolume;
        public float MusicVolume;
        public float SoundEffectsVolume;
        public float VoiceVolume;
        public int TargetFrameRate;
    }
}