using System;
using NetGrammar.Client.Mailboxes;
using UnityEngine;

namespace NetGrammar.Client.Objects
{
    public class SimpleNetObject : MonoBehaviour
    {
        public int ID;
        public bool isMaster;
        private float sendTime = 10f;
        private void Awake()
        {
            PositionMailbox.NetObjects.Add(ID, this);
        }
        
        public virtual void SetPosition(Vector3 position, int movementType = 0)
        {
            if(isMaster == true)
            {
                return;
            }
            transform.position = position;
        }

        protected virtual Vector3 FindPosition()
        {
            return transform.position;
        }

        void Update()
        {
            if (Time.time > sendTime && isMaster || true)
            {
                Vector3 pos = FindPosition();
                sendTime = Time.time + 0.5f;
                ClientGrammar.SendData(NetDefs.MailPrefix.SinglePosition,
                    NetBits.ToInt32(NetBits.GetBytes(pos.x)),
                    NetBits.ToInt32(NetBits.GetBytes(pos.y)),
                    NetBits.ToInt32(NetBits.GetBytes(pos.z)),
                    ID,
                    0,
                    0
                );
            }
        }
    }
}
