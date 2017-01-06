using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROJ_BM_SacredWind : MonoBehaviour
{

    float lifeTimer = 10.0f;
    public float speed = 45.0f;
    public int teamAssignment;
    public GameObject owner;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile forward
        transform.position += transform.forward * speed * Time.deltaTime;
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTeam(int teamNum)
    {
        teamAssignment = teamNum;
    }

    public void SetOwner(GameObject theOwner)
    {
        owner = theOwner;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            if (ph.GetTeamAssignment() != teamAssignment)
            {
                ph.TakeDamage(50);
            }
            else if (ph.GetTeamAssignment() == teamAssignment)
            {
                if (other.gameObject != owner)
                {
                    ph.GainHealth(100);
                }
                else
                {
                    ph.GainHealth(25);
                }
            }
        }
    }
}
