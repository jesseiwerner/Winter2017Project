using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHAR_BalindaMonroe : MonoBehaviour
{

    public Camera cam;
    public Rigidbody rb;
    public Transform transf;
    public MovementController moveCont;
    public GroundedChecker theGroundedChecker;

    //Abilities
    public GameObject PROJ_SacredWind;
    public GameObject PROJ_Buffet;
    public float floatStrength = -0.8f;
    public float buffetStrength = 15.0f;


    // Use this for initialization
    void Start()
    {
        gameObject.name = "PLAYER_Balinda";
        rb = GetComponent<Rigidbody>();
        transf = GetComponent<Transform>();
        theGroundedChecker = GetComponentInChildren<GroundedChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            BM_W1_SacredWind();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            BM_Passive_Float();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            BM_A1_Buffet();
        }
    }

    //BM = Balinda Monroe
    //W1 = Weapon 1 (some characters have more than one)
    void BM_W1_SacredWind()
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
            GameObject newProjectile = Instantiate(PROJ_SacredWind, transform.position, r);
            newProjectile.GetComponent<PROJ_BM_SacredWind>().SetOwner(gameObject);
            newProjectile.GetComponent<PROJ_BM_SacredWind>().SetTeam(gameObject.GetComponent<PlayerHealth>().GetTeamAssignment());
        }
        else
        {
            // Spawn the project from the center of hitbox to horizon
            GameObject newProjectile = Instantiate(PROJ_SacredWind, transform.position, cam.transform.rotation);
            newProjectile.GetComponent<PROJ_BM_SacredWind>().SetOwner(gameObject);
            newProjectile.GetComponent<PROJ_BM_SacredWind>().SetTeam(gameObject.GetComponent<PlayerHealth>().GetTeamAssignment());
        }
    }

    //A1 = Ability 1
    void BM_A1_Buffet() //Buffet
    {
        Vector3 distanceFromBalinda = transform.forward * 3.5f;
        rb.AddForce(Vector3.up * buffetStrength, ForceMode.Impulse);
        rb.AddForce(-1 * transf.forward * buffetStrength, ForceMode.Impulse);
        GameObject newProjectile = Instantiate(PROJ_Buffet, transform.position + distanceFromBalinda, transform.rotation);
        newProjectile.GetComponent<PROJ_BM_Buffet>().SetOwner(gameObject);
        newProjectile.GetComponent<PROJ_BM_Buffet>().SetTeam(gameObject.GetComponent<PlayerHealth>().GetTeamAssignment());
    }

    void BM_A2_PrayerCircle() //Prayer Circle
    {

    }

    void BM_A3_GodsBreath() //God's Breath
    {

    }

    void BM_Ult_Ascension() //Ascension [ULTIMATE]
    {

    }

    //TODO:  Logic isn't correct here
    void BM_Passive_Float()
    {
        if (rb.velocity.y < floatStrength)
        {
            rb.velocity = new Vector3(rb.velocity.x, floatStrength, rb.velocity.z);
        }
    }
}
