using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public CockpitManager cm;
    public GameObject bullet;
    public int ammo;
    public float dispersionValue;
    public float fireRPM;
    public bool startedRoutine;
    public AudioPlayer ap;
    private float lastFiredTime;
    private bool firing;
    

    // Start is called before the first frame update
    void Start()
    {
        ap = transform.GetComponent<AudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForFire();
    }

    public virtual void CheckForFire()
    {
        float fireDelay = 1 / (fireRPM / 60);
        float timeSinceFired = Time.time - lastFiredTime;

        if (cm.rightTriggerPull == true)
        {
            Debug.Log("pull detected");
            while (timeSinceFired > fireDelay)
            {
                Debug.Log("firewhile");
                float skippedTime = timeSinceFired; //for projectile advancement
                timeSinceFired -= fireDelay;
                if (ammo <= 0) { firing = false; return; }
                ammo -= 1;
                fireGun(skippedTime);
                lastFiredTime = Time.time;
                if (firing != true) { firing = true; break; }
            }
        }
        else
        {
            Debug.Log("noedetec");
            firing = false;
        }
    }
    public void fireGun(float skippedTime)
    {
        if (cm.ammoCount > 0)
        {
            Quaternion aimVector = Quaternion.RotateTowards(transform.rotation, Random.rotation, dispersionValue);
            BulletScript instBullet = Instantiate(bullet, transform.position, aimVector).GetComponent<BulletScript>();
            instBullet.SpinUpProjectile(cm.ac.rb.velocity, skippedTime);
            cm.ammoCount -= 1f;
            if(ap != null)
            {
                ap.Play();
            }
        }
    }
}
