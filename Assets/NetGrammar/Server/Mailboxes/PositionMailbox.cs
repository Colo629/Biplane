namespace NetGrammar.Server.Mailboxes
{
    [ServerMailbox]
    public class PositionMailbox : SimpleServerMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.SinglePosition;

        protected override MailboxGrammar Clone()
        {
            return new PositionMailbox();
        }

        public override void HandleMessage(byte[] data, ushort prefix)
        {
            var clients = ThisClient.CurrentRoom?.GetClients();
            if (clients == null) return;
            foreach (NetClient client in clients)
            {
                client.Connection?.SendBytes(data, prefix);
            }
        }
    }
}