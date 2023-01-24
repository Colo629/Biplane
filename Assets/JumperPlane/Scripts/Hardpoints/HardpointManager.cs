using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardpointManager : MonoBehaviour
{
    public Tracker tracker;
    [SerializeField]
    List<Hardpoint> hardPoints = null;
    
    // Start is called before the first frame update
    void Start()
    {
        tracker = gameObject.GetComponent<Tracker>();
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
                    break;
                }
                else continue;
            }
        }
    }
}
