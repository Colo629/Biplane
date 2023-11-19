using System;
using System.Collections.Generic;
using UnityEngine;

namespace NetGrammar.Server.Mailboxes
{
    [ServerMailbox]
    public class PhotonIdMailbox : SimpleServerMailbox
    {
        private static readonly Dictionary<Int64, int> PhotonIDs = new Dictionary<Int64, int>();
        private static readonly Dictionary<int, Int64> ClientIDs = new Dictionary<int, Int64>();

        /// <summary>
        /// Get a photonID from a clientID
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public static int GetPhotonID(Int64 clientID)
        {
            PhotonIDs.TryGetValue(clientID, out int photonID);
            if (photonID == 0) Debug.LogError("Nonexistent photonID requested.");
            return photonID;
        }

        /// <summary>
        /// Get a clientID from a photonID
        /// </summary>
        /// <param name="photonID"></param>
        /// <returns></returns>
        public static Int64 GetClientID(int photonID)
        {
            ClientIDs.TryGetValue(photonID, out Int64 clientID);
            if (clientID == 0) Debug.LogError("Nonexistent clientID requested.");
            return clientID;
        }

        /// <summary>
        /// Add a clientID to photonID pairing.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="photonID"></param>
        private static void AddID(Int64 clientID, int photonID)
        {
            if (PhotonIDs.ContainsKey(clientID)) 
                Debug.LogWarning($"Client with server ID \"{clientID}\" had photon ID overwritten with {photonID}");
            PhotonIDs.Add(clientID, photonID);
            ClientIDs.Add(photonID, clientID);
        }

        #region MailboxBoilerplate
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.PhotonID;
        protected override MailboxGrammar Clone()
        {
            return new PhotonIdMailbox();
        }

        public override void HandleMessage(byte[] data, ushort prefix)
        {
            AddID(ParentConnection.ID, BitConverter.ToInt32(data, 0));
        }
        #endregion
    
    }
}

