using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public AeroSurface aeroS;
    public bool disabled;
    public bool test;
    // Start is called before the first frame update
    void Start()
    {
        aeroS = transform.GetComponent<AeroSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BulletDamageCalc(float damage)
    {
        if(aeroS.hp > 0)
        {
            aeroS.hp -= damage;
        }
        if(aeroS.hp < 0)
        {
            aeroS.hp = 0;
        }
        DisablePart();
       
    }
    public void DisablePart()
    {
        if(aeroS.hp <= 0f)
        {
            disabled = true;
        }
        if(aeroS.hp > 0)
        {
            disabled = false;
        }
        if (!disabled)
        {
            aeroS.gameObject.SetActive(true);
        }
        if (disabled)
        {
            aeroS.gameObject.SetActive(false);
        }
    }
}
