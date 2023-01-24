using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardpointManager : MonoBehaviour
{
    public GameObject trackedTarget;
    [SerializeField]
    List<Hardpoint> hardPoints = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void FireLoadedHardpoint()
    {
        foreach(Hardpoint hardpoint in hardPoints)
        {
            if (hardpoint.payloadDropped == false)
            {
                hardpoint.LaunchPayload(trackedTarget);
                break;
            }
            else continue;
        }
    }
}
