using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    public Transform shipWheel;
    public float lastRotation;
    public float totalRotation;

    public float output;
    // Start is called before the first frame update
    void Start()
    {
        lastRotation = shipWheel.localEulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CountRotation()
    {
        float currentRotation = shipWheel.localEulerAngles.z;
        //float differenceInRotation = currentRotation 
        if (currentRotation < lastRotation)
        {
            //totalRotation -= 
        }
    }
}
