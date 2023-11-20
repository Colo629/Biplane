using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetGrammar.Client;

public class Balling : MonoBehaviour
{
    public int Killstreak { get => _killstreak; set 
        {
            KillstreakUpdate(value);
        } 
    }
    private int _killstreak;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void KillstreakUpdate(int killstreak)
    {
        _killstreak = killstreak;
        if (ClientGrammar.IsConnected)
        {
            ClientGrammar.SendData(NetGrammar.NetDefs.MailPrefix.Balling, killstreak);
        }
    }
}
