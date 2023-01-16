using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public SeekerHead seeker;
    public float gSens = 0.1f;
    private Rigidbody rigidBody;
    public float thrustForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       // transform.LookAt(transform.forward, Vector3.up);
        transform.RotateAround(transform.position, transform.up, seeker.targetOffset.x * Time.deltaTime * gSens);
        transform.RotateAround(transform.position, transform.right, seeker.targetOffset.y * Time.deltaTime * gSens);
        
    }
    private void FixedUpdate()
    {
        rigidBody.AddRelativeForce(Vector3.forward * thrustForce);
    }
}
