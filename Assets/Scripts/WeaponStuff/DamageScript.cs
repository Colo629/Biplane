using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public AeroSurface aeroS;
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
       
    }

}
