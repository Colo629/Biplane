
namespace NetGrammar.Client.Mailboxes
{
    [ClientMailbox]
    public class BlankClientMailbox : SimpleClientMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.AudioData;

        protected override MailboxGrammar Clone()
        {
            return new BlankClientMailbox();
        }
        
        public override void HandleMessage(byte[] data, ushort prefix)
        {

        }
    }
}
