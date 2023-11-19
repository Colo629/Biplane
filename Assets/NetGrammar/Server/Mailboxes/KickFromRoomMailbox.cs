using UnityEngine;

namespace NetGrammar.Server.Mailboxes
{
    [ServerMailbox]
    public class KickFromRoomMailbox : SimpleServerMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.KickFromRoom;

        protected override MailboxGrammar Clone()
        {
            return new KickFromRoomMailbox();
        }

        public override void HandleMessage(byte[] data, ushort prefix)
        {
            // Expectation is an eight-byte message, {0-7: int ID}
            var code = NetBits.ToInt64(data);
            if (CurrentRoom.RoomCode == -1) 
                Debug.LogError("Tried to kick player while not in room.");
            else CurrentRoom.KickClient(code);
        }
    }
}