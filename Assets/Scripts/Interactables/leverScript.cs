using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class leverScript : MonoBehaviour

{
  
    // Start is called before the first frame update
    public GameObject reference;
    public GameObject hand;
    public GameObject handParent;
    public bool grabbed;
    public SteamVR_Action_Boolean grabObject;
    public Vector3 output;
    public SteamVR_Input_Sources rightLeverSon;
    public static float multiplier = 0.5f;
    public bool mirror;
    public bool armTriggerR;
    public bool armTriggerL;
    public bool runOnce;
    public float outputX;
    public float outputY;
    public float timeToZero = 5;
    private float timeLeft;

    
    
    //6
    
    
    
    
    
    
    
    
    void OnTriggerStay(Collider other)
    {
        if(grabObject.GetStateDown(rightLeverSon))
        {
            grabbed = true;
            hand = other.gameObject;
        }
    }

    void Start()
    {
        timeLeft = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //"handParent" has to be manually set, this code is trash but I don't feel like reworking it right now
        //decided to switch handParent with "hand" revert code if it doesn't work
        //removed nulling hand parent in getstateup
        if(grabObject.GetStateUp(rightLeverSon))
        {
            grabbed = false;
            hand = null;
            runOnce = false;
        }
        if(!grabbed & !mirror)
        {
            ZeroLever();
        }
        if(grabbed)
        {
            timeLeft = 0;
            if (!mirror)
            {
            //rotate reference?
                armTriggerR = true;
                output = Vector3.ClampMagnitude(reference.transform.InverseTransformPoint(hand.transform.position),multiplier); //makes shit not able to rotate among other stuff
                output = new Vector3(0,Mathf.Clamp(output.y,-0.05f,multiplier/4),Mathf.Clamp(output.z,0,multiplier/2)); //Mathf.Clamp(output.x,-multiplier/2,0.25f
            if(handParent.transform.localEulerAngles.z < 180)
                {
                output.x = (handParent.transform.localEulerAngles.z )/210;
                }
            if(handParent.transform.localEulerAngles.z > 180)
                {
                    output.x = ((handParent.transform.localEulerAngles.z - 365))/210;
                }
           // Debug.Log(hand.transform.localEulerAngles);
            output.x = Mathf.Clamp(output.x,-0.15f,0.5f);
            outputX = output.x;
            outputY = output.y;
            transform.rotation = hand.transform.rotation;   //to change model rotation modify this
            }

            if(mirror)
            {
                    armTriggerL = true;
                            //rotate reference?
                    output = Vector3.ClampMagnitude(reference.transform.InverseTransformPoint(hand.transform.position),multiplier); //makes shit not able to rotate among other stuff
                    output = new Vector3(0,Mathf.Clamp(output.y,-multiplier/4,0.05f),Mathf.Clamp(output.z,0,multiplier/2)); //Mathf.Clamp(output.x,-multiplier/2,0.25f
                if(handParent.transform.localEulerAngles.z < 180)
                {
                    output.x = (handParent.transform.localEulerAngles.z + 93.7f)/210;
                }
                    if(handParent.transform.localEulerAngles.z > 180)
                {
                    output.x = ((handParent.transform.localEulerAngles.z - 365) + 93.7f)/210;
                }
                // Debug.Log(hand.transform.localEulerAngles);
                output.x = Mathf.Clamp(output.x,-0.15f,0.5f);
                outputX = output.x;
                outputY = output.y;
                transform.rotation = hand.transform.rotation;
                transform.localEulerAngles = new Vector3(0, 0, hand.transform.localEulerAngles.z);
                
            }


        }
            transform.position = reference.transform.TransformPoint(new Vector3(0, 0, output.z)); ; //moves the aesthetics?
                                // transform.localEulerAngles = new Vector3(output.x,transform.localEulerAngles.y,transform.localEulerAngles.z) ;
        //transform.localEulerAngles = output;
    }
    void ZeroLever()
    {
        if (timeLeft < timeToZero)
        {
            timeLeft += Time.fixedDeltaTime;
        }
        float timeLeftPercent = timeLeft / timeToZero;
        Vector3 neutralPoint = new Vector3(0.175f, 0, 0.125f);
        Vector3 reduceOutput = Vector3.Lerp(output, neutralPoint, timeLeftPercent);
        output.x= reduceOutput.x;
        output.z = reduceOutput.z;
    }
}
