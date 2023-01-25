using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{

    public float missileCount;

    private CockpitManager cockpitManager;
    private HardpointManager hardpointManager;
    


    void Start()
    {
        cockpitManager = GetComponentInParent<CockpitManager>();
        hardpointManager = GetComponentInParent<HardpointManager>();
    }

    // Update is called once per frame
    void Update()
    {
   

    }
    void DisplayMissileCount()
    {

    }
}
