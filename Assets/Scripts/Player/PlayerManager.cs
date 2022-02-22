using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager _inputManager;
    PlayerLocomotion _playerLocomotion;
    CameraManager _cameraManager;

    public bool IsInteracting;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
        _cameraManager = FindObjectOfType<CameraManager>();
    }

    private void Update()
    {
        _inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        _playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        _cameraManager.HandleAllCameraMovement();
    }
}
