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
    float modifiedSpeed;

    public List<float> speedMod = new List<float>();
    public List<float> modDurations = new List<float>();

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = cameraAnchor.GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        originalSpeed = moveSpeed;
        AkSoundEngine.PostEvent("Play_TestSound", gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateSpeedModifier();
        WASDControls();
        MouseRotation();
        Jump();
    }

    void WASDControls()
    {
        // Adjust the speed for each speed mod on the character
        modifiedSpeed = moveSpeed;
        foreach (float mod in speedMod)
        {
            modifiedSpeed *= mod;
        }

        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            newPosition += transform.forward * modifiedSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPosition -= transform.forward * modifiedSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPosition -= transform.right * modifiedSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPosition += transform.right * modifiedSpeed * Time.deltaTime;
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

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && theGroundedChecker.GetComponent<GroundedChecker>().GetIsGrounded())
        {
            rb.AddForce(Vector3.up * jumpHeight * Time.deltaTime, ForceMode.Impulse);
        }
    }

    public void ChangeSpeed(float strength, float duration)
    {
        speedMod.Add(strength);
        modDurations.Add(duration);
    }

    void UpdateSpeedModifier()
    {
        for (int i = 0; i < speedMod.Count; i++)
        {
            modDurations[i] -= Time.deltaTime;
            if (modDurations[i] < 0)
            {
                modDurations.RemoveAt(i);
                speedMod.RemoveAt(i);
                i -= 1;
            }
        }
    }
}
