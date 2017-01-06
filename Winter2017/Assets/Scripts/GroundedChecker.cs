using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedChecker : MonoBehaviour {

    public bool isGrounded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
        {
            isGrounded = false;
        }
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }
}
