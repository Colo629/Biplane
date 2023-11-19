namespace NetGrammar.Server
{
    /// <summary>
    /// A simple connection which will listen on the default TCP port for a single message prefix.
    /// </summary>
    public class SimpleServerMailbox : MailboxGrammar
    {
        protected virtual ushort MailboxPrefix => 0;

        public override bool SelectMessage(ushort messagePrefix)
        {
            // The simple connection class only cares about whether the preset prefix is identical.
            return messagePrefix == MailboxPrefix;
        }

        protected override MailboxGrammar Clone()
        {
            // Yes, I realize this is strange
            return new SimpleServerMailbox();
        }
    }
}

