using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public CockpitManager cm;
    public ButtonScript bs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bs.buttonPressed == true)
        {
            cm.engineOn = true;
            return;
        }
        else
        {
            cm.engineOn = false;
            return;
        }
    }
}
