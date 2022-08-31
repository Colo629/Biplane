using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequipMover : MonoBehaviour
{
    public Transform requipPosition;
    public AirshipLandingChecker landingChecker;
    private float journeyTime;
    public float totalTransitionTime;
    private bool parentSet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (landingChecker.requip)
        {
            RequipMove();
        }
        if(landingChecker.requip == false)
        {
            journeyTime = 0;
            landingChecker.landedPlane.transform.parent = null;
        }
    }
    void RequipMove()
    {
        CalculateTravelPercent();
        if (!parentSet)
        {
            SettingParent();
        }
        landingChecker.landedPlaneRB.isKinematic = true;
        landingChecker.landedPlane.transform.position = Vector3.Lerp(landingChecker.landedPlane.transform.position, requipPosition.position, journeyTime);
        landingChecker.landedPlane.transform.rotation = Quaternion.Lerp(landingChecker.landedPlane.transform.rotation, requipPosition.rotation, journeyTime);
    }
    void CalculateTravelPercent()
    {
        
        journeyTime += Time.deltaTime;
        if(journeyTime > totalTransitionTime)
        {
            journeyTime = totalTransitionTime;
        }
        //percentage of total distance
        journeyTime /= totalTransitionTime;

    }
    void SettingParent()
    {
        landingChecker.landedPlane.transform.SetParent(requipPosition , true);
    }
}
