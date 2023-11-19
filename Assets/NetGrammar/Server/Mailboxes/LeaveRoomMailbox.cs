namespace NetGrammar.Server.Mailboxes
{
    [ServerMailbox]
    public class LeaveRoomMailbox : SimpleServerMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.LeaveRoom;

        protected override MailboxGrammar Clone()
        {
            return new LeaveRoomMailbox();
        }

        public override void HandleMessage(byte[] data, ushort prefix)
        {
            // Expectation is an eight-byte message, {0-7: int ID}
            var code = NetBits.ToInt64(data);
            NetRoom.GetRoom(code).KickClient(ParentConnection.ID, "Left room.");
        }
    }
}