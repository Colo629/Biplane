using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshipLandingChecker : MonoBehaviour
{
    public bool landed;
    public float requipTime = 3f;
    private float storedTime;
    public bool requip;
    // Start is called before the first frame update
    void Start()
    {
        storedTime = requipTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (landed)
        {
            StartRequipCount();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        storedTime = requipTime;
        landed = true;
    }
    private void OnTriggerExit(Collider other)
    {
        storedTime = requipTime;
        requip = false;
        landed = false;
    }
    void StartRequipCount()
    {
        storedTime -= Time.deltaTime;
        if(storedTime <= 0)
        {
            requip = true;
        }
    }
}
