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
    public AudioSource ap = null;

    public string debugFireButton = "DebugFire";
    protected float lastFiredTime;
    public bool firing;
    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        ap = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckForFire();
    }

    protected virtual void CheckForFire()
    {
        float fireDelay = 1 / (fireRPM / 60);
        float timeSinceFired = Time.time - lastFiredTime;

        if (cm.rightTriggerPull == true | Input.GetButton(debugFireButton))
        {
            while (timeSinceFired > fireDelay)
            {
                float skippedTime = timeSinceFired-fireDelay; //for projectile advancement
                timeSinceFired -= fireDelay;
                if (ammo <= 0) { firing = false; return; }
                ammo -= 1;
                lastFiredTime = Time.time;
                Debug.Log(skippedTime);
                if (firing != true) { firing = true; FireGun(0); break; }
                else { FireGun(skippedTime); }
            }
        }
        else
        {
            firing = false;
            ap.loop = false;
        }
    }

    protected void FireGun(float skippedTime)
    {
            Quaternion aimVector = Quaternion.RotateTowards(transform.rotation, Random.rotation, dispersionValue);
            BulletScript instBullet = Instantiate(bullet, transform.position, aimVector).GetComponent<BulletScript>();
            instBullet.SpinUpProjectile(cm.ac.rb.velocity, skippedTime);
            if(ap != null)
            {
                if(ap.isPlaying == false) { ap.Play(); }
                ap.loop = true;
            }
            
    }
}
