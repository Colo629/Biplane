using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableExplosion : Explosion
{
    public float maxDamage;
    public float minDamage;

    protected override void ExplosionConnect(Collider other)
    {
        damage = Random.Range(minDamage, maxDamage);
        base.ExplosionConnect(other);
        if (other.attachedRigidbody != null)
        {
            other.attachedRigidbody.AddExplosionForce(force, transform.position, explosionRadius);
        }
    }
}
