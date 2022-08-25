using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CockpitManager : MonoBehaviour
{
    public leverScript rls;
    public leverScript lls;
    public float leftPull;
    public float leftRotate;
    public float rightPull;
    public float rightRotate;
    public bool rightTriggerPull;
    public bool leftGrabbed;
    public bool rightGrabbed;
    public AirplaneController ac;
    public SteamVR_Action_Single rightTrigger;
    public SteamVR_Action_Vector2 rightRotatePress;
    public float ammoCount;
    public bool engineOn;

    public static CockpitManager cockpitManager;

    private void Awake()
    {
        cockpitManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //x = rotation -0.15-0.5 y - rotation?
        //z = pull 0-0.25f 0.25 is towards the pilot not away
        //8
    }

    // Update is called once per frame
    void Update()
    {
        Throttle();
        FilterLeftData();
        FilterRightData();
        ControlSurfaces();
        TriggerStatus();

    }
    void FilterLeftData()
    {
        float rotate;
        rotate = (lls.output.x + 0.15f) * (100f / 65f);
        leftRotate = Mathf.Clamp(rotate, 0, 1f);
        leftRotate -= 0.5f;
        leftRotate *= 2f;
        float pull = lls.output.z * 4f;
        leftPull = Mathf.Clamp(pull, 0, 1);


    }
    void TriggerStatus()
    {
        if (rightTrigger.axis > 0.7 & rls.grabbed == true)
        {
            rightTriggerPull = true;
            return;
        }
        else
            rightTriggerPull = false;

    }
    void FilterRightData()
    {
        float rotate;
        rotate = (rls.output.x + 0.15f) * (100f / 65f);
        rightRotate = Mathf.Clamp(rotate, 0, 1f);
        rightRotate -= 0.5f;
        rightRotate *= 2f;
        float pull = rls.output.z * 4f;
        rightPull = Mathf.Clamp(pull, 0, 1);     
    }
    void Throttle()
    {
        if (!ac.testControl)
        {
            if (engineOn)
            {
                ac.thrustPercent = Mathf.Abs(leftPull - 1f);
            }
            if (!engineOn)
            {
                ac.thrustPercent = 0;
            }
        }
       
        
    }
    void ControlSurfaces()
    {
        ac.Pitch = (Mathf.Abs(rightPull - 1f) - 0.5f) * 2;
        ac.Roll = rightRotate;
        // ac.Yaw = leftRotate;
        ac.Yaw = rightRotatePress.axis.x;
        
        
    }
}
