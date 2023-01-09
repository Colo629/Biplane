using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleaJump : MonoBehaviour
{
    public AircraftPhysics aircraftPhysics;
    Rigidbody planeRigidBody;
    public float jumpThrust;
    public float maxJumpThrust;
    public leverScript jumpLever;
    public JumperManager jumperManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    public void CalculateJumpThrust()
    {
        jumpThrust = jumpLever.outputX * maxJumpThrust;
    }
    public void ActivateFleaJump()
    {
        CalculateJumpThrust();
        planeRigidBody.AddForce((new Vector3(0, 0, jumpThrust)), ForceMode.Impulse);
    }
  
}
