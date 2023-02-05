using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// controller for the player if he is on the overview map to get to different scenes
/// </summary>
public sealed class MapPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    // get only the image so we can rotate it without rotating the gameobject
    private Transform image;

    [Header("cfg")]
    [SerializeField]
    private float movementForce;
    [SerializeField]
    private float imageRotationOffset;

    private RothnagInputActionAsset.CharacterOverviewMapActions _inputs;
    private Vector2 _currentWalkInput;

    public Animator animator;

    private void Awake()
    {
        _inputs = InputProvider.instance.CharacterOverviewMap;
        image = transform.Find("player_top_image");
    }

    private void OnEnable()
    {
        // map functions to input delegates here and unbind them in disable
        _inputs.Walk.performed += Walk;
    }

    private void OnDisable()
    {
        _inputs.Walk.performed -= Walk;
    }

    private void Walk(InputAction.CallbackContext cb)
        => _currentWalkInput = cb.ReadValue<Vector2>();

    void FixedUpdate()
    {
        rb.AddForce(_currentWalkInput * movementForce);
        var velocity = rb.velocity;
        float angle = Mathf.Atan2(velocity.y * Mathf.Deg2Rad, velocity.x * Mathf.Deg2Rad) * Mathf.Rad2Deg + imageRotationOffset;
        angle %= 360f;
        image.rotation = Quaternion.Euler(0f, 0f, angle);
        animator.SetBool("IsWalking", Math.Abs(_currentWalkInput.x) >= .1f || Math.Abs(_currentWalkInput.y) >= .1f);
    }
}