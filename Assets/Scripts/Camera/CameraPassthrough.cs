using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPassthrough : MonoBehaviour
{
    public Transform realCockpitCamera;

    public Transform fakeCockpitCamera;

    public Transform realCockpitAnchor;

    public Transform fakeCockpitAnchor;

    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        SetAnchors();
        SetFov();
    }

    // Update is called once per frame
    void Update()
    {
        
        AdjustCameraPositions();
        AdjustCameraRotations();
    }
    void SetFov()
    {
        fakeCockpitCamera.GetComponent<Camera>().fieldOfView = realCockpitCamera.GetComponent<Camera>().fieldOfView;
    }
        

    private void AdjustCameraPositions()
    {
        fakeCockpitCamera.localPosition = realCockpitCamera.localPosition;
    }

    private void AdjustCameraRotations()
    {
        fakeCockpitCamera.localEulerAngles = realCockpitCamera.localEulerAngles;
    }

    private void SetAnchors()
    {
        fakeCockpitAnchor.localEulerAngles = player.localEulerAngles;
        var localPosition = player.localPosition;
        fakeCockpitAnchor.localPosition = localPosition;
        realCockpitAnchor.localPosition = localPosition;
    }
}
