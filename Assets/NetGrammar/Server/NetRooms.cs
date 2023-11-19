using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace NetGrammar.Server  // Praise be to our lord and saviour, Hash Tables
{
    public class NetRoom
    {
        #region Static Fields
        private static readonly Dictionary<long, NetRoom> Rooms = new Dictionary<long, NetRoom>();
        
        #endregion
        
        public long RoomCode { get; private set; }
        public bool IsClosed { get; private set; }
        private readonly HashSet<long> roomClients = new HashSet<long>();

        public delegate void RoomClosureDelegate(long roomCode);
        public RoomClosureDelegate RoomClosed;

        public NetRoom(long roomCode)
        {
            RoomCode = roomCode;
        }
        
        #region Static Methods

        /// <summary>
        /// Get a room by room code. Returns null if no room is found.
        /// </summary>
        /// <param name="roomCode"></param>
        /// <returns></returns>
        public static NetRoom GetRoom(long roomCode)
        {
            Rooms.TryGetValue(roomCode, out NetRoom room);
            return room;
        }
        
        /// <summary>
        /// Get a list of all rooms currently tracked by the server.
        /// </summary>
        /// <returns></returns>
        public static List<NetRoom> GetRooms()
        {
            return Rooms.Values.ToList();
        }

        /// <summary>
        /// Create a new empty room with the chosen code. 
        /// </summary>
        /// <param name="roomCode"></param>
        public static NetRoom NewRoom(long roomCode)
        {
            if (GetRoom(roomCode) != null)
            {
                Debug.LogError($"Attempted to add existing room with room code {roomCode}");
                return null; 
            }
            Rooms.Add(roomCode, new NetRoom(roomCode));
            return GetRoom(roomCode);
        }
        
        /// <summary>
        /// Close a room by room code.
        /// </summary>
        /// <param name="roomCode"></param>
        public static void CloseRoom(long roomCode)
        {   
            GetRoom(roomCode).Close();
            Rooms.Remove(roomCode);  // Maybe needs a null check, idr
        }

        public static void ClearEmpty()
        {
            foreach (NetRoom room in Rooms.Values.Where(room => room.roomClients.Count == 0)) room.Close();
        }

        #endregion

        /// <summary>
        /// Add a client to a room
        /// </summary>
        /// <param name="clientID"></param>
        public void JoinClient(long clientID)
        {
            if (roomClients.Contains(clientID)) 
                Debug.LogError($"Client \"{clientID}\" was added to room ({RoomCode}) they already occupied.");
            roomClients.Add(clientID);
            NetClient.GetClient(clientID).SendToRoom(RoomCode);
            Debug.Log($"Client \"{clientID}\" joined room \"{RoomCode}\"");
        }

        /// <summary>
        /// Get a client by id, if they're in the room.
        /// </summary>
        /// <param name="clientID"></param>
        public NetClient GetClient(long clientID)
        {
            return roomClients.Contains(clientID) ? NetClient.GetClient(clientID) : null;
        }

        /// <summary>
        /// Get a list of all clients in a room.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NetClient> GetClients()
        {
            return roomClients.Select(GetClient).ToList();
        }

        /// <summary>
        /// Checks whether the client associated with the ID is in the room.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public bool ContainsClient(long clientID)
        {
            return roomClients.Contains(clientID);
        }

        /// <summary>
        /// Kick a client from a room and notify them of the kick, if they were in the room.
        /// Optionally, force a kick notification to the client regardless of presence in room.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="reason"></param>
        /// <param name="forceNotify"></param>
        public void KickClient(long clientID, string reason = "None given.", bool forceNotify = false)
        {
            if (roomClients.Contains(clientID))
            {
                roomClients.Remove(clientID);
                NetClient.GetClient(clientID).NotifyKick(RoomCode, reason);
                return;
            }
            if (forceNotify)
                NetClient.GetClient(clientID).NotifyKick(RoomCode, reason);
        }

        /// <summary>
        /// Close the room, notifying all clients in the room.
        /// </summary>
        public void Close()
        {
            RoomClosed.Invoke(RoomCode);
            foreach (long clientID in roomClients)
                NetClient.GetClient(clientID).NotifyKick(RoomCode, "Room closed.");
            IsClosed = true;
        }
    }

    /// <summary>
    /// Handles client-specific actions related to room actions and client grouping.
    /// NetRoom instances should be used to specify adding and kicking clients from a room.
    /// </summary>
    public class NetClient
    {
        #region Static Properties
        /// <summary>
        /// The collection of all clients tracked by this server.
        /// </summary>
        private static readonly Dictionary<Int64, NetClient> Clients = new Dictionary<Int64, NetClient>(); 
        
        #endregion
        
        /// <summary>
        /// This client's unique serverside identifier.
        /// </summary>
        public Int64 ID { get; private set; }

        /// <summary>
        /// Vanity name for this client, could be a player name or photonID. 
        /// </summary>
        public string VanityName = "None";
        
        /// <summary>
        /// The room code of the room the client is in, if any.
        /// </summary>
        public Int64 RoomCode { get; private set; } = -1;

        /// <summary>
        /// The connection associated with this client.
        /// </summary>
        public ServerGrammar.ClientConnection Connection;

        public NetRoom CurrentRoom => GetCurrentRoom();
        
        public bool Connected { get; private set; }
        private readonly Dictionary<Type, MailboxGrammar> mailboxes = new Dictionary<Type, MailboxGrammar>();

        public NetClient(Int64 id, List<MailboxGrammar> boxes, ServerGrammar.ClientConnection connection)
        {
            ID = id;
            Connection = connection;
            foreach (MailboxGrammar mailbox in boxes)
                mailboxes.Add(mailbox.GetType(), mailbox);
        }

        /// <summary>
        /// Set the current room of the NetClient.
        /// </summary>
        /// <param name="roomCode"></param>
        public void SendToRoom(long roomCode)
        {
            RoomCode = roomCode;
        }
        
        #region Static Methods

        /// <summary>
        /// Get a client by client ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static NetClient GetClient(Int64 id)
        {
            Clients.TryGetValue(id, out NetClient outClient);
            return outClient;
        }

        /// <summary>
        /// Add a client for tracking by the NetClient system.
        /// </summary>
        /// <param name="client"></param>
        public static void AddClient(NetClient client)
        {
            if (NetClient.GetClient(client.ID) != null) 
                throw new Exception("Attempted to add client with duplicate ID.");
            Clients.Add(client.ID, client);
        }

        #endregion
        

        /// <summary>
        /// Tell a client it's been removed from a particular room. 
        /// NetRoom instances should be used to specify adding and kicking clients from a room.
        /// </summary>
        /// <param name="roomCode"></param>
        /// <param name="reason"></param>
        public void NotifyKick(Int64 roomCode, string reason = "None given.")
        {
            if (RoomCode != roomCode) return;
            RoomCode = -1;
            Debug.Log($"Client \"{ID}\" kicked for reason: {reason}");
        }

        /// <summary>
        /// Returns the mailbox associated with a client of a particular type.
        /// </summary>
        /// <param name="mailboxType"></param>
        /// <returns></returns>
        public MailboxGrammar GetMailbox(Type mailboxType)
        {
            mailboxes.TryGetValue(mailboxType, out MailboxGrammar mailbox);
            return mailbox;
        }

        private NetRoom GetCurrentRoom()
        {
            return NetRoom.GetRoom(RoomCode);
        }

    }
    
}


