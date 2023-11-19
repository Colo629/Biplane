namespace NetGrammar.Client.Mailboxes
{
    [ClientMailbox]
    public class ClientDataMailbox : SimpleClientMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.AudioData;

        protected override MailboxGrammar Clone()
        {
            return new ClientDataMailbox();
        }
        
        public override void HandleMessage(byte[] data, ushort prefix)
        {
                
        }
    }
}