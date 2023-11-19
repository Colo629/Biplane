using System.Collections.Generic;
using NetGrammar.Client.Objects;
using UnityEngine;

namespace NetGrammar.Client.Mailboxes
{
    [ClientMailbox]
    public class PositionMailbox : SimpleClientMailbox
    {
        public static readonly Dictionary<int, SimpleNetObject> NetObjects = new Dictionary<int, SimpleNetObject>();

        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.SinglePosition;

        protected override MailboxGrammar Clone()
        {
            return new PositionMailbox();
        }
        
        public override void HandleMessage(byte[] data, ushort prefix)
        {
            float[] posVec = NetBits.ToSingles(data, 0, 3);
            int[] objData = NetBits.ToInt32s(data, sizeof(float) * 3);

            int netObjectID = objData[0];
            int senderID = objData[1];
            int motionType = objData[2];
            
            Vector3 vec = new Vector3(posVec[0], posVec[1], posVec[2]);

            if (NetObjects.TryGetValue(netObjectID, out SimpleNetObject obj))
            {
                obj.SetPosition(vec, motionType);
            }
        }
    }
}