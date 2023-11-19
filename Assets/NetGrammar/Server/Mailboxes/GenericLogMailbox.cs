using UnityEngine;

namespace NetGrammar.Server.Mailboxes
{
    [ServerMailbox]
    public class GenericLogMailbox : MailboxGrammar
    {
        public override bool SelectMessage(ushort messagePrefix)
        {
            return true;
        }

        protected override MailboxGrammar Clone()  // You *HAVE* to override this or nothing works
        {
            return new GenericLogMailbox();
        }

        public override void HandleMessage(byte[] data, ushort prefix)
        {
            //Debug.Log($"Message received with prefix \"{prefix}\"");
        }
    }
}
