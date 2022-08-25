using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{

    public float airspeed;
    public float airspeedMPH;
    public float throttle;
    public float roll;
    public float pitch;
    public float yaw;

    public Text airspeedText;
    public Text airspeedMPHText;
    public Text pitchText;
    public Text yawText;
    public Text rollText;
    public Text throttleText;
    

    private Vector3 prevPosition;

    void Start()
    {
        prevPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        airspeed = Vector3.Distance(transform.position, prevPosition) / Time.deltaTime;
        airspeedMPH = airspeed * 2.236936f;
        throttle = CockpitManager.cockpitManager.leftPull;
        roll = CockpitManager.cockpitManager.rightRotate;
        pitch = CockpitManager.cockpitManager.rightPull;
        yaw = CockpitManager.cockpitManager.rightRotatePress.axis.x;

        
        airspeedText.text = airspeed.ToString().Substring(0, Mathf.Clamp(4, 0, airspeed.ToString().Length)).Trim()+"m/s";
        airspeedText.text = airspeedMPH.ToString().Substring(0, Mathf.Clamp(4, 0, airspeedMPH.ToString().Length)).Trim()+" MPH";
        pitchText.text = pitch.ToString().Substring(0, Mathf.Clamp(4, 0, pitch.ToString().Length)).Trim();
        yawText.text = yaw.ToString().Substring(0, Mathf.Clamp(4, 0, yaw.ToString().Length)).Trim();
        rollText.text = roll.ToString().Substring(0, Mathf.Clamp(4, 0, roll.ToString().Length)).Trim();
        throttleText.text = (throttle*100).ToString().Substring(0, Mathf.Clamp(3, 0, throttle.ToString().Length)).Trim()+"%";
    }
}
