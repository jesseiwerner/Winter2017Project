using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour {
    Rigidbody rb;
    float timeTraveled = 0;
    public float moveSpeed = 4;
    public float travelTime = 5;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = transform.position;
        timeTraveled += Time.deltaTime;
        timeTraveled %= 2*travelTime;
        if (timeTraveled < travelTime)
        {
            newPosition += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            newPosition -= transform.forward * moveSpeed * Time.deltaTime;
        }
        rb.MovePosition(newPosition);
    }
}
