using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequipMover : MonoBehaviour
{
    public Transform requipPosition;
    public AirshipLandingChecker landingChecker = null;
    private float journeyTime;
    public float totalTransitionTime;
    private bool parentSet;
    private AnimationHandler thisAnimationHandler;
    private float journeyPercent;
    

    // Start is called before the first frame update
    void Start()
    {
        thisAnimationHandler = transform.GetComponent<AnimationHandler>();
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
            if(landingChecker.landedPlane.transform.parent != null)
            {
                landingChecker.landedPlane.transform.parent = null;
                parentSet = false;
            }
        }
        if(journeyTime == totalTransitionTime)
        {
            Launch();
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
        landingChecker.landedPlane.transform.position = Vector3.Lerp(landingChecker.landedPlane.transform.position, requipPosition.position, journeyPercent);
        landingChecker.landedPlane.transform.rotation = Quaternion.Lerp(landingChecker.landedPlane.transform.rotation, requipPosition.rotation, journeyPercent);
    }
    void CalculateTravelPercent()
    {
        
        journeyTime += Time.deltaTime;
        if(journeyTime > totalTransitionTime)
        {
            journeyTime = totalTransitionTime;
        }
        //percentage of total distance
        journeyPercent = journeyTime/totalTransitionTime;

    }
    void SettingParent()
    {
        parentSet = true;
        landingChecker.landedPlane.transform.SetParent(requipPosition , true);
    }
    void Launch()
    {
        thisAnimationHandler.TriggerAnimation();
    }
}
