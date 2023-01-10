using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimballControl : MonoBehaviour
{
    private JumperManager jmprMngr;
    private Transform jumperShip;
    private CockpitManager cockpitManager;
    public float rollSens;
    public float yawSens;
    public float pitchSens;
    private Vector3 filteredRotationValues;


    //local x = forward
    //local z = roll
    //local y = yaw
    //pull goes 0 - 1
    // rotate is solid already

    // Start is called before the first frame update
    void Start()
    {
        cockpitManager = gameObject.GetComponent<CockpitManager>();
        jmprMngr = gameObject.GetComponent<JumperManager>();
        jumperShip = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(jmprMngr.gimball == true)
        {
            GimballMovement();
        }
    }
    public void GimballMovement()
    {
        FilterValues();
       transform.RotateAround(transform.position, transform.up , filteredRotationValues.y);
       transform.RotateAround(transform.position, transform.forward, filteredRotationValues.z);
       transform.RotateAround(transform.position, transform.right, filteredRotationValues.x);
    }
    public void FilterValues()
    {
        float rightPull = cockpitManager.rightPull;
        //convert y pull on lever from 0,1 to -1 to 1
        rightPull -= 0.5f;
        rightPull *= 2f;
        Vector3 cockpitCommand = new Vector3((rightPull * -1) * pitchSens, cockpitManager.leftRotate * yawSens, cockpitManager.rightRotate * rollSens);
        filteredRotationValues =  cockpitCommand * Time.deltaTime;
    }

}
