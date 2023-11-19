namespace NetGrammar.Server.Mailboxes
{
    [ServerMailbox]  // Marks this particular class as an endpoint for messages
    public class ExampleServerMailbox : SimpleServerMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.Example;
        // You set this prefix to decide which messages will go here
        // In this case, any message beginning with 100 will be grabbed
                                                        
        protected override MailboxGrammar Clone()  // You *HAVE* to override this or nothing works
        {
            // Don't think about it, you'll gain permanent warp.
            return new ExampleServerMailbox();
        }

        public override void HandleMessage(byte[] data, ushort prefix)
        {
            // You can ignore the prefix for simple connections!
            // Do something with the data here!
            // It'll be received as the same chunk that was sent
        }
    
        private void SomeFunctionThatWantsToSendData()
        {
            // Sending data is easy!
            SendData(new byte[10], MailboxPrefix);
            // This would send this new empty byte array with a prefix of 100 to the client
        }
    }
}
