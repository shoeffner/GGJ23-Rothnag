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
    public SpriteRenderer spriteRenderer;
    
    public Sprite bucketFullSprite;
    public Sprite bucketEmptySprite;

    private RothnagInputActionAsset.BucketMiniGameActions _inputs;
    private RothnagInputActionAsset.CharacterActionMapActions _charInputs;
    

    [Header("cfg")]
    public float pullForce = 3.0f;

    private Vector3 bucketPosition;
    // Start is called before the first frame update
    
    private void Awake()
    {
        _inputs = InputProvider.instance.BucketMiniGame;
        _charInputs = InputProvider.instance.CharacterActionMap;
        _charInputs.Jump.Enable();
        _charInputs.Walk.Enable();
        _inputs.Crank.Disable();
    }

    public void OnEnable() {
        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
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


    private void OnDisable()
    {
        _inputs.Crank.performed -= Crank;
    }


    private void Crank(InputAction.CallbackContext cb)
    {
        rb.AddForce(pullForce * Vector2.up, ForceMode2D.Impulse);
    }

    void Start()
    {
        bucketPosition = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z);
        top.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider == bottom) {
            bucketFull = true;
            spriteRenderer.sprite = bucketFullSprite;
            // start for the player
            top.enabled = true;
            _inputs.Crank.Enable();
        }
        if (collision.collider == top) {
            //TODO: give bucket to player
            _charInputs.Jump.Enable();
            _charInputs.Walk.Enable();
            _inputs.Crank.Disable();
            rb.gravityScale = 0f;
            top.enabled = false;
            transform.position = new Vector3(
                bucketPosition.x,
                bucketPosition.y,
                bucketPosition.z);
            // TODO: disable image
        }
    }

    public void PreStartGame() {
        spriteRenderer.sprite = bucketEmptySprite;
        _charInputs.Jump.Disable();
        _charInputs.Walk.Disable();
        transform.position = new Vector3(
            bucketPosition.x,
            bucketPosition.y,
            bucketPosition.z);
        top.enabled = false; // will be set to true when we hit the bottom
        rb.gravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
