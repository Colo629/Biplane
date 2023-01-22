using UnityEngine;

public class FakeAero : MonoBehaviour
{
    Rigidbody rigidBody;
    public float aeroStrength = 0.8f; //value from 0 , 1
    private Vector3 aeroVelocity;
    private MissileManager missileManager;
    // Start is called before the first frame update

    //tired of messing with this, implemented scuffed version
    void Start()
    {
        missileManager = gameObject.GetComponent<MissileManager>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        //offsetAmmount = transform.InverseTransformDirection(rigidBody.velocity).normalized.z - rigidBody.velocity.normalized.magnitude;
        if(missileManager.fireMissile == true)
        {

        }
        ScuffedAero();
    }
    void ScuffedAero()
    {
        aeroVelocity = Vector3.Slerp(transform.InverseTransformDirection(rigidBody.velocity), Vector3.forward * rigidBody.velocity.magnitude, aeroStrength * Time.fixedDeltaTime);
        // rigidBody.velocity = transform.TransformDirection(aeroVelocity);
        rigidBody.velocity = transform.TransformDirection(aeroVelocity);
    }
}
