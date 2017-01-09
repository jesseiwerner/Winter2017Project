using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROJ_BM_GodsBreath : MonoBehaviour {

    public int teamAssignment;
    public float boostStrength = 1.5f;
    public float boostDuration = 2.0f;
    public float lifeTimer = 0.25f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        KillSelf();
	}

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        MovementController mc = other.GetComponent<MovementController>();
        if (ph != null)
        {
            if (ph.GetTeamAssignment() == teamAssignment)
            {
                mc.ChangeSpeed(boostStrength, boostDuration);
            }
        }
    }
    public void SetTeam(int teamNum)
    {
        teamAssignment = teamNum;
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
