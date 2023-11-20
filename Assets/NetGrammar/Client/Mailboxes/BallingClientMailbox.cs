
namespace NetGrammar.Client.Mailboxes
{
    [ClientMailbox]
    public class BallingClientMailbox : SimpleClientMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.Balling;

        protected override MailboxGrammar Clone()
        {
            return new BallingClientMailbox();
        }
        
        public override void HandleMessage(byte[] data, ushort prefix)
        {

        }
    }
}
