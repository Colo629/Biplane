namespace NetGrammar.Client
{
    public class SimpleClientMailbox : MailboxGrammar
    {
        protected virtual ushort MailboxPrefix => 0;

        public override bool SelectMessage(ushort messagePrefix)
        {
            return messagePrefix == MailboxPrefix;
        }

        protected override MailboxGrammar Clone()
        {
            return new SimpleClientMailbox();
        }
    }
}
