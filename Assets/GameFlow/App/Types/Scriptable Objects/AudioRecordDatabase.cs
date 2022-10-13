using System.Collections.Generic;
using GameFlow.Types.Classes;
using UnityEngine;

namespace GameFlow.Types.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Audio Record Database", menuName = "Game Flow/Audio/Audio Record Database")]
    public class AudioRecordDatabase : ScriptableObject
    {
        public List<AudioRecord> Records;
    }
}