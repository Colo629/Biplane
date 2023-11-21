using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using static System.Int32;
using Random = System.Random;

namespace NetGrammar.Server
{
    /// <summary>
    /// Marks a class as a ServerMailbox for registry with the proper server listeners.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ServerMailbox : Attribute
    {
    }
    public sealed class ServerGrammar : MonoBehaviour
    {
        private static readonly Dictionary<int, ServerListener> ServerListeners = new Dictionary<int, ServerListener>();
        private static Queue<HandlerCall> _callQueue = new Queue<HandlerCall>();
    
        private struct HandlerCall
        {
            public readonly Action<byte[], ushort> Handler;
            public readonly byte[] Message;
            public HandlerCall(Action<byte[], ushort> handler, byte[] message)
            {
                Handler = handler;
                Message = message;
            }
        }

        private void Awake()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var mailboxes = 
                    from type in assembly.GetTypes()
                    where type.IsDefined(typeof(ServerMailbox), false)
                    select type;
                foreach (Type mailbox in mailboxes)
                {   // Is reflection ever *not* cursed?
                    var box = Activator.CreateInstance(mailbox) as MailboxGrammar;
                    Debug.Log(box?.GetType().ToString());
                    RegisterMailboxClass(box); 
                }
            }
            StartListeners();
        }
        
        private void Update()
        {
            DequeueMessages();
        }

        private void OnApplicationQuit()
        {
            foreach (var serverListener in ServerListeners.Values)
            {
                serverListener.StopListen();
            }
        }


        /// <summary>
        /// A class containing information about a single server listener, and its associated clients.
        /// </summary>
        private class ServerListener
        {
            public readonly int Port;
            public readonly TcpListener Listener;
            public readonly Dictionary<Int64, ClientConnection> Clients = new Dictionary<Int64, ClientConnection>();
            public Thread ListenThread { get; private set; }
            private bool _isListening;
            public readonly List<MailboxGrammar> ConnectionGrammars = new List<MailboxGrammar>();
            
            public ServerListener(int port, TcpListener listener)
            {
                Port = port;
                Listener = listener;
            }

            /// <summary>
            /// Starts the ServerListener listening for inbound client connections. 
            /// </summary>
            public void StartListen()
            {
                Listener.ExclusiveAddressUse = false;
                Listener.Start();
                ListenThread = new Thread(ListenLoop);
                ListenThread.Start();
                _isListening = true;
            }

            /// <summary>
            /// Stops the ServerListener from accepting new inbound client connections. Preserves current connections.
            /// </summary>
            public void StopListen()
            {
                _isListening = false;
                foreach (ClientConnection client in Clients.Values)
                {
                    client.Dispose();
                }
                ListenThread.Interrupt();
            }

            /// <summary>
            /// Add a connection class to the listener for cloning to clients.
            /// </summary>
            /// <param name="mailboxClass"></param>
            public void AddConnectionClass(MailboxGrammar mailboxClass)
            {
                ConnectionGrammars.Add(mailboxClass);
            }

            /// <summary>
            /// Method for use as a threaded listener for creating client threads.
            /// </summary>
            private void ListenLoop()
            {
                while (_isListening)
                {
                    TcpClient inboundClient = Listener.AcceptTcpClient();
                    Debug.Log($"Client connected from {inboundClient.Client.RemoteEndPoint}");
                    var ClientDef = new ClientConnection(inboundClient, ConnectionGrammars);
                    Clients.Add(ClientDef.ID, ClientDef);
                }
            }
        }

        /// <summary>
        /// Represents a connection of a client as related to a particular ServerListener. Clients connected on
        /// multiple ports will have multiple ServerConnection objects with separate tracking and mailboxes.
        /// </summary>
        public class ClientConnection : IDisposable
        {
            public readonly Thread ClientThread;
            public readonly Int64 ID;
            private readonly TcpClient tcpClient;
            private readonly NetworkStream stream;
            private readonly List<MailboxGrammar> _connectionHandlers = new List<MailboxGrammar>();
            private readonly Queue<byte[]> bytesToSend = new Queue<byte[]>();  // Need to remove readonly for hot-swap method
            private Queue<byte[]> bytesToRead = new Queue<byte[]>();
            private byte[] _partialPacket = new byte[4];  // This is where the messages are assembled before being sent to handlers
            private int pPointer;
            private bool haveSize;
            private bool disposed;
            private readonly byte[] smallWriteBuffer = new byte[8];
            private readonly byte[] bigWriteBuffer = new byte[1024];
            
            public ClientConnection(TcpClient client, List<MailboxGrammar> connectionTypes)
            {
                //ID = Path.GetRandomFileName().Take(8).ToString();  // lol  // Left for posterity's sake
                var ranGen = new Random();
                ID = ((long)ranGen.Next(0, MaxValue) << 32) + ranGen.Next(0, MaxValue);
                tcpClient = client;
                foreach (MailboxGrammar connectionType in connectionTypes)
                {
                    MailboxGrammar handler = connectionType.GetClone();
                    handler.ParentConnection = this;                       // You have gained permanent warp.
                    _connectionHandlers.Add(handler);
                }
                stream = tcpClient.GetStream();
                NetClient.AddClient(new NetClient(ID, _connectionHandlers, this));
                ClientThread = new Thread(ConnectionLoop);
                ClientThread.Start();
            }

            public void Dispose()
            {
                Dispose(true);
                // TODO: Test finalizer error states? 
            }

            private void Dispose(bool disposing)
            {
                if (!disposed && disposing)
                {
                    tcpClient.Close();
                    ClientThread.Interrupt();
                }
                disposed = true;
            }

            /// <summary>
            /// Queues bytes for sending.
            /// </summary>
            /// <param name="bytes"></param>
            private void QueueSend(byte[] bytes)
            {
                // This is probably ok for now, but may cause issues if the main server processes are single-threaded
                lock (bytesToSend)
                    bytesToSend.Enqueue(bytes);
            }

            /// <summary>
            /// Prep and send the data to the client.
            /// </summary>
            /// <param name="bytes"></param>
            /// <param name="prefix"></param>
            [Obsolete("SendBytes is deprecated, please use SendData instead.")]
            public void SendBytes(byte[] bytes, ushort prefix)
            {   
                QueueSend(NetBits.AssemblePacket(bytes, prefix));
            }
            
            /// <summary>
            /// Prep and send the data to the client.
            /// </summary>
            /// <param name="bytes"></param>
            /// <param name="prefix"></param>
            [Obsolete("SendBytes is deprecated, please use SendData instead.")]
            public void SendBytes(byte[] bytes, NetDefs.MailPrefix prefix)
            {
                SendBytes(bytes, (ushort)prefix);
            }

            #region SendData Overloads

            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(ushort prefix, params bool[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(NetDefs.MailPrefix prefix, params bool[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(ushort prefix, params char[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(NetDefs.MailPrefix prefix, params char[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(ushort prefix, params float[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(NetDefs.MailPrefix prefix, params float[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(ushort prefix, params double[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(NetDefs.MailPrefix prefix, params double[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(ushort prefix, params int[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(NetDefs.MailPrefix prefix, params int[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(ushort prefix, params long[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(NetDefs.MailPrefix prefix, params long[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(ushort prefix, params short[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(NetDefs.MailPrefix prefix, params short[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(ushort prefix, params uint[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(NetDefs.MailPrefix prefix, params uint[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(ushort prefix, params ulong[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(NetDefs.MailPrefix prefix, params ulong[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(ushort prefix, params ushort[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(NetDefs.MailPrefix prefix, params ushort[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send string data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(ushort prefix, params string[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }
            /// <summary>
            /// Prep and send string data to the client.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="dataElements"></param>
            public void SendData(NetDefs.MailPrefix prefix, params string[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, dataElements));
            }

            /// <summary>
            /// Prep and send string data to the client.
            /// Optionally skips all strings with non-ASCII characters. 
            /// This is useful in cases like user input where input strings may contain non-ASCII characters,
            /// but is somewhat slower overall.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="skipNonASCII"></param>
            /// <param name="dataElements"></param>
            public void SendData(ushort prefix, bool skipNonASCII, params string[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, skipNonASCII, dataElements));
            }

            /// <summary>
            /// Prep and send string data to the client.
            /// Optionally skips all strings with non-ASCII characters. 
            /// This is useful in cases like user input where input strings may contain non-ASCII characters,
            /// but is somewhat slower overall.
            /// </summary>
            /// <param name="prefix"></param>
            /// <param name="skipNonASCII"></param>
            /// <param name="dataElements"></param>
            public void SendData(NetDefs.MailPrefix prefix, bool skipNonASCII, params string[] dataElements)
            {
                QueueSend(NetBits.AssemblePacket(prefix, skipNonASCII, dataElements));
            }
            #endregion

            /// <summary>
            /// The threaded send/receive loop for the client connection. 
            /// </summary>
            private void ConnectionLoop()
            {
                while (!disposed && tcpClient.Connected)
                {
                    // TODO: Redo this to not use GetByte, it's weird because we're not on 2.1 so we can't use Span
                    
                    // Read everything
                    if (stream.DataAvailable) 
                        BufferAndQueue(smallWriteBuffer);
                    while (stream.DataAvailable)
                        BufferAndQueue(bigWriteBuffer);
                        
                    void BufferAndQueue(byte[] buffer)
                    {
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        var partial = new byte[bytesRead];
                        Array.Copy(buffer, partial, bytesRead);
                        bytesToRead.Enqueue(partial);
                    }
                    //Debug.Log(bytesToRead.Count);
                    
                    // Manage the mailbox handlers
                    ManageHandlers();
                    
                    // Send stuff
                    lock(bytesToSend)  // Can give this the swap treatment if necessary 
                        while (bytesToSend.Count > 0)
                        {
                            byte[] packet = bytesToSend.Dequeue();  // Also bad, but I'll deal with it later
                            stream.Write(packet, 0, packet.Length); 
                        }
                    
                    // TODO: More consistent tick rate management
                    Thread.Sleep(1000/NetDefs.GeneralTickRate);
                }
                tcpClient?.Dispose();
            }

            /// <summary>
            /// Get all full packets which have been received since the last packet request.
            /// </summary>
            /// <returns></returns>
            private List<byte[]> GetFullPackets()
            {
                var fullPackets = new List<byte[]>();
                Queue<byte[]> stagedBytes;
                lock (bytesToRead)
                {
                    stagedBytes = bytesToRead;  // Queue swap is optimal for speedy release of the lock
                    bytesToRead = new Queue<byte[]>();
                }

                while (stagedBytes.Count > 0)
                {
                    byte[] headBytes = stagedBytes.Dequeue();
                    var hPointer = 0;
                    if (headBytes.Length <= 0) continue;
                    
                    // Copy across the head's data to the partial message
                    while (hPointer < headBytes.Length)
                    {
                        _partialPacket[pPointer] = headBytes[hPointer];
                        pPointer++;
                        hPointer++;
                        
                        // "Did we complete a message?"
                        if (pPointer == _partialPacket.Length && haveSize)
                        {
                            fullPackets.Add(_partialPacket);
                            _partialPacket = new byte[4];
                            pPointer = 0;
                            haveSize = false;
                        }

                        // "Do we have the new size?"
                        if (pPointer == _partialPacket.Length && !haveSize)
                        {
                            // Big-endian byte -> int32
                            int size = (_partialPacket[0] << 24) + (_partialPacket[1] << 16) + (_partialPacket[2] << 8) + _partialPacket[3];
                            if (size == 0)  // TODO: Test this to see what's up
                            {
                                _partialPacket = new byte[4];
                                pPointer = 0;
                                continue;  
                            }
                        
                            size -= 4;  // We already read the first four bytes of the packet, the rest is the message
                            _partialPacket = new byte[size];
                            pPointer = 0;
                            haveSize = true;
                        }
                    }
                }
                return fullPackets;
            }

            /// <summary>
            /// Sends all relevant messages to all relevant handlers.
            /// </summary>
            private void ManageHandlers()
            {
                foreach (byte[] bytes in GetFullPackets())
                {
                    var prefix = (ushort)((bytes[0] << 8) + bytes[1]);  // Big endian byte -> ushort
                    foreach (MailboxGrammar connectionHandler in _connectionHandlers)
                    {
                        if (connectionHandler.SelectMessage(prefix)) 
                            EnqueueFromThread(connectionHandler.HandleMessage, bytes);
                    }
                }
            }
        }
        
        #region Dethreading Call Management
        
        /// <summary>
        /// For external use, enqueues a message from off-thread to be handled on the main thread.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="message"></param>
        private static void EnqueueFromThread(Action<byte[], ushort> handler, byte[] message) 
        {
            lock (_callQueue) _callQueue.Enqueue(new HandlerCall(handler, message)); 
        }
    
        /// <summary>
        /// Runs on the main thread in Update() to dequeue messages and send them to HandleMessage().
        /// </summary>
        private static void DequeueMessages()
        {
            Queue<HandlerCall> calls;
            lock (_callQueue)
            {
                calls = _callQueue;  // Swapping the queue out is optimal for speedy release of the lock
                _callQueue = new Queue<HandlerCall>();
            }
            while (calls?.Count > 0)
            {
                HandlerCall call = calls.Dequeue();
                byte[] message = call.Message;
                var prefix = (ushort)((message[0] << 8) + message[1]);  // Big endian byte -> ushort
                call.Handler(message.Skip(2).ToArray(), prefix);
            }
        }
        #endregion

        #region Initial Setup
        /// <summary>
        /// Registers an additional connection class to be used for new connections.
        /// </summary>
        /// <param name="mailboxType"></param>
        private static void RegisterMailboxClass(MailboxGrammar mailboxType)
        {
            // Adds the connection class to the listener on the associated port, or adds it to a new one
            if (ServerListeners.TryGetValue(mailboxType.Port, out ServerListener listener))
            {
                listener.AddConnectionClass(mailboxType);
            }
            else
            {
                var newListener = new ServerListener(mailboxType.Port, NetDefs.NewTcpListener());
                ServerListeners.Add(mailboxType.Port, newListener);
                newListener.AddConnectionClass(mailboxType);
            }
        }

        /// <summary>
        /// Start listening on all built listeners, should be called after registering all connection classes.
        /// </summary>
        private static void StartListeners()
        {
            foreach (var serverListener in ServerListeners.Values)
            {
                serverListener.StartListen();
            }
        }
        #endregion
    }
}