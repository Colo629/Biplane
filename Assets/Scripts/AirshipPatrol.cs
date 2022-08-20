using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshipPatrol : MonoBehaviour
{
    public Rigidbody rbody;
    public float speedMetersPerSecond = 13.4f;
    public float turnTime = 180;
    private float turnAtTime = 0;

    private void Start()
    {
        if (rbody == null) { rbody = GetComponent<Rigidbody>(); }
        turnAtTime = Time.time + turnTime;
        rbody.mass = 10000;
        rbody.drag = 0;
        rbody.useGravity = false;
        rbody.isKinematic = false;
        rbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        rbody.velocity = transform.forward * speedMetersPerSecond;
        if (Time.time > turnAtTime)
        {
            turnAtTime = Time.time + turnTime;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
