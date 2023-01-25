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
    public float beamRadius;
    string[] collidableLayers = { "trackable", "obstacle" };
    private int layersToCheck;
    public SteamVR_Action_Single trackTrigger;
    // Start is called before the first frame update
    void Start()
    {
        layersToCheck = LayerMask.GetMask(collidableLayers);
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
        RaycastHit hit;
        Ray lookRay = new Ray(playerHead.position , playerHead.forward);
        if (Physics.SphereCast(lookRay, beamRadius, out hit, beamRange, layersToCheck))
        {
            if (hit.collider.gameObject.tag == "trackable")
            {
                trackedTarget = hit.collider.gameObject;
            }
            return;
        }
        else ForgetTarget();
    }
    void ForgetTarget()
    {
        trackedTarget = null;
    }

}
