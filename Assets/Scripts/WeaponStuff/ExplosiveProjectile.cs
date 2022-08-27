using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : BulletScript
{
    public GameObject explosionObject;
    public float delayTime = 1f;
    private float visVelocityY;
    // Start is called before the first frame update


    // Update is called once per frame
    void Start()
    {
        worldVelocity = transform.forward * velocity;
        //calculateBullet();
        startTime = Time.time;
    }

    public override void calculateBullet()
    {
        /* velocityY -= gravity * Time.deltaTime;
         Vector3 currentVelocity = worldVelocity + new Vector3(0, velocityY, 0);
         Vector3 newPos = transform.position + (currentVelocity * Time.deltaTime);*/
        RaycastHit hit;
        float deltaTime = Time.fixedDeltaTime;
        visualsTransform.localPosition = Vector3.zero;
        velocityY -= gravity * deltaTime;
        visVelocityY = velocityY;

        Vector3 currentVelocity = new Vector3();
        Vector3 newPos = new Vector3();

        currentVelocity = worldVelocity + new Vector3(0, velocityY, 0);
        newPos = transform.position + (currentVelocity * deltaTime);
        Physics.Raycast(transform.position, currentVelocity, out hit, (currentVelocity.magnitude * Time.deltaTime), (1 << 25) + (1 << 15), QueryTriggerInteraction.Ignore); //add bitshift in partenthesnes
        if (hit.collider != null)
        {
            Instantiate(explosionObject , transform.position , Quaternion.identity);
            Instantiate(hitExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        transform.position = newPos;
    }
    void Update()
    {
        if (Time.time - startTime > delayTime)
        {
            Instantiate(explosionObject, transform.position, Quaternion.identity);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        calculateBullet();
    }
}
