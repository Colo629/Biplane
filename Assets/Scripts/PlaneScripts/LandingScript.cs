using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingScript : MonoBehaviour
{
    public float landingWeight = 50;
    public float startingWeight;
    public float maxWeight;
    public Rigidbody rb;
    public bool startLand;
    public AirplaneController ac;

    // Start is called before the first frame update
    void Start()
    {
        startingWeight = rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
       // if(ac.thrustPercent)
    }
    private void OnTriggerEnter(Collider other)
    {
        startLand = true;
    }
    private void OnTriggerExit(Collider other)
    {
        startLand = false;
    }
    private void FixedUpdate()
    {
        if(startLand == true)
        {
            Landing();
        }
    }
    void Landing()
    {
        rb.mass = landingWeight * Time.fixedDeltaTime;
        Mathf.Clamp(rb.mass , startingWeight, maxWeight);
    }

}
