using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletScript : MonoBehaviour
{
    public Transform visualsTransform;
    public float velocity = 400f;
    public float drag;
    public ParticleSystem hitExplosion;
    public float damage;
    public float velocityY;
    private float visVelocityY;
    public Vector3 worldVelocity;
    public float gravity = 9.81f;
    public ParticleSystem explosion;
    public float startTime;
    public TrailRenderer bulletTrail;
    public float trailBuildTime = 1f;

    public bool visualOnly = false;
    private bool physFrame = false;
    private Vector3 lastCastPos;
    private float skipTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        worldVelocity = transform.forward * velocity;
        //calculateBullet();
        startTime = Time.time;
    }

    public virtual void SpinUpProjectile(Vector3 inheretedVelocity, float skippedTime = 0)
    {
        worldVelocity = (transform.forward * velocity) + inheretedVelocity;
        skipTime = skippedTime;
        //calculateBullet();
    }

    // Update is called once per frame
    public virtual void calculateBullet()
    {

        if (bulletTrail != null)
        {
            if (Time.time < startTime + trailBuildTime)
            {
                bulletTrail.widthMultiplier = (Time.time - startTime) / trailBuildTime;
            }
        }


        Vector3 currentVelocity = new Vector3();
        Vector3 newPos = new Vector3();

        if (Time.inFixedTimeStep | skipTime != 0)
        {
            float deltaTime = Time.fixedDeltaTime;
            if (skipTime != 0) { deltaTime = skipTime; }

            visualsTransform.localPosition = Vector3.zero;
            velocityY -= gravity * deltaTime;
            visVelocityY = velocityY;

            currentVelocity = worldVelocity + new Vector3(0, velocityY, 0);
            newPos = transform.position + (currentVelocity * deltaTime);

            RaycastHit hit;
            Physics.Raycast(transform.position, currentVelocity, out hit, (currentVelocity.magnitude * deltaTime), (1 << 25) + (1 << 15), QueryTriggerInteraction.Ignore); //add bitshift in partenthesnes
            HitCalculate(hit);
            transform.position = newPos;
            skipTime = 0;
        }
        else if (physFrame == false) //visuals movement
        {
            visVelocityY -= gravity * Time.deltaTime;
            currentVelocity = worldVelocity + new Vector3(0, visVelocityY, 0);
            newPos = transform.position + (currentVelocity * Time.deltaTime);
            visualsTransform.position = newPos;
        }
    }

    protected virtual void HitCalculate(RaycastHit hit)
    {
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "damageable")
            {
                hit.collider.gameObject.GetComponent<DamageScript>().BulletDamageCalc(damage);
                damage = 0;
                transform.position = hit.point;
            }
            Instantiate(hitExplosion, hit.point, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        physFrame = true;
        calculateBullet();
    }
    void Update()
    {
        calculateBullet();
        if (Time.time - startTime > 3f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
