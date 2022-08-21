using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostScript : MonoBehaviour
{
    public ButtonScript bs;
    public AircraftPhysics ap;
    public float cooldownTime = 60f;
    private bool coolingDown;
    public float boostThrust = 1.2f;
    public float penaltyThrust = 0.8f;
    public float boostDuration = 20f;
    public float penaltyTime = 10f;
    private float defaultThrust;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        defaultThrust = ap.thrust;
        ap = transform.GetComponent<AircraftPhysics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bs.buttonPressed == true & !active & !coolingDown)
        {
            Boost();
        }
        if (coolingDown)
        {
            bs.buttonPressed = false;
        }
    }
    void Boost()
    {
        ap.thrust *= boostThrust;
        active = true;
        StartCoroutine(BoostTime());
    }
    IEnumerator BoostTime()
    {

        yield return new WaitForSeconds(boostDuration);
        StartCoroutine(Cooldown());
        active = false;
        
    }
    IEnumerator Cooldown()
    {
        StartCoroutine(PenaltySpeed());
        coolingDown = true;
        yield return new WaitForSeconds(cooldownTime);
        coolingDown = false;
        

    }
    IEnumerator PenaltySpeed()
    {
        ap.thrust *= penaltyThrust;
        yield return new WaitForSeconds(penaltyTime);
        ap.thrust = defaultThrust; 
    }
}
