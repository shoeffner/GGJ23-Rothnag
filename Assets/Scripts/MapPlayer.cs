using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MapPlayer : MonoBehaviour {
 
    private float rotationSpeed = 4f;
    private float forwardSpeed = .1f;
    private Vector3 forwardVector = new Vector3(0f, 0f, 0f);

 
    // Use this for initialization
    void Start () {
    }
 
    // Update is called once per frame
    void Update () { 
        // get the movement
    }
 
    void FixedUpdate() {
        float moveLR = 0; //Input.GetAxis("Horizontal") * rotationSpeed;
        float moveFw = 0; //Input.GetAxis("Vertical");
        forwardVector.y = forwardSpeed * moveFw;
        transform.Translate(forwardVector);
        transform.Rotate(0, 0, -moveLR);
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