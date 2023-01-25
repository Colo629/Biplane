using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardpointManager : MonoBehaviour
{
    public Tracker tracker;
    [SerializeField]
    List<Hardpoint> hardPoints = null;
    public float antiJumpMissilesRemaining;
    public Material hardPointLoaded;
    public Material hardPointEmpty;
    
    // Start is called before the first frame update
    void Start()
    {
        tracker = gameObject.GetComponent<Tracker>();
        foreach (Hardpoint hardpoint in hardPoints)
        {
            hardpoint.hudMarker.material = hardPointLoaded;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void FireLoadedHardpoint()
    {
        if(tracker.trackedTarget != null)
        {
            foreach (Hardpoint hardpoint in hardPoints)
            {
                if (hardpoint.payloadDropped == false)
                {
                    hardpoint.LaunchPayload(tracker.trackedTarget);

                    hardpoint.hudMarker.material = hardPointEmpty; //probably make this more general after we add multiple missile types but for testing i'll leave it as is
                    break;
                }
                else continue;
            }
        }
    }
    public void SelectedMissile()
    {

    }
    public void Rearm()
    {

    }
}
