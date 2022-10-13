using System;
using UnityEngine;
using UnityEngine.InputSystem;
using GameFlow.Misc;
using GameFlow.App.Utilities;
using GameFlow.App.Types.Enums;
using GameFlow.App.External.Managers;
using GameFlow.App.Types.ScriptableObjects;

namespace GameFlow.App.External.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class FpsPlayerController : MonoBehaviour
    {
        [Header("Define the FPS Configuration Scriptable Object")]
        [Tooltip(FpsPlayerControllerMessageStrings.FPSConfigTooltip)]
        [SerializeField] private FpsPlayerControllerParameters fpsConfig;

        [Space]
        
        [Header("Define if the controller must 'bend' the camera if the player walk horizontally")]
        [SerializeField] private bool bendCamera = false;
        [SerializeField] [Range(0.0f, 5.0f)] private float amountOfBend = 0.0f;
        [Tooltip("Use this parameter to define how fast the bend must be. The higher the number, the faster the bend")]
        [SerializeField] private float bendDamping = 5f;
        
        [Space]

        [Header("Define the Player Attributes parameters")]
        [Tooltip(FpsPlayerControllerMessageStrings.FPSAttributesTooltip)]
        [SerializeField] private PlayerControllerBaseAttributes playerAttributes;

        [Header("Define the camera reference")]
        [Tooltip(FpsPlayerControllerMessageStrings.CameraReferenceTooltip)]
        [SerializeField] private Camera playerAimCamera;

        [Space]

        [Header("Define the movement inputs if the new input system is active")]
        [Tooltip(FpsPlayerControllerMessageStrings.MovementInputTooltip)]
        [SerializeField] private InputAction movementInput;
        
        [Tooltip(FpsPlayerControllerMessageStrings.AimYInputTooltip)]
        [SerializeField] private InputAction aimYInput;

        [Tooltip(FpsPlayerControllerMessageStrings.AimXInputTooltip)]
        [SerializeField] private InputAction aimXInput;
        
        [Tooltip(FpsPlayerControllerMessageStrings.JumpInputTooltip)]
        [SerializeField] private InputAction jumpInput;

        [Space] 
        
        [Header("Define the forward direction of the controller")] 
        [SerializeField] private Color gizmosColor = Color.cyan;

        private Rigidbody controllerRigidbody = new();
        private Vector3 movementDirection;
        private float aimX, aimY, xAxisCameraRotation;
        private bool controllerIsEnabled;
        private float bendValue = 0f;

        private void Awake() => this.controllerRigidbody = this.GetComponent<Rigidbody>();

        private void Start()
        {
            GameFlowManager.Instance.OnGameStateChange += this.EnablePlayerController;
            Helpers.LockCursor();
            this.EnablePlayerController();
            if (!this.playerAimCamera) Debug.LogError(FpsPlayerControllerMessageStrings.NoCameraProvided, this);
            if (!this.playerAttributes) Debug.LogError(FpsPlayerControllerMessageStrings.NoPlayerAttributesProvided, this);
            if (!this.fpsConfig) Debug.LogError(FpsPlayerControllerMessageStrings.NoFPSConfigProvided, this);
            this.movementInput.Enable();
            this.aimXInput.Enable();
            this.aimYInput.Enable();
        }
        
        private void Update()
        {
            if (!this.controllerIsEnabled) return;
            this.ReadInputValues();
            this.PerformAiming();
        }

        private void FixedUpdate()
        {
            if (!this.controllerIsEnabled) return;
            this.MovePlayer(); 
        }

        private void OnDestroy()
        {
            GameFlowManager.Instance.OnGameStateChange -= this.EnablePlayerController;
        }

        private void EnablePlayerController()
        {
            this.controllerIsEnabled = GameFlowManager.Instance.GetGameState() == GameState.Play;
        }

        private void ReadInputValues()
        {
            this.movementDirection =
                this.movementInput.ReadValue<Vector3>().normalized *
                (Time.fixedDeltaTime * this.playerAttributes.walkingMovementSpeed);
            
            this.aimX = this.aimXInput.ReadValue<float>() * Time.deltaTime * (this.fpsConfig.aimSensitivity * 100);
            this.aimY = this.aimYInput.ReadValue<float>() * Time.deltaTime * (this.fpsConfig.aimSensitivity * 100);
            if (this.fpsConfig.invertXAxis) this.aimX *= -1;
            if (this.fpsConfig.invertXAxis) this.aimY *= -1;
        }

        private void MovePlayer()
        {
            Transform playerTransform = this.transform;
            Vector3 movement = (
                (playerTransform.right * this.movementDirection.x) + 
                (playerTransform.forward * this.movementDirection.z)
            );
            
            this.controllerRigidbody.position += movement;
            
            if (this.bendCamera)
            {
                if (this.movementDirection.x > 0) this.bendValue = this.amountOfBend * -1;
                if (this.movementDirection.x < 0) this.bendValue = this.amountOfBend;
                if (this.movementDirection.x == 0) this.bendValue = 0f;
                
                Quaternion desiredRotationOnZ = Quaternion.Euler(
                    this.playerAimCamera.transform.eulerAngles.x,
                    this.playerAimCamera.transform.eulerAngles.y,
                    this.bendValue
                );
                
                this.playerAimCamera.transform.rotation = Quaternion.Lerp(
                    this.playerAimCamera.transform.rotation, 
                    desiredRotationOnZ, 
                    Time.deltaTime * this.bendDamping
                );
            }
        }

        private void PerformAiming()
        {
            this.transform.Rotate(Vector3.up * this.aimX);
            this.xAxisCameraRotation -= this.aimY;
            this.xAxisCameraRotation = Mathf.Clamp(
                this.xAxisCameraRotation, 
                this.fpsConfig.upAimClampDegrees * -1, 
                this.fpsConfig.downAimClampDegrees
            );
            
            this.playerAimCamera.transform.localRotation = Quaternion.Euler(
                this.xAxisCameraRotation, 0f, this.playerAimCamera.transform.eulerAngles.z
            );
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = this.gizmosColor;
            Gizmos.DrawRay(this.transform.position, Vector3.forward * 1.2f);
            Gizmos.DrawIcon(
                this.transform.position + Vector3.forward * 1.2f, 
                "Forward Point", 
                true, 
                this.gizmosColor
            );
            Gizmos.color = Color.white;
        }
    }
}