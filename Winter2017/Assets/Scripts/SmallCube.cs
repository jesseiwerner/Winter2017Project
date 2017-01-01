using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCube : MonoBehaviour {

    float lifeTimer = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Move the projectile forward
        transform.position += transform.forward * 10* Time.deltaTime;
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
