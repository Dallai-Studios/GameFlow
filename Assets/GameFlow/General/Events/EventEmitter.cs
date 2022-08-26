using System;
using UnityEngine;

namespace GameFlow.General.Events
{
    [CreateAssetMenu(fileName = "Event Emitter", menuName = "Game Flow/Events/Event Emitter")]
    public class EventEmitter : ScriptableObject
    {
        [Header("Event Configuration")]
        [Tooltip("This field defines if the event already was invoked on any part of your game")]
        public bool EventAlreadyInvoked;

        [Tooltip("This field defines if the event can run multiple times during the gameplay phase")]
        public bool EventCanRunMultipleTimes;
        public event Action Event;

        public void RaiseEvent()
        {
            if(this.EventCanRunMultipleTimes) this.Event?.Invoke();
            else if(!this.EventAlreadyInvoked) this.Event?.Invoke();
        }
    }
}