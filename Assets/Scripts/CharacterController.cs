using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class CharacterController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;

    [Header("cfg")]
    public float jumpForce;

    public float movementForce;

    public float animatorTolerance;

    private RothnagInputActionAsset.CharacterActionMapActions _inputs;
    private float _currentWalkInput;

    private int _lastDirection = 0;

    private void Awake()
    {
        _inputs = InputProvider.instance.CharacterActionMap;
    }

    private void OnEnable()
    {
        // map functions to input delegates here and unbind them in disable
        _inputs.Walk.performed += Walk;
        _inputs.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        _inputs.Walk.performed -= Walk;
        _inputs.Jump.performed -= Jump;
    }

    private void Walk(InputAction.CallbackContext cb)
        => _currentWalkInput = cb.ReadValue<float>();

    private void Jump(InputAction.CallbackContext cb)
    {
        rb.AddForce(jumpForce * Vector2.up);
        animator.SetTrigger("Jump");
    }

    private int VelocityToDirection(float verticalVelocity)
    {
        if (verticalVelocity <= animatorTolerance && verticalVelocity >= -animatorTolerance)
        {
            return 0;
        }

        return verticalVelocity < 0 ? -1 : 1;
    }

    private void FixedUpdate()
    {
        // apply continuous inputs here
        rb.AddForce(_currentWalkInput * movementForce * Vector2.right);

        int currentDirection = VelocityToDirection(rb.velocity.x);
        if (_lastDirection == currentDirection)
        {
            return;
        }
        
        Vector2 newRotation = new Vector2(0, currentDirection < 0 ? 180 : 0);
        gameObject.transform.eulerAngles = newRotation;
        animator.SetBool("IsWalking", Math.Abs(rb.velocity.x) >= 1);
    }
}