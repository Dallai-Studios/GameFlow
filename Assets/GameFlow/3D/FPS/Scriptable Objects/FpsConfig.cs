using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameFlow._3D.FPS.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Fps Configurations", menuName = "GameFlow/3D/FPS/Fps Configurations")]
    public class FpsConfig : ScriptableObject
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