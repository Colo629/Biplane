using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAnimationManager : MonoBehaviour
{
    private CockpitManager cockpitManager;

    private Animator propAnimator;

    private static readonly int EngineOn = Animator.StringToHash("EngineOn");
    private bool currentEngineState;

    // Start is called before the first frame update
    void Start()
    {
        propAnimator = GetComponent<Animator>();
        cockpitManager = GetComponentInParent<CockpitManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cockpitManager.engineOn == currentEngineState) return;
        Debug.Log("changingstate");
        propAnimator.SetBool(EngineOn , cockpitManager.engineOn);
        currentEngineState = cockpitManager.engineOn;
    }
}
