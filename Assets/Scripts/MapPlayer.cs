// controller for the player if he is on the overview map to get to different scenes
using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Rothnag;

public class MapPlayer : MonoBehaviour {

    private float movementSpeed = .1f;
    private Vector3 movementVector = new Vector3(0f, 0f, 0f);
    private Vector3 rotationVector = new Vector3(0f, 0f, 0f);
    private RothnagInputActionAsset.CharacterOverviewMapActions _inputs;

    private float leftRight;
    private float upDown;

    // get only the image so we can rotate it without rotating the gameobject
    private Transform image;

    private void Awake()
    {
        _inputs = InputProvider.instance.CharacterOverviewMap;
        image = transform.Find("player_top_image");
    }

    private void OnEnable()
    {
        // map functions to input delegates here and unbind them in disable
        _inputs.MapLeftRight.performed += LeftRightFunc;
        _inputs.MapUpDown.performed += UpDownFunc;
    }

    private void OnDisable()
    {
        _inputs.MapLeftRight.performed -= LeftRightFunc;
        _inputs.MapUpDown.performed -= UpDownFunc;
    }

    private void UpDownFunc(InputAction.CallbackContext cb)
        => upDown = cb.ReadValue<float>();

    private void LeftRightFunc(InputAction.CallbackContext cb)
        => leftRight = cb.ReadValue<float>();

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        // get the movement
    }

    void FixedUpdate() {
        movementVector.x = movementSpeed * leftRight;
        movementVector.y = movementSpeed * upDown;
        transform.Translate(movementVector);
        if (leftRight != 0) {
            rotationVector.z = -leftRight * 90f;
        }
        if (upDown == 1) {
            rotationVector.z = 0f;
        }
        if (upDown == -1) {
            rotationVector.z = 180f;
        }
        image.eulerAngles = rotationVector;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position=Vector3.zero;
    }

}
