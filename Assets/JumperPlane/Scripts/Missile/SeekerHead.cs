using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerHead : MonoBehaviour
{
    public Vector3 targetOffset;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offsetAngle = transform.InverseTransformPoint(target.position);
        transform.LookAt(target, Vector3.up);
        targetOffset = offsetAngle;
    }
}
