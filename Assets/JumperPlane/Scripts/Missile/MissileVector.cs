using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissileVector : MonoBehaviour
{
    public MissileManager missileManager;
    private Rigidbody targetRB;
    private Transform targetTransform;
    private Rigidbody missileRB;
    private Vector3 relativeVelocity;
    private bool fired;
    public float thrustForce = 20;
    private Vector3 targetDirection;
    private Vector3 angularVelocity;
    public float missileSens = 1;
    private Vector3 cross1;
    private Vector3 cross2;
    public float angularClamp = 1f;
    private Vector3 transformedAngle;
    public float drag = 0.02f;
    public float burnTime;
    public float angleDifference;
    public Rigidbody planeRB;
    private Detonator detonator;
    public float fuzeRange;
    
    // Start is called before the first frame update
    void Start()
    {
        detonator = gameObject.GetComponent<Detonator>();
        targetRB = missileManager.target.GetComponent<Rigidbody>();
        //planeRB = GetComponentInParent<Rigidbody>();
        targetTransform = missileManager.target.transform;
        missileRB = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (missileManager.fireMissile == true & !fired)
        {
            missileRB.velocity = planeRB.velocity;
            fired = true;
        }
        if(fired == true)
        {
            if(missileRB.isKinematic == true)
            {
                missileRB.isKinematic = false;
            }
            
            if(missileManager.thrust == true & burnTime > 0)    
            {
                Thrust();
            }
            GetAngularVelocity();
            AngleMissile();
            DistanceFuze();
        }
        
    }
    public void AngleMissile()
    {
        
        transformedAngle = transform.InverseTransformDirection(angularVelocity);
        //  angleDifference = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);
        angleDifference = Vector3.Angle(angularVelocity, transform.forward);
        Vector3 filteredAngle = Vector3.ClampMagnitude(transformedAngle, angularClamp) * Time.fixedDeltaTime * missileSens * Mathf.Abs(angleDifference * angleDifference);
        transform.RotateAround(transform.position , Vector3.up, filteredAngle.x);
        transform.RotateAround(transform.position , transform.right, -filteredAngle.y);
        transform.RotateAround(transform.position , transform.forward , -transform.localEulerAngles.z);
    }
    public void OnDrawGizmos()
    {
        if(targetTransform == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position , transform.position + targetDirection * 100);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(targetTransform.position, targetTransform.position + relativeVelocity * 100);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(targetTransform.position, targetTransform.position + angularVelocity * 100);
        Gizmos.DrawLine(transform.forward, transform.forward + transformedAngle);
    }
    public void GetAngularVelocity()
    {
        targetDirection = targetTransform.position - transform.position;
        relativeVelocity = targetRB.velocity - missileRB.velocity;
        cross1 = Vector3.Cross(targetDirection, relativeVelocity);
        cross2 = Vector3.Cross(cross1 , targetDirection);
        angularVelocity = cross2 * Vector3.Dot(relativeVelocity , cross2);
    }
    public void Thrust()
    {
        missileRB.AddRelativeForce(Vector3.forward * thrustForce);
        burnTime -= Time.fixedDeltaTime;
    }
    public void Drag()
    {
    //using built in drag on the rigidbody for now
    }
    public void DistanceFuze()
    {
        if(targetDirection.magnitude < fuzeRange)
        {
            detonator.Detonate();
        }
    }


}
