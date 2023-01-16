using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    private MissileVector vectorChecker;
    private Rigidbody missileRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        vectorChecker = gameObject.GetComponent<MissileVector>();
        missileRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    void TestGuidance()
    {
       
    }
}
