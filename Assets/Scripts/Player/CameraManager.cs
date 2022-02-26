using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform TargetTransform;
    public Transform CameraPivot;
    private Transform CameraTransform;

    public LayerMask CollisionLayers;

    public float CameraFollowSpeed = 0.2f;
    public float CameraLookSpeed = 2f;
    public float CameraPivotSpeed = 2f;
    public float CameraCollisionRadius = 0.2f;
    public float CameraCollisionOffset = 0.2f;
    public float MinimumCollisionOffset = 0.2f;

    public float LookAngle;
    public float PivotAngle;

    public float MinimumPivotAngle = -35f;
    public float MaximumPivotAngle = 35f;

    public InputManager InputManager;

    private Vector3 _cameraFollowVelocity = Vector3.zero;
    private Vector3 _cameraVectorPosition;
    private float _defaultPosition;
  
    private void Awake()
    {
        //TargetTransform = FindObjectOfType<PlayerManager>().transform;
        //InputManager = FindObjectOfType<InputManager>();
        CameraTransform = Camera.main.transform;
        _defaultPosition = CameraTransform.localPosition.z;    
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position,
            TargetTransform.position,
            ref _cameraFollowVelocity,
            CameraFollowSpeed);

        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        LookAngle = LookAngle + (InputManager.CameraInputX * CameraLookSpeed);
        PivotAngle = PivotAngle - (InputManager.CameraInputY * CameraPivotSpeed);

        PivotAngle = Mathf.Clamp(PivotAngle, MinimumPivotAngle, MaximumPivotAngle);

        rotation = Vector3.zero;
        rotation.y = LookAngle;

        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = PivotAngle;
        targetRotation = Quaternion.Euler(rotation);

        CameraPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollisions()
    {
        float targetPosition = _defaultPosition;
        RaycastHit hit;
        Vector3 direction = CameraTransform.position - CameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(CameraPivot.transform.position, CameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), CollisionLayers))
        {
            float distance = Vector3.Distance(CameraPivot.position, hit.point);
            targetPosition = - (distance - CameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < MinimumCollisionOffset)
        {
            targetPosition = targetPosition - MinimumCollisionOffset;
            
        }
        
        _cameraVectorPosition.z = Mathf.Lerp(CameraTransform.localPosition.z, targetPosition, 0.2f);
        CameraTransform.localPosition = _cameraVectorPosition;
    }
}
