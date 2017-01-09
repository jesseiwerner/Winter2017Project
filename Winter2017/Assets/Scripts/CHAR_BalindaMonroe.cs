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
    public GameObject PROJ_GodsBreath;
    //Passive
    public float floatStrength = -0.8f;
    //Buffet
    public float buffetStrength = 15.0f;
    //Ascension
    public bool isAscending;
    public float ascensionHeight = 10.0f;
    public float ascensionYPos;
    public float landingYPos;
    float ascensionTimer = 8.0f;
    public float ascensionTimerValue = 8.0f;




    // Use this for initialization
    void Start()
    {
        gameObject.name = "PLAYER_Balinda";
        rb = GetComponent<Rigidbody>();
        transf = GetComponent<Transform>();
        theGroundedChecker = GetComponentInChildren<GroundedChecker>();
        ascensionTimer = 0.0f;
        landingYPos = gameObject.transform.position.y;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            BM_A3_GodsBreath();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ascensionYPos = gameObject.transform.position.y + ascensionHeight;
            ascensionTimer = ascensionTimerValue;
            isAscending = true;
        }
        BM_Ult_Ascension();
        Debug.Log(ascensionTimer);
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
        GameObject newProjectile = Instantiate(PROJ_GodsBreath, transform.position , transform.rotation);
        newProjectile.GetComponent<PROJ_BM_GodsBreath>().SetTeam(gameObject.GetComponent<PlayerHealth>().GetTeamAssignment());
    }

    void BM_Ult_Ascension() //Ascension [ULTIMATE]
    {
        if (ascensionTimer <= 0)
        {
            isAscending = false;
        }
        if (isAscending)
        {
            Vector3 flyingPos = new Vector3(gameObject.transform.position.x, ascensionYPos, gameObject.transform.position.z);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, flyingPos, Time.time / 5);
            rb.useGravity = false;
        }
        else if (!isAscending)
        {
            //Vector3 landingPos = new Vector3(gameObject.transform.position.x, landingYPos, gameObject.transform.position.z);
            //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, landingPos, Time.time / 5);
        }
        ascensionTimer -= Time.deltaTime;
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
