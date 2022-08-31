using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequipMover : MonoBehaviour
{
    public Transform requipPosition;
    public Transform plane;
    public AirshipLandingChecker landingChecker;
    public float transitionSpeed;

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
    }
    void RequipMove()
    {
        transitionSpeed *= Time.deltaTime;
        plane.position = Vector3.Lerp(plane.position, requipPosition.position, transitionSpeed);
    }
}
