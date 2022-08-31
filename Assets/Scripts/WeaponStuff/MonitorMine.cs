using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorMine : DamageScript
{
    public GameObject preExplosion;
    public GameObject explosion;
    public float explosionDelay;
    public float speed = 1f;

    private bool exploding;
    private float startTime;



    private void Update()
    {
        MineLoop();
    }

    protected override void DamageableStart()
    {
        
    }

    protected virtual void MineLoop() 
    {

        if(PlayerTracker.tracker != null)
        {
            Transform player = PlayerTracker.tracker.transform;
            Vector3 moveVec = (player.position - transform.position).normalized * speed * Time.deltaTime;
            transform.position += moveVec;
            if ((player.position - transform.position).sqrMagnitude < 44 * 44) //bad bad bad
            {
                Explode();
            }
        }

        if (exploding)
        {
            if (Time.time > startTime + explosionDelay)
            {
                Explode();
            }
        }
    }

    public override void BulletDamageCalc(float damage)
    {
        if (!exploding)
        {
            DelayExplode();
        }
    }

    private void DelayExplode()
    {
        Instantiate(preExplosion, transform.position, transform.rotation, null);
        exploding = true;
        startTime = Time.time;
    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation, null);
        DebugMineSpawner.minesToSpawn += 1;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
}
