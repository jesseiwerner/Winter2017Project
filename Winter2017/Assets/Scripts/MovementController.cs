using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public Rigidbody rb;
    public float moveSpeed = 5;
    public float turnSpeed = 50;
    public float jumpHeight = 100;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        WASDControls();
        MouseRotation();
        Jump();
    }

    void WASDControls()
    {
        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            newPosition += transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPosition -= transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPosition -= transform.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPosition += transform.right * moveSpeed * Time.deltaTime;
        }
        rb.MovePosition(newPosition);
    }

    void MouseRotation()
    {
        Quaternion newRotation = Quaternion.identity;
        newRotation.eulerAngles = new Vector3(0, Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * newRotation);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpHeight * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
