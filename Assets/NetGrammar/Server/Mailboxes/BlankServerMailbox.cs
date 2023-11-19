namespace NetGrammar.Server.Mailboxes
{
    [ServerMailbox]
    public class BlankServerMailbox : SimpleServerMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.Example;

        protected override MailboxGrammar Clone()
        {
            return new BlankServerMailbox();
        }

        public override void HandleMessage(byte[] data, ushort prefix)
        {
            
        }
    }
}
