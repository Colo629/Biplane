using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Tracker : MonoBehaviour
{
    public SteamVR_Action_Boolean lockTarget;
    public Transform playerHead;
    public GameObject trackedTarget;
    public float beamRange;
    string[] collidableLayers = { "trackable", "obstacle" };
    private int layersToCheck;
    public SteamVR_Action_Single trackTrigger;
    public List<GameObject> validTargets;
    private Camera playerCam;
    public float maxTrackAngle;
    private float lowestAngle;
    public WorldManager worldManager;

    // Start is called before the first frame update
    void Start()
    {
        layersToCheck = LayerMask.GetMask(collidableLayers);
        playerCam = playerHead.gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (lockTarget.GetStateDown(SteamVR_Input_Sources.Any) == true)
        {
            LockTarget();
        }*/
        if (trackTrigger.axis > 0.7f)
        {
            LockTarget();
        }
    }
   void LockTarget()
    {
        CheckForValidTargets();
    }
    void ForgetTarget()
    {
        trackedTarget = null;
        lowestAngle = 0;
        validTargets.Clear();
    }
    void CheckForValidTargets()
    {
        ForgetTarget();
        foreach(GameObject visibleTarget in worldManager.visibleTargets)
        {
            DistanceCheck(visibleTarget);
            AngleCheck(visibleTarget);
        }
    }
    void DistanceCheck(GameObject viableTarget)
    {
        if (Vector3.Distance(viableTarget.transform.position, transform.position) <= beamRange)
        {
            validTargets.Add(viableTarget);
        }
        else
            RemoveTargetFromPool(viableTarget);
    }
    void AngleCheck(GameObject viableTarget)
    {
        float thisAngle = Vector3.Angle(viableTarget.transform.position - playerHead.position, playerHead.forward);
        if (thisAngle <= maxTrackAngle)
        {
            if(thisAngle < lowestAngle)
            {
                lowestAngle = thisAngle;
                SetTarget(viableTarget);
            }
            if(lowestAngle == 0)
            {
                lowestAngle = thisAngle;
                SetTarget(viableTarget);
            }
        }
        else
            RemoveTargetFromPool(viableTarget);
    }
    void RemoveTargetFromPool(GameObject viableTarget)
    {
        validTargets.Remove(viableTarget);
    }
    void SetTarget(GameObject viableTarget)
    {
        trackedTarget = viableTarget.gameObject;
        //make sure to clear angle after lock so this whole thing doesn't brick itself
    }

}
