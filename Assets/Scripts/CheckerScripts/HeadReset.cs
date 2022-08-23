using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HeadReset : MonoBehaviour
{
    public Transform cameraRig;
    public Transform targetPosition;
    private Vector3 storedRotationHead;
    private Vector3 storedCameraRotation;
    public Transform headset;
    public SteamVR_Action_Boolean resetButton;
    private Vector3 storedPosHead;
    private Vector3 storedHeadPos2;
    private Vector3 storedCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(resetButton.GetStateDown(SteamVR_Input_Sources.Any))
        {
            ResetHeadPos();
        }
    }
    public void ResetHeadPos()
    {

        Vector3 offset = targetPosition.position - headset.position;
        cameraRig.position += offset;


        //cameraRig.position = targetPosition.position;
        //cameraRig.position = headset.localPosition;
       // cameraRig.position -= headset.position;
        /*storedPosHead = headset.localPosition;
        storedCameraPos = cameraRig.localPosition;
        
        headset.position = targetPosition.position;*/
        //cameraRig.localPosition = storedPosHead - headset.localPosition;
       // headset.position = targetPosition.position;
        /*headset.position = targetPosition.position;
        storedPosHead -= headset.localPosition;
        cameraRig.localPosition += storedPosHead;
        headset.position = targetPosition.position;*/

    }
}
