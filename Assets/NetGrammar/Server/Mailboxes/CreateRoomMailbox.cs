using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace NetGrammar.Server.Mailboxes
{
    [ServerMailbox]
    public class CreateRoomMailbox : SimpleServerMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.CreateRoom;

        protected override MailboxGrammar Clone()
        {
            return new CreateRoomMailbox();
        }

        public override void HandleMessage(byte[] data, ushort prefix)
        {
            // Expectation is an eight-byte message, {0-7: int ID}
            var code = NetBits.ToInt64(data);
            if (NetRoom.GetRoom(code) == null)
                NetRoom.NewRoom(code);
            else
                Debug.LogWarning($"Duplicate room creation attempt with code {code}");
        }
    }
}