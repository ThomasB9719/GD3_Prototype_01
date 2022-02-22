using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager _inputManager;
    PlayerManager _playerManager;

    Vector3 _moveDirection;
    Transform _cameraObject;
    Rigidbody _playerRigidbody;

    [Header("Falling")]
    public float InAirTimer;
    public float LeapingVelocity;
    public float FallingVelocity;
    public float RayCastHeightOffset = 0.5f;
    public LayerMask GroundLayer;
    
    [Header("Movement Flags")]
    public bool IsSprinting;
    public bool IsGrounded;
    public bool IsJumping;

    [Header("Movement Speeds")]
    public float WalkingSpeed = 1.5f;
    public float MovementSpeed = 5f;
    public float SprintingSpeed = 7.5f;
    public float RotationSpeed = 15f;

    [Header("Jump Speeds")]
    public float JumpHeight = 3f;
    public float GravityIntensity = -15f;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerManager = GetComponent<PlayerManager>();

        _playerRigidbody = GetComponent<Rigidbody>();
        _cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        //HandleJumping();
        
        if (_playerManager.IsInteracting)
            return;
        
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        //if (IsJumping) return;
        
        _moveDirection = _cameraObject.forward * _inputManager.VerticalInput;
        _moveDirection = _moveDirection + _cameraObject.right * _inputManager.HorizontalInput;
        _moveDirection.Normalize();
        _moveDirection.y = 0;

        if (IsSprinting)
        {
            _moveDirection = _moveDirection * SprintingSpeed;
        }
        else
        {
            if (_inputManager.MoveAmount > 0.5f)
            {
                _moveDirection = _moveDirection * MovementSpeed;
            }
            else
            {
                _moveDirection = _moveDirection * WalkingSpeed;
            }
        }

        Vector3 movementVelocity = _moveDirection;
        _playerRigidbody.velocity += movementVelocity / 25;
    }

    private void HandleRotation()
    {
        if (IsJumping) return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = _cameraObject.forward * _inputManager.VerticalInput;
        targetDirection = targetDirection + _cameraObject.right * _inputManager.HorizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + RayCastHeightOffset;

        if(!IsGrounded /*&& !IsJumping*/)
        {
            InAirTimer = InAirTimer + Time.deltaTime;
            _playerRigidbody.AddForce(transform.forward * LeapingVelocity);
            _playerRigidbody.AddForce(-Vector3.up * FallingVelocity * InAirTimer, ForceMode.Force);

            //_playerRigidbody.velocity += Physics.gravity;

            //Debug.Log(InAirTimer);
        }

        if(Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, 2f, GroundLayer))
        {
            InAirTimer = 0;
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    //public void HandleJumping()
    //{
    //    if(IsGrounded && IsJumping)
    //    {
    //        float jumpingVelocity = Mathf.Sqrt(-2f * GravityIntensity * JumpHeight);
    //        Vector3 playerVelocity = _moveDirection;
    //        playerVelocity.y = jumpingVelocity * 1000;

    //        //_playerRigidbody.velocity = playerVelocity;
    //        _playerRigidbody.AddForce(playerVelocity);
    //        IsJumping = false;
    //        Debug.Log(jumpingVelocity);
    //    }
    //}

    public void HandleJumping()
    {
        if (IsGrounded /*&& IsJumping*/)
        {
            float jumpingVelocity = Mathf.Sqrt(-2f * GravityIntensity * JumpHeight);
            Vector3 jumpVector = new Vector3(0, jumpingVelocity, 0);

            //Vector3 playerVelocity = _moveDirection;
            //playerVelocity.y = jumpingVelocity * 1000;

            _playerRigidbody.velocity += jumpVector;

            //_playerRigidbody.AddForce(playerVelocity);
            //IsJumping = false;
            //Debug.Log(jumpingVelocity);
        }
    }
}
