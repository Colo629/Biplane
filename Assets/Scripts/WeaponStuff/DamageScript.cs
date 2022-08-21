using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public float hp;
    public float maxhp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BulletDamageCalc(float damage)
    {
        if(hp > 0)
        {
            hp -= damage;
        }
        if(hp < 0)
        {
            hp = 0;
        }
    }

}
