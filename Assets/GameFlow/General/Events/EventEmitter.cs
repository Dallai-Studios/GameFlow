using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameFlow.General.Events
{
    [CreateAssetMenu(fileName = "Event Emitter", menuName = "Game Flow/General/Events/Event Emitter")]
    public class EventEmitter : ScriptableObject
    {
        [Header("Event Configuration")]
        [Tooltip("This field defines if the event already was invoked on any part of your game")]
        public bool eventAlreadyInvoked;

        [Tooltip("This field defines if the event can run multiple times during the gameplay phase")]
        public bool eventCanRunMultipleTimes;
        public event Action Event;

        public void RaiseEvent()
        {
            if(this.eventCanRunMultipleTimes) this.Event?.Invoke();
            else if(!this.eventAlreadyInvoked) this.Event?.Invoke();
        }
    }
}