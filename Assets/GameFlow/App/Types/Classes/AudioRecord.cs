using System;
using UnityEngine;

namespace GameFlow.Types.Classes
{
    [Serializable]
    public class AudioRecord
    {
        public string ClipName;
        public AudioClip AudioClip;
        public bool Loop;
        [Range(-0.9f, 0.9f)] public float VolumeOverride;
        [Range(-3f, 3f)] public float Pitch = 1f;
    }
}