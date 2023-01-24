using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hardpoint : MonoBehaviour
{
    public GameObject payload;
    private GameObject firedObject;
    public bool payloadDropped;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LaunchPayload(GameObject trackedTarget)
    {
        payloadDropped = true;
        firedObject = Instantiate(payload, transform.position, transform.rotation);
        MissileManager firedObjectManager = firedObject.GetComponent<MissileManager>();
        payload.SetActive(false);
        firedObjectManager.fireMissile = true;
        firedObjectManager.target = trackedTarget;
    }
}
