using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public DamageScript damageScr;
    private Material thisMaterial;
    private Renderer thisRenderer;
    // Start is called before the first frame update
    void Start()
    {
        thisRenderer = GetComponent<Renderer>();
        damageScr = GetComponentInParent<DamageScript>();
        thisMaterial = thisRenderer.material;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateColor();
    }
    public void UpdateColor()
    {
        if(damageScr == null) { return; }
        thisMaterial.color = damageScr.storedColor;
    }
}
