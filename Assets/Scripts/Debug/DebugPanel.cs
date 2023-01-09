using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{

    public float refreshRate = 5;

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

    private CockpitManager cockpitManager;


    private Vector3 prevPosition;
    private float nextRefresh;
    private float prevFrameTime;

    void Start()
    {
        cockpitManager = GetComponentInParent<CockpitManager>();
        prevPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < nextRefresh) { return; }


        airspeed = Vector3.Distance(transform.position, prevPosition) / (Time.time-prevFrameTime);
        prevPosition = transform.position;
        airspeedMPH = airspeed * 2.236936f;
        throttle = cockpitManager.leftPull;
        roll = cockpitManager.rightRotate;
        pitch = cockpitManager.rightPull;
        yaw = cockpitManager.leftRotatePress.axis.x;

        
        airspeedText.text = airspeed.ToString().Substring(0, Mathf.Clamp(4, 0, airspeed.ToString().Length)).Trim()+"m/s";
        airspeedMPHText.text = airspeedMPH.ToString().Substring(0, Mathf.Clamp(4, 0, airspeedMPH.ToString().Length)).Trim()+" MPH";
        pitchText.text = pitch.ToString().Substring(0, Mathf.Clamp(4, 0, pitch.ToString().Length)).Trim();
        yawText.text = yaw.ToString().Substring(0, Mathf.Clamp(4, 0, yaw.ToString().Length)).Trim();
        rollText.text = roll.ToString().Substring(0, Mathf.Clamp(4, 0, roll.ToString().Length)).Trim();
        throttleText.text = (throttle*100).ToString().Substring(0, Mathf.Clamp(3, 0, throttle.ToString().Length)).Trim()+"%";

        nextRefresh += 1 / refreshRate;
        prevFrameTime = Time.time;
    }
}
