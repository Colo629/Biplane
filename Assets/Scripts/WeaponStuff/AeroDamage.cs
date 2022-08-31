using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AeroDamage : DamageScript
{
    public AeroSurface aeroS;

    protected override void DamageableStart()
    {
        aeroS = transform.GetComponent<AeroSurface>();
        UpdateGradient();
    }

    public override void CheckDisabled()
    {
        if (currentHp <= 0f)
        {
            disabled = true;
        }
        if (currentHp> 0)
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
