using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RigidbodyController : MonoBehaviour
{
    [SerializeField]
    private float accelerationForce = 10f;

    [SerializeField]
    private float maxSpeed = 2;

    [SerializeField]
    PhysicMaterial stoppingPhysicsMaterial, movingPhysicsMaterial;

    private readonly int movementInputAnimParam = Animator.StringToHash("MovementInput");
    private Animator animator;

    [SerializeField]
    private float turnSpeed = 3;
    private new Rigidbody rigidbody;
    private Vector2 input;
    private new Collider collider;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Vector3 cameraLookDirection = GetCameraLookDirection();

        UpdatePhysicsMaterial();
        Move(cameraLookDirection);
        RotateToFaceInputDirection(cameraLookDirection);
    }
    /// <summary>
    /// Turning the character to face the direction it wants to move in.
    /// </summary>
    /// <param name="cameraLookDirection"> Movement direction</param>
     private void RotateToFaceInputDirection(Vector3 cameraLookDirection)
    {
        if (cameraLookDirection.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraLookDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed);
        }
    }

    /// <summary>
    /// Moves the player in a direction based on its max speed and acceleration
    /// </summary>
    /// <param name="MoveDirection"> The direction to move in.</param>
    private void Move(Vector3 MoveDirection)
    {
        if (rigidbody.velocity.magnitude < maxSpeed)
        {
            rigidbody.AddForce(MoveDirection * accelerationForce, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// Upadtes the physics material to a low friction option if the player is moving 
    /// or a high friction version if they are trying to stop.
    /// </summary>
    private void UpdatePhysicsMaterial()
    {
        collider.material = input.magnitude > 0 ? movingPhysicsMaterial : stoppingPhysicsMaterial;
    }

    /// <summary>
    /// Uses the input vector to create a camera relative version so the player can move
    /// based on the camera's forward.
    /// </summary>
    /// <returns> returns the camera look direction (forward)</returns>
    private Vector3 GetCameraLookDirection()
    {
        var inputDirection = new Vector3(input.x, 0, input.y);

        Vector3 flattenedCameraForward = Camera.main.transform.forward;
        flattenedCameraForward.y = 0;

        Quaternion cameraRotation = Quaternion.LookRotation(flattenedCameraForward);

        Vector3 cameraLookDirection = cameraRotation * inputDirection;

        return cameraLookDirection;
    }


    /// <summary>
    /// This event hadnler is called from the PlayerInput component
    /// using the new Input System.
    /// </summary>
    /// <param name="context">Vector2 representing the move input.</param>

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        animator.SetFloat(movementInputAnimParam, input.magnitude);
    }
}
