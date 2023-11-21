namespace NetGrammar.Server.Mailboxes
{
    [ServerMailbox]
    public class ResetSceneServerMailbox: SimpleServerMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.ResetScene;

        protected override MailboxGrammar Clone()
        {
            return new ResetSceneServerMailbox();
        }

        public override void HandleMessage(byte[] data, ushort prefix)
        {
            NetRoom room = CurrentRoom;
            if (room != null)
            {
                foreach (NetClient client in room.GetClients())
                {
                    client.Connection.SendData(MailboxPrefix , 1 );
                }
            }
        }
    }
}
