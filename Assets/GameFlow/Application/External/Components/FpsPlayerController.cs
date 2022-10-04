using GameFlow.Application.External.Managers;
using GameFlow.Application.Types.Enums;
using GameFlow.Application.Types.ScriptableObjects;
using GameFlow.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameFlow.Application.External.Components
{
    public class FpsPlayerController : MonoBehaviour
    {
        [Header("Define the FPS Configuration Scriptable Object")]
        [Tooltip(FpsPlayerControllerMessageStrings.FPSConfigTooltip)]
        [SerializeField]
        private FpsPlayerControllerParameters fpsConfig;

        [Space]

        [Header("Define the Player Attributes parameters")]
        [Tooltip(FpsPlayerControllerMessageStrings.FPSAttributesTooltip)]
        [SerializeField]
        private PlayerControllerBaseAttributes playerAttributes;

        [Header("Define the camera reference")]
        [Tooltip(FpsPlayerControllerMessageStrings.CameraReferenceTooltip)]
        [SerializeField]
        private Camera playerAimCamera;

        [Space]

        [Header("Define the movement inputs if the new input system is active")]
        [Tooltip(FpsPlayerControllerMessageStrings.MovementInputTooltip)]
        private InputAction _movementInput;
        
        [Tooltip(FpsPlayerControllerMessageStrings.AimYInputTooltip)]
        private InputAction _aimYInput;

        [Tooltip(FpsPlayerControllerMessageStrings.AimXInputTooltip)]
        private InputAction _aimXInput;

        private Rigidbody _rigidBody;
        private Vector3 _movementDirection;
        private float _aimX;
        private float _aimY;
        private float _xAxisCameraRotation;
        private bool _controllerIsEnabled;

        private void Awake() => this._rigidBody = this.GetComponent<Rigidbody>();

        private void Start()
        {
            GameFlowManager.Instance.OnGameStateChange += this.EnablePlayerController;
            this.EnablePlayerController();
            if(!this.playerAimCamera) Debug.LogError(FpsPlayerControllerMessageStrings.NoCameraProvided, this);
            if(!this.playerAttributes) Debug.LogError(FpsPlayerControllerMessageStrings.NoPlayerAttributesProvided, this);
            if(!this.fpsConfig) Debug.LogError(FpsPlayerControllerMessageStrings.NoFPSConfigProvided, this);
            this._movementInput.Enable();
            this._aimXInput.Enable();
            this._aimYInput.Enable();
        }

        private void Update()
        {
            if(!this._controllerIsEnabled) return; 
            this.ReadInputValues(); 
        }

        private void FixedUpdate()
        {
            if(!this._controllerIsEnabled) return;
            this.MovePlayer(); 
            this.PerformAiming();
        }

        private void OnDestroy()
        {
            GameFlowManager.Instance.OnGameStateChange -= this.EnablePlayerController;
        }

        private void EnablePlayerController()
        {
            this._controllerIsEnabled = GameFlowManager.Instance.actualGameState == GameState.PLAY;
        }

        private void ReadInputValues()
        {
            this._movementDirection =
                this._movementInput.ReadValue<Vector3>().normalized *
                (Time.fixedDeltaTime * this.playerAttributes.walkingMovementSpeed);
            
            this._aimX = this._aimXInput.ReadValue<float>() * Time.deltaTime * this.fpsConfig.aimSensitivity;
            this._aimY = this._aimYInput.ReadValue<float>() * Time.deltaTime * this.fpsConfig.aimSensitivity;
            if(this.fpsConfig.invertXAxis) this._aimX *= -1;
            if(this.fpsConfig.invertXAxis) this._aimY *= -1;
        }

        private void MovePlayer()
        {
            Transform playerTransform = this.transform;
            Vector3 movement = (
                (playerTransform.right * this._movementDirection.x) + 
                (playerTransform.forward * this._movementDirection.z)
            );
            this._rigidBody.position += movement;  
        }

        private void PerformAiming()
        {
            this.transform.Rotate(Vector3.up * this._aimX);
            this._xAxisCameraRotation -= this._aimY;
            this._xAxisCameraRotation = Mathf.Clamp(
                this._xAxisCameraRotation, 
                this.fpsConfig.downAimClampDegrees * -1, 
                this.fpsConfig.upAimClampDegrees
            );
            this.playerAimCamera.transform.localRotation = Quaternion.Euler(this._xAxisCameraRotation, 0f, 0f);
        }
    }
}