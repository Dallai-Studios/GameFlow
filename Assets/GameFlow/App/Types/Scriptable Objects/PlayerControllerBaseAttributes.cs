using UnityEngine;

namespace GameFlow.App.Types.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "Player Controller Base Attributes", 
        menuName = "Game Flow/Player/Attributes/Player Controller Base Attributes"
    )]
    public class PlayerControllerBaseAttributes : ScriptableObject
    {
        [Header("Player Basic Attributes")]
        [Tooltip("Define the player maximum movement speed")]
        public float walkingMovementSpeed;

        [Tooltip("Define the player maximum amount of health")]
        public float runningMovementSpeed;

        [Tooltip("Define how high the player can jump")]
        public float jumpForce = 10;

        [Tooltip("Define the player current amount of heath")]
        public int health;

        [Tooltip("Define the player maximum amount of health")]
        public int maxHealth;

        [Tooltip("Define the player maximum amount of health")]
        public int stamina;

        [Tooltip("Define the player maximum amount of health")]
        public int maxStamina;
    }
}