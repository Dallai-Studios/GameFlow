using GameFlow.App.Types.Interfaces;
using UnityEngine;

namespace GameFlow.App.External.Components
{
    public class BreakableObject: MonoBehaviour, IDamageble
    {
        [Header("Define the object amount of durability (health)")]
        public int Durability = 50;

        public void ApplyDamage(int Damage)
        {
            this.Durability -= Damage;
            if (this.Durability <= 0) this.Kill();
        }

        public void Kill()
        {
            this.Durability = 0;
            this.DestroySelfAndDisplayParticles();
        }

        private void DestroySelfAndDisplayParticles()
        {
            throw new System.NotImplementedException();
        }
    }
}