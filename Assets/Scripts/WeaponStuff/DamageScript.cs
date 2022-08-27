using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public AeroSurface aeroS;
    public bool disabled;
    public bool test;
    public Gradient gradient;
    public Color storedColor;

    // Start is called before the first frame update
    void Start()
    {
        StartDamageable();
        UpdateGradient();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void UpdateGradient()
    {
        storedColor = gradient.Evaluate(aeroS.hp / aeroS.maxhp);
    }
    protected virtual void StartDamageable()
    {
        aeroS = transform.GetComponent<AeroSurface>();
    }
    public virtual void BulletDamageCalc(float damage)
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
        UpdateGradient();
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
