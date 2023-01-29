using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    public GameObject target = null;
    public bool fireMissile;
    public bool thrust;
    public float delayTime = 1f;
    float checkedAngle;
    public float maximumAngle = 45;
    public Detonator detonator;
    public bool spool;
    public float delayCheck = 2;
    private Rigidbody thisRigidbody;
    private JumperManager jumperManager;
    private MissileVector missileVector;
    // Start is called before the first frame update
    void Start()
    {
        detonator = gameObject.GetComponent<Detonator>();
        thisRigidbody = gameObject.GetComponent<Rigidbody>();
        jumperManager = GetComponentInParent<JumperManager>();
        missileVector = GetComponent<MissileVector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(thrust == true)
        {
          if(delayCheck > 0)
            {
                delayCheck -= Time.fixedDeltaTime;
            }
          if(delayCheck <= 0)
            {
                CheckLock();
            }
        }
        if(spool == false & fireMissile)
        {
            spool = true;
            StartCoroutine(DelayLaunch());
            transform.parent = null;
        }
    }
    void CheckLock()
    {
        checkedAngle = Vector3.Angle(target.transform.position - transform.position , transform.forward);
        if(Mathf.Abs(checkedAngle) > maximumAngle)
        {
            if(Vector3.Distance(target.transform.position , transform.position) < missileVector.fuzeRange * 1.5)
            {
                detonator.Detonate();
            }
            else
            detonator.SelfDestruct();
        }
    }
    IEnumerator DelayLaunch()
    {
        yield return new WaitForSeconds(delayTime);
        thrust = true;
    }
}
