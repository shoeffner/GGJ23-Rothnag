using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class BucketMiniGame : MonoBehaviour
{
    private bool bucketFull = false;
    public Rigidbody2D rb;
    public Collider2D top;
    public Collider2D bottom;
    private RothnagInputActionAsset.BucketMiniGameActions _inputs;
    private RothnagInputActionAsset.CharacterActionMapActions _charInputs;

    [Header("cfg")]
    public float jumpForce = 1.0f;

    private Vector3 bucketPosition;
    // Start is called before the first frame update
    
    private void Awake()
    {
        _inputs = InputProvider.instance.BucketMiniGame;
        _charInputs = InputProvider.instance.CharacterActionMap;
    }

    public void OnEnable() {
        if (top == null) {
            top = GameObject.Find("well_top").GetComponent<Collider2D>();
        }
        if (bottom == null) {
            bottom = GameObject.Find("well_bottom").GetComponent<Collider2D>();
        }
        if (rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }
        _inputs.Crank.performed += Crank;
    }

    private void Crank(InputAction.CallbackContext cb)
    {
        rb.AddForce(jumpForce * Vector2.up);
    }

    void Start()
    {
        bucketPosition = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z);
        top.enabled = false;
        rb.gravityScale = 0f;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other == bottom) {
            bucketFull = true;
            // start for the player
            top.enabled = true;
            rb.gravityScale = 0.1f;
        }
    }

    public void PreStartGame() {
        _charInputs.Jump.Disable();
        transform.position = new Vector3(
            bucketPosition.x,
            bucketPosition.y,
            bucketPosition.z);
        rb.gravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
