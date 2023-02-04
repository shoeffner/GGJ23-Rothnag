// controller for the player if he is on the overview map to get to different scenes
using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
 
public class MapPlayer : MonoBehaviour {
 
    private float rotationSpeed = 4f;
    private float forwardSpeed = .1f;
    private Vector3 forwardVector = new Vector3(0f, 0f, 0f);
    private RothnagInputActionAsset.CharacterOverviewMapActions _inputs;

    private float _currentForward;
    private float _currentRotate;

    private void Awake()
    {
        _inputs = InputProvider.instance.CharacterOverviewMap;
    }

    private void OnEnable()
    {
        // map functions to input delegates here and unbind them in disable
        _inputs.MapRotate.performed += Rotate;
        _inputs.MapForward.performed += Forward;
    }

    private void OnDisable()
    {
        _inputs.MapRotate.performed -= Rotate;
        _inputs.MapForward.performed -= Forward;
    }

    private void Forward(InputAction.CallbackContext cb)
        => _currentForward = cb.ReadValue<float>();

    private void Rotate(InputAction.CallbackContext cb)
        => _currentRotate = cb.ReadValue<float>();
 
    // Use this for initialization
    void Start () {
    }
 
    // Update is called once per frame
    void Update () { 
        // get the movement
    }
 
    void FixedUpdate() {
        forwardVector.y = forwardSpeed * _currentForward;
        transform.Translate(forwardVector);
        transform.Rotate(0, 0, -_currentRotate * rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGERED");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position=Vector3.zero;
    }

}