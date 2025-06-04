using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;
using Valve.VR;

public class InteractableGun : GunScript
{
    public Grabbable grabbable;
    public SteamVR_Action_Single leftTrigger;
    private bool grabbed;
    private bool leftTriggerPull;
    // Start is called before the first frame update
    protected override void Start()
    {
        ap = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        CheckForGrab();
        TriggerStatus();
        CheckForFire();
    }

    void CheckForGrab()
    {
        if (grabbable.GetHeldBy().Count > 0)
        {
            grabbed = true;
            return;
        }
        else
        {
            grabbed = false;
            return;
        }
    }
    void TriggerStatus()
    {
        if (leftTrigger.axis > 0.7)
        {
            leftTriggerPull = true;
            return;
        }
        else
            leftTriggerPull = false;

    }

    protected override void CheckForFire()
    {
        float fireDelay = 1 / (fireRPM / 60);
        float timeSinceFired = Time.time - lastFiredTime;

        if (grabbed & leftTriggerPull)
        {
            while (timeSinceFired > fireDelay)
            {
                float skippedTime = timeSinceFired-fireDelay; //for projectile advancement
                timeSinceFired -= fireDelay;
                if (ammo <= 0) { firing = false; return; }
                ammo -= 1;
                lastFiredTime = Time.time;
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
}
