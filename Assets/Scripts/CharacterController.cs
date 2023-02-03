using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class CharacterController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;

    [Header("cfg")]
    public float jumpForce;

    public float movementForce;

    private RothnagInputActionAsset.CharacterActionMapActions _inputs;
    private float _currentWalkInput;

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
    }

    private void FixedUpdate()
    {
        // apply continuous inputs here
        
        rb.AddForce(_currentWalkInput * movementForce * Vector2.right);
    }
}