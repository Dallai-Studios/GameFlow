using GameFlow.App.Types.Interfaces;
using GameFlow.App.Types.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace GameFlow.App.External.Components
{
    public class ExplosiveObject : MonoBehaviour, IDamageble
    {
        [Header("Define the explosion object properties scriptable object")] 
        public ExplosiveObjectProperties Properties;

        [Space]

        [Header("Define the object durability")]
        [Tooltip("This parameter defines how much damage the object needs to take before explode")]
        public int Durability = 50;


        public void ApplyDamage(int Damage)
        {
            this.Durability -= Damage;
            if (this.Durability <= 0f) this.Kill();
        } 

        public void Kill()
        {
            this.Durability = 0;
            this.gameObject.SetActive(false);
        }

        private void CreateExplosionCollider()
        {
            SphereCollider explosionTriggerCollider = this.AddComponent<SphereCollider>();
            explosionTriggerCollider.radius = this.Properties.ExplosionRange;
            // play the particles
            
        }
        
        private void OnDrawGizmosSelected()
        {
            if (this.Properties == null) return;
            Gizmos.color = this.Properties.ExplosionRangeGizmoColor;
            Gizmos.DrawWireSphere(this.transform.position, this.Properties.ExplosionRange);
            Gizmos.color = Color.white;
        }
    }
}