using System;
using UnityEngine;

namespace NetGrammar.Server.Mailboxes
{
    [ServerMailbox]
    public class PhotonRoomManage : SimpleServerMailbox
    {
        private enum Command : byte
        {
            None = 0,
            ClaimHost = 1,
            JoinRoom = 2,
            LeaveRoom = 3,
            CloseRoom = 4,
            KickClient = 5,
            CreateRoom = 6,
        }

        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.PhotonRoomManage;  
        protected override MailboxGrammar Clone()
        {
            return new PhotonRoomManage();
        }

        public override void HandleMessage(byte[] data, ushort prefix)
        {
            // Expectation is a five-byte message, {0: cmd, 1-4: int ID}
            var code = BitConverter.ToInt64(data, 1);
            switch ((Command)data[0])
            {
                case Command.None:
                    break;
                case Command.ClaimHost:
                    Debug.Log("Serverside host functionality not implemented, no action taken.");
                    break;
                case Command.LeaveRoom:
                    NetRoom.GetRoom(code).KickClient(ParentConnection.ID, "Left room.");
                    break;
                case Command.JoinRoom:
                    NetRoom.GetRoom(code).JoinClient(ParentConnection.ID);
                    break;
                case Command.CloseRoom:
                    NetRoom.GetRoom(code).Close();
                    break;
                case Command.KickClient:
                    if (CurrentRoom.RoomCode == -1) 
                        Debug.LogError("Tried to kick player while not in room.");
                    else CurrentRoom.KickClient(code);
                    break;
                case Command.CreateRoom:
                    NetRoom.NewRoom(code).JoinClient(ParentConnection.ID);
                    break;
                default:
                    Debug.LogError("Unknown photon room command received.");
                    break;
            }
        }
    }
}
