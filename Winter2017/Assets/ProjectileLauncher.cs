using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour {

    // Reference to camera used to aim.
    public Camera cam;

    // Projectile to spawn with direction toward crosshairs
    public GameObject spawn;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            // See if the crosshairs interset with a target
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            {
                // Get vector from current position to ray target.
                Vector3 orientation = new Vector3(hit.point.x - transform.position.x,
                    hit.point.y - transform.position.y,
                    hit.point.z - transform.position.z);

                // Create a rotation from the direction vector.
                Quaternion r = Quaternion.LookRotation(orientation);

                // Spawn the projectile in the direction of the ray.
                Instantiate(spawn, transform.position, r);
            }
            else
            {
                // Spawn the project from the center of hitbox to horizon
                Instantiate(spawn, transform.position, cam.transform.rotation);
            }
        }
    }
}
