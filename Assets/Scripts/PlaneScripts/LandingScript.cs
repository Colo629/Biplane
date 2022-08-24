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
    public float weightClamp;

    // Start is called before the first frame update
    void Start()
    {
        startingWeight = rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        if(ac.thrustPercent > 0.9f)
        {
            startLand = false;
            TakeOff();
        }
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
        float weight = (landingWeight * Time.fixedDeltaTime) + rb.mass;
        rb.mass = Mathf.Clamp(weight , startingWeight, maxWeight);
    }
    void TakeOff()
    {
        startLand = false;
        rb.mass = startingWeight;
    }

}
