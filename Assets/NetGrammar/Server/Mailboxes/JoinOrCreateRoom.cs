namespace NetGrammar.Server.Mailboxes
{
    [ServerMailbox]
    public class JoinOrCreateRoom : SimpleServerMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.JoinOrCreateRoom;

        protected override MailboxGrammar Clone()
        {
            return new JoinOrCreateRoom();
        }

        public override void HandleMessage(byte[] data, ushort prefix)
        {
            // Expectation is an eight-byte message, {0-7: int ID}
            var code = NetBits.ToInt64(data);
            if (NetRoom.GetRoom(code) == null)
                NetRoom.NewRoom(code).JoinClient(ParentConnection.ID);
            else
                NetRoom.GetRoom(code).JoinClient(ParentConnection.ID);
        }
    }
}