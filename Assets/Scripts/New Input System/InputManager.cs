using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    PlayerControls _playerControls;

    PlayerLocomotion _playerLocomotion;

    public Vector2 MovementInput;
    public Vector2 CameraInput;

    public bool SprintingInput;
    public bool JumpingInput;

    public float CameraInputX;
    public float CameraInputY;

    public float MoveAmount;
    public float VerticalInput;
    public float HorizontalInput;

    private void Awake()
    {
        _playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();

            _playerControls.PlayerMovement.Movement.performed += i => MovementInput = i.ReadValue<Vector2>();
            _playerControls.PlayerMovement.Camera.performed += i => CameraInput = i.ReadValue<Vector2>();           

            _playerControls.PlayerActions.Sprinting.performed += i => SprintingInput = true;
            _playerControls.PlayerActions.Sprinting.canceled += i => SprintingInput = false;

            //_playerControls.PlayerActions.Jumping.performed += i => HandleJumpingInput();
            _playerControls.PlayerActions.Jumping.performed += i => JumpingInput = true;
            _playerControls.PlayerActions.Jumping.canceled += i => JumpingInput = false;
        }

        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpingInput();
    }

    private void HandleMovementInput()
    {
        VerticalInput = MovementInput.y;
        HorizontalInput = MovementInput.x;

        CameraInputX = CameraInput.x;
        CameraInputY = CameraInput.y;

        MoveAmount = Mathf.Clamp01(Mathf.Abs(HorizontalInput) + Mathf.Abs(VerticalInput));
    }

    private void HandleSprintingInput()
    {
        if(SprintingInput && MoveAmount > 0.5f)
        {
            _playerLocomotion.IsSprinting = true;
        }
        else
        {
            _playerLocomotion.IsSprinting = false;
        }
    }

    private void HandleJumpingInput()
    {
        if (JumpingInput)
        {
            //_playerLocomotion.IsJumping = true;
            //JumpingInput = false;  
            //JumpingInput = false;
            _playerLocomotion.HandleJumping();
        }
    }
}
