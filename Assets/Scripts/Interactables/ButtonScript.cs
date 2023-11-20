using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool buttonPressed;
    private Material shaderMat;
    private Vector3 offScale;
    private Vector3 onScale;
    private float flipDelay = 0.5f;
    private float lastFlipTime;
    // Start is called before the first frame update
    void Start()
    {
        shaderMat = GetComponentInChildren<MeshRenderer>().material;
        shaderMat.EnableKeyword("_EMISSION");
        offScale = transform.localScale;
        onScale = new Vector3(offScale.x, offScale.y, offScale.z * 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)
        {
            shaderMat.SetColor("_EmissionColor", Color.red);
            transform.localScale = onScale;
        }
        else
        {
            shaderMat.SetColor("_EmissionColor", Color.black);
            transform.localScale = offScale;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < lastFlipTime + flipDelay) { return; }
        lastFlipTime = Time.time;
        if (buttonPressed == false)
        {
            buttonPressed = true;
            return;
        }
        if(buttonPressed == true)
        {
            buttonPressed = false;
            return;
        }
    }
}
