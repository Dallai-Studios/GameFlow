using UnityEngine;

namespace GameFlow.App.Types.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Explosive Object Properties", menuName = "Game Flow/Utilities/Explosive Objective Properties")]
    public class ExplosiveObjectProperties : ScriptableObject
    {
        [Header("Define how much damage the explosion does")]
        public int DamageAmount = 100;
        
        [Space]
        
        [Header("Define the explosion Range")]
        public float ExplosionRange = 5f;
        
        [Space]
        
        [Header("Define the gizmo color on editor mode")]
        public Color ExplosionRangeGizmoColor = Color.red;

        public void ResetDefaultValues()
        {
            this.DamageAmount = 100;
            this.ExplosionRange = 5f;
            this.ExplosionRangeGizmoColor = Color.red;
        }
    }
}