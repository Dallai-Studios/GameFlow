using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameFlow.App.External.Events
{
    [CreateAssetMenu(fileName = "Event Emitter", menuName = "Game Flow/General/Events/Event Emitter")]
    public class EventEmitter : ScriptableObject
    {
        [Header("Event Configuration")]
        [Tooltip("This field defines if the event already was invoked on any part of your game")]
        [SerializeField] private bool eventAlreadyInvoked;

        [Tooltip("This field defines if the event can run multiple times during the gameplay phase")]
        [SerializeField] private bool eventCanRunMultipleTimes;
        public event Action Event;

        public void RaiseEvent()
        {
            if(!this.eventAlreadyInvoked) this.Event?.Invoke();
            if(this.eventAlreadyInvoked && this.eventCanRunMultipleTimes) this.Event?.Invoke();
        }
    }
}