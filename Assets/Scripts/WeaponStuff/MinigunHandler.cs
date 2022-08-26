using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunHandler : MonoBehaviour
{
    public bool facingDown;
    public GunScript gunScript;
    public float charge = 100;
    public float minCharge = 5f;
    private float maxCharge = 100;
    public float chargeRate = 33;
    public float drainRate = 100;
    public float originalFireRate;
    // Start is called before the first frame update
    void Start()
    {
        gunScript = transform.GetComponent<GunScript>();
        originalFireRate = gunScript.fireRPM;
    }

    // Update is called once per frame
    void Update()
    {
        
        CalculateFace();
        if(facingDown == false & gunScript.firing == true)
        {
            GunDrain();
        }
        if (facingDown == true)
        {
            GunCharge();
        }
        CalculateFireRate();

    }
    void CalculateFace()
    {
        if (transform.up.y < 0)
        {
            facingDown = true;
            return;
        }
        else
            facingDown = false;
    }
    void GunCharge()
    {
        charge += chargeRate * Time.deltaTime;
        if(charge > maxCharge)
        {
            charge = maxCharge;
        }
    }
    void GunDrain()
    {
        charge -= drainRate * Time.deltaTime;
        if(charge < minCharge)
        {
            charge = minCharge;
        }
    }
    void CalculateFireRate()
    {     
        gunScript.fireRPM = ((charge * originalFireRate)/100);
    }
}
