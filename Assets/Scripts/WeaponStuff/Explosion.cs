using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage;
    public float explosionRadius;
    public float force = 100f;
    private SphereCollider thisCollider;
    // Start is called before the first frame update
    void Awake()
    {
        thisCollider = gameObject.GetComponent<SphereCollider>();
        thisCollider.radius = explosionRadius;
        Debug.Log("Spooled");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        ExplosionConnect(other);
    }

    protected virtual void ExplosionConnect(Collider other)
    {
        if (other.gameObject.tag == "damageable")
        {
            other.gameObject.GetComponent<DamageScript>().BulletDamageCalc(damage);
        }
        Destroy(gameObject);
    }
}
