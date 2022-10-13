using UnityEngine;

namespace GameFlow.App.Types.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Base Enemy Attributes", menuName = "Game Flow/Enemies/Base Enemy Attributes")]
    public class BaseEnemyAttributes : ScriptableObject
    {
        public float walkingMovementSpeed;
        public float health;
        public float maxHealth;
        public float stamina;
        public float maxStamina;
    }
}