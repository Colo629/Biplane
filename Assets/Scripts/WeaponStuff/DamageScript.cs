using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public float hpTotal = 100;
    protected float currentHp = 100;
    public bool disabled;
    public Gradient gradient;
    public Color storedColor;

    // Start is called before the first frame update
    void Start()
    {
        //put things in DamageableStart() instead of Start() directly
        DamageableStart();
    }

    // Update is called once per frame
    void Update()
    {
        //put things in DamageableUpdate() instead of Update() directly
        DamageableUpdate();
    }

    public virtual float GetHealthCurrent()
    {
        return currentHp;
    }

    public virtual float GetHealthTotal()
    {
        return hpTotal;
    }

    protected virtual void DamageableUpdate()
    {

    }
    protected virtual void UpdateGradient()
    {
        storedColor = gradient.Evaluate(currentHp / hpTotal);
    }
    protected virtual void DamageableStart()
    {
        UpdateGradient();
    }
    public virtual void BulletDamageCalc(float damage)
    {
        if(currentHp > 0)
        {
            currentHp -= damage;
        }
        if(currentHp < 0)
        {
            currentHp = 0;
        }
        CheckDisabled();
        UpdateGradient();
    }
    public virtual void CheckDisabled()
    {
    
    }
}
