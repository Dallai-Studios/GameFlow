using System;
using UnityEngine;

namespace GameFlow.Application.Types.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "FPS Player Controller Parameters", 
        menuName = "Game Flow/FPS/FPS Player Controller Parameters"
    )]
    public class FpsPlayerControllerParameters : ScriptableObject
    {
        public float aimSensitivity;
        public bool invertXAxis;
        public bool invertYAxis;
        public float downAimClampDegrees;
        public float upAimClampDegrees;

        public event Action OnChangeAimSensitivity;
        public event Action OnInvertXAxis;
        public event Action OnInvertYAxis;

        public void RaiseEventOnChangeAimSensitivity() => this.OnChangeAimSensitivity?.Invoke();
        public void RaiseEventOnInvertXAxis() => this.OnInvertXAxis?.Invoke();
        public void RaiseEventOnInvertYAxis() => this.OnInvertYAxis?.Invoke();
    }
}