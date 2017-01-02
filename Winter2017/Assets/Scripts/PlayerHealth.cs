using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int health = 1000;
    public int teamAssignment;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponentInChildren<TextMesh>().text = "" + health;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    public void TakeDamage(int damageAmt)
    {
        health = health - damageAmt;
    }

    public void GainHealth(int healthAmt)
    {
        health = health + healthAmt;
    }

    public int GetTeamAssignment()
    {
        return teamAssignment;
    }
}
