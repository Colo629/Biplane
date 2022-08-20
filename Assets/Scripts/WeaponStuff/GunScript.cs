using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public CockpitManager cm;
    public GameObject bullet;
    public float dispersionValue;
    private bool fired;
    public float fireRPM;
    public bool startedRoutine;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(cm.rightTriggerPull == true & !fired)
        {
            StartCoroutine(Shoot());

        }
    }
    public void fireGun()
    {
        if (cm.ammoCount > 0)
        {
            Quaternion aimVector = Quaternion.RotateTowards(transform.rotation, Random.rotation, dispersionValue);
            GameObject instBullet = Instantiate(bullet, transform.position, aimVector) as GameObject;
            cm.ammoCount -= 1f;
            //edditted out the doubleshoot code
          /*  Quaternion aimVector1 = Quaternion.RotateTowards(transform.rotation, Random.rotation, dispersionValue); 
            GameObject instBullet1 = Instantiate(bullet, transform.position, aimVector1) as GameObject;
            cm.ammoCount -= 1f;*/
        }
    }
    IEnumerator Shoot()
    {
        fireGun();
        fired = true;
        yield return new WaitForSeconds(1 / (fireRPM / 60));
        fired = false;
    }
}
