using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public Rigidbody rb;
    public Transform cameraTransform;
    public float moveSpeed = 5;
    float originalSpeed;
    public float turnSpeed = 50;
    public float verticalCameraSpeed = -4;
    public float jumpHeight = 100;
    public GameObject cameraAnchor;
    public GameObject theGroundedChecker;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = cameraAnchor.GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        originalSpeed = moveSpeed;
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
        // Horizontal Rotation For the Character
        Quaternion newRotation = Quaternion.identity;
        newRotation.eulerAngles = new Vector3(0, Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * newRotation);

        // Vertical Rotation for the Camera
        cameraTransform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime * verticalCameraSpeed, 0, 0));
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && theGroundedChecker.GetComponent<GroundedChecker>().GetIsGrounded())
        {
            rb.AddForce(Vector3.up * jumpHeight * Time.deltaTime, ForceMode.Impulse);
        }
    }

    //I'll think about this more later I'm lazy right now
    public void ChangeSpeed(float strength, float duration)
    {
        float newSpeed = moveSpeed * strength;
        float timer = 0.0f;
        timer += Time.deltaTime;
        if (timer <= duration)
        {
            moveSpeed = newSpeed;
        }
        else if (timer > duration)
        {
            moveSpeed = originalSpeed;
        }
    }
}
