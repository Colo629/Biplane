using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    //public GameObject explosionObject;
    public ParticleSystem explosionParticles;
    public GameObject explosionScripts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Detonate()
    {
        Instantiate(explosionScripts, transform.position, transform.rotation);
        Instantiate(explosionParticles, transform.position, transform.rotation);
        // ^^^ keep seperate of instantiate doesn't work
        Destroy(gameObject);
    }
}
