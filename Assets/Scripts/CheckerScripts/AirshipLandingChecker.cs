using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshipLandingChecker : MonoBehaviour
{
    public bool landed;
    public float requipTime = 3f;
    private float storedTime;
    public bool requip;
    public Rigidbody landedPlaneRB = null;
    public GameObject landedPlane;
    private Collider storedCollider;
   
   
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
 
   public void StartRequipCount()
    {
        storedTime -= Time.deltaTime;
        if(storedTime <= 0)
        {
            requip = true;
        }
    }
    public void ResetRequip()
    {
        storedTime = requipTime;
        requip = false;
    }
}
