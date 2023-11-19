using System;
using System.Linq;
using UnityEngine;

namespace NetGrammar.Client
{
    public class MailboxGrammar
    {
        /// <summary>
        /// The port this mailbox will listen on.
        /// </summary>
        public int Port = NetDefs.GeneralPortTCP;

        /// <summary>
        /// Return a new instance of this particular class. Should be overriden by each new derived class.
        /// </summary>
        /// <returns></returns>
        protected virtual MailboxGrammar Clone()
        {
            return new MailboxGrammar();
        }

        public MailboxGrammar GetClone()
        {
            MailboxGrammar cloned = Clone();
            if (cloned.GetType() != this.GetType())
                throw new Exception("Derived mailbox types must override Clone() method to return derived class");
            return cloned;
        }

        /// <summary>
        /// If this function returns true for a message's prefix, then it will be sent to the handlers.
        /// </summary>
        /// <param name="messagePrefix"></param>
        /// <returns></returns>
        public virtual bool SelectMessage(ushort messagePrefix)
        {
            return false;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// This method is called with data from relevant messages from the associated port and client.
        /// Is called once for each message received since the last frame. Receive order is maintained
        /// for each client's messages, but messages from different clients may be called in a different order relative
        /// to each other.
        /// </summary>
        /// <param name="data">The data contained within a message, stripped of prefix.</param>
        /// <param name="prefix">The message's prefix.</param>
        public virtual void HandleMessage(byte[] data, ushort prefix)
        {
            Debug.Log($"Message of size {data.Count()} received with prefix <{prefix}>");
            // Override me.
        }

        /// <summary>
        /// Constructs and sends a message with the associated prefix. 
        /// </summary>
        /// <param name="data">The data to be sent.</param>
        /// <param name="prefix">The prefix the message will be labelled with.</param>
        protected void SendData(byte[] data, ushort prefix)
        {
            ClientGrammar.Connection.SendBytes(data, prefix);
        }

    }
}
