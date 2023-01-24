using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class JumperManager : MonoBehaviour
{
    public bool jump;
    public bool wingSwept;
    public SteamVR_Action_Boolean sweepWing;
    public GameObject aeroDynamics;
    public bool wingsDestroyed;
    public float wingSweepTime;
    public bool gimball;
    public SteamVR_Action_Boolean fireMissile;
    private HardpointManager hardpointManager;
    // Start is called before the first frame update  
    void Start()
    {
        hardpointManager = gameObject.GetComponent<HardpointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sweepWing.GetStateDown(SteamVR_Input_Sources.Any) == true)
        {
            SweepWings();
        }
        //Aero = aerodynamics
        if(fireMissile.GetStateDown(SteamVR_Input_Sources.Any) == true)
        {
            hardpointManager.FireLoadedHardpoint();
        }
        ManageAero();
    }

    public void JumpChecker()
    {
        if(jump == true & wingSwept != true)
        {
            DestroyWings();
        }
    }
    public void SweepWings()
    {
        if(wingSwept == false)
        {
            wingSwept = true;
            gimball = true;
            return;
        }
        if (wingSwept == true)
        {
            wingSwept = false;
            gimball = false;
            return;
        }

    }
    public void ManageAero()
    {
        if(wingSwept == true)
        {
            aeroDynamics.SetActive(false);
        }
        else if(wingsDestroyed == false)
        {
            aeroDynamics.SetActive(true);
        }
    }
    public void DestroyWings()
    {
        aeroDynamics.SetActive(false);
        gimball = false;
        wingsDestroyed = true;
    }
    public void SelectedMissile()
    {

    }
   
    
    
}
