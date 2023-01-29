using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    private CockpitManager cockpitManager;
    private HardpointManager hardpointManager;
    private JumperManager jumperManager;
    public MeshRenderer boostHudMarker;
    public Material boostReady;
    public Material boostCD;
    private Tracker tracker;
    public GameObject trackerHudMarker;
    public Transform trackerHudGraphics;
    private bool trackerHudOff;



    void Start()
    {
        cockpitManager = GetComponentInParent<CockpitManager>();
        hardpointManager = GetComponentInParent<HardpointManager>();
        tracker = GetComponentInParent<Tracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tracker.trackedTarget != null)
        {
            trackerHudOff = false;
            TrackerHud();
        }
        if(tracker.trackedTarget == null & trackerHudOff == false)
        {
            trackerHudMarker.SetActive(false);
        }
    }
    void TrackerHud()
    {
        trackerHudMarker.SetActive(true);
       // Vector3 direction = transform.position - tracker.trackedTarget.transform.position;
        trackerHudMarker.transform.LookAt(tracker.trackedTarget.transform.position);
    }
    public void SetBoostHud(bool cooldown)
    {
        if (cooldown)
        {
            boostHudMarker.material = boostCD;
        }
        else
            boostHudMarker.material = boostReady;
    }
}
