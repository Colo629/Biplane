namespace NetGrammar.Server.Mailboxes
{
    [ServerMailbox]
    public class JoinRoomMailbox : SimpleServerMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.JoinRoom;

        protected override MailboxGrammar Clone()
        {
            return new JoinRoomMailbox();
        }

        public override void HandleMessage(byte[] data, ushort prefix)
        {
            // Expectation is an eight-byte message, {0-7: int ID}
            var code = NetBits.ToInt64(data);
            NetRoom.GetRoom(code).JoinClient(ParentConnection.ID);
        }
    }
}