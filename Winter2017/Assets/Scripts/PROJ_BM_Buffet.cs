using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROJ_BM_Buffet : MonoBehaviour {

    public int teamAssignment;
    public GameObject myOwner;
    public float lifeTimer = 0.25f;
    public int strength = 20;
    public int upStrength = 7;

    void Update()
    {
        KillSelf();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerHealth>().GetTeamAssignment() != teamAssignment)
        {
            float xComponent = other.transform.position.x - myOwner.transform.position.x;
            float zComponent = other.transform.position.z - myOwner.transform.position.z;
            Vector3 pushDirection = new Vector3(xComponent, 0.0f, zComponent);
            pushDirection.Normalize();
            Debug.Log(pushDirection);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(pushDirection * strength, ForceMode.Impulse);
            rb.AddForce(Vector3.up * upStrength, ForceMode.Impulse);
        }
    }

    public void SetTeam(int theTeam)
    {
        teamAssignment = theTeam;
    }

    public void SetOwner(GameObject theOwner)
    {
        myOwner = theOwner;
    }

    void KillSelf()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
