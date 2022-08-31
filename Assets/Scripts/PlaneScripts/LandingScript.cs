using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingScript : MonoBehaviour
{
    public float landingWeight = 300;
    public float startingWeight;
    public float maxWeight;
    public Rigidbody rb;
    public bool startLand;
    public AirplaneController ac;
    private Collider storedCollider;
    public AirshipLandingChecker airshipLandingChecker;
    public float weightClamp;
    private GameObject thisPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingWeight = rb.mass;
        thisPlane = rb.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(ac.thrustPercent > 0.9f)
        {
            startLand = false;
            RemoveAirshipVariables();
            TakeOff();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        storedCollider = other;
        startLand = true;
        SetAirshipVariables();
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
    private void FixedUpdate()
    {
        if(startLand == true)
        {
            Landing();
            airshipLandingChecker.StartRequipCount();
        }
    }
    void Landing()
    {
        float weight = (landingWeight * Time.fixedDeltaTime) + rb.mass;
        rb.mass = Mathf.Clamp(weight , startingWeight, maxWeight);
    }
    void TakeOff()
    {
        startLand = false;
        rb.mass = startingWeight;
    }
    void SetAirshipVariables()
    {
        airshipLandingChecker = storedCollider.GetComponent<AirshipLandingChecker>();
        airshipLandingChecker.landedPlaneRB = rb;
        airshipLandingChecker.landedPlane = thisPlane;
    }
    void RemoveAirshipVariables()
    {
        storedCollider = null;
        startLand = false;
        airshipLandingChecker = null;
        airshipLandingChecker.landedPlaneRB = null;
        airshipLandingChecker.landedPlane = null;
        airshipLandingChecker.ResetRequip();
    }

}
