using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FleaJump : MonoBehaviour
{
    public AircraftPhysics aircraftPhysics;
    public Rigidbody planeRigidBody;
    public float jumpThrust;
    public float maxJumpThrust;
    public leverScript jumpLever;
    public JumperManager jumperManager;
    public SteamVR_Action_Single jumpTrigger;
    private bool coolingDown;
    public float cooldownTime = 40f;
    public float jumpDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(jumpTrigger.axis > 0.7f & coolingDown == false)
        {
            Jump();
        }
        CalculateJumpThrust();
    }
    public void Jump()
    {
        StartCoroutine(JumpStart());
        StartCoroutine(JumpCooldown());
        if(jumperManager.wingSwept == false)
        {
            jumperManager.DestroyWings();
        }
    }
    public void CalculateJumpThrust()
    {
        // * 4 to normalize on 0 - 1
        jumpThrust = (jumpLever.output.z * 4) * maxJumpThrust;
    }
    public void ActivateFleaJump()
    {
        planeRigidBody.AddRelativeForce((new Vector3(0, 0, jumpThrust)), ForceMode.Impulse);
    }

    IEnumerator JumpCooldown()
    {           
        coolingDown = true;
        yield return new WaitForSeconds(cooldownTime);
        coolingDown = false;
    }
    IEnumerator JumpStart()
    {
        
        yield return new WaitForSeconds(jumpDelay);
        ActivateFleaJump();
    }
}


