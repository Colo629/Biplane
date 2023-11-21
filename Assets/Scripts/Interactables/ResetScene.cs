using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetGrammar.Client;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{

    private ButtonScript buttonScript;

    private bool flipBool;
    // Start is called before the first frame update
    void Start()
    {
        buttonScript = GetComponent<ButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonScript.buttonPressed == true & flipBool == false)
        {
            flipBool = true;
            ResetCurrentScene();
        }
    }

    private void ResetCurrentScene()
    {
        Debug.Log("triggeredrset");
        if (ClientGrammar.IsConnected)
        {
            
            ClientGrammar.SendData(NetGrammar.NetDefs.MailPrefix.ResetScene , 1 );
        }
    }
}
