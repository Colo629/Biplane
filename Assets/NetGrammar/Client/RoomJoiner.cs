using System;
using System.Collections;
using System.Collections.Generic;
using NetGrammar;
using NetGrammar.Client;
using UnityEngine;

public class RoomJoiner : MonoBehaviour
{
    public long roomID = 10;
    private bool isJoined = false;
    
    public void JoinRoom()
    {
        ClientGrammar.SendData(NetDefs.MailPrefix.JoinOrCreateRoom, roomID);
    }

    private void Update()
    {
        if (ClientGrammar.IsConnected && !isJoined)
        {
            JoinRoom();
            isJoined = true;
        }
    }
}
