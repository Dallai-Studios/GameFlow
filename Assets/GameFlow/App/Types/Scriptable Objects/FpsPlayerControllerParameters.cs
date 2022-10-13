using System;
using UnityEngine;

namespace GameFlow.App.Types.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "FPS Player Controller Parameters", 
        menuName = "Game Flow/FPS/FPS Player Controller Parameters"
    )]
    public class FpsPlayerControllerParameters : ScriptableObject
    {
        [Header("Define the controller aim sensitivity")]
        [Range(0.1f, 10f)] public float aimSensitivity = 1f;
        
        [Space]
        
        [Header("Define if the X axis or Y axis must be inverted")]
        public bool invertXAxis = false;
        public bool invertYAxis = false;
        
        [Space]
        
        [Header("Define the maximum up and down rotation in degrees for the player fps camera")]
        [Range(20f, 90f)] public float downAimClampDegrees = 70f;
        [Range(20f, 90f)] public float upAimClampDegrees = 70f;

        public event Action OnChangeAimSensitivity;
        public event Action OnInvertXAxis;
        public event Action OnInvertYAxis;

        public void RaiseEventOnChangeAimSensitivity() => this.OnChangeAimSensitivity?.Invoke();
        public void RaiseEventOnInvertXAxis() => this.OnInvertXAxis?.Invoke();
        public void RaiseEventOnInvertYAxis() => this.OnInvertYAxis?.Invoke();

        public void ResetParamenters()
        {
            this.aimSensitivity = 1f;
            this.invertXAxis = false;
            this.invertYAxis = false;
            this.downAimClampDegrees = 70;
            this.upAimClampDegrees = 70;
        }
    }
}