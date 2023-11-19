using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;

namespace NetGrammar.Client
{
    /// <summary>
    /// Marks a class as a ServerMailbox for registry with the proper server listeners.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ClientMailbox : Attribute
    {
    }
    public sealed class ClientGrammar : MonoBehaviour
    {
        public static ClientGrammar Instance;
        public static ServerConnection Connection;
        public static bool IsConnected => CanSend();
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
            Instance = this;
            var tcpClient = new TcpClient();
            tcpClient.Connect(NetDefs.ServerAddress, NetDefs.GeneralPortTCP);
            var mailboxInstances = new List<MailboxGrammar>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var mailboxes = 
                    from type in assembly.GetTypes()
                    where type.IsDefined(typeof(ClientMailbox), false)
                    select type;
                foreach (Type mailbox in mailboxes)
                    mailboxInstances.Add(Activator.CreateInstance(mailbox) as MailboxGrammar);
            }
            Connection = new ServerConnection(tcpClient, mailboxInstances);
            Debug.Log("Connection Created.");
            
        }
        
        private void Update()
        {
            DequeueMessages();
        }

        private void OnApplicationQuit()
        {
            Connection.Dispose();
        }

        /// <summary>
        /// Queue a message which will be sent to the server with the supplied data and prefix.
        /// Returns whether there is currently an open connection with the server.
        /// </summary>
        public static bool SendNetGram(byte[] data, ushort prefix)
        {
            Connection.SendBytes(data, prefix);
            return IsConnected;
        }

        #region SendData Overloads

        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(ushort prefix, params byte[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(NetDefs.MailPrefix prefix, params byte[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(ushort prefix, params bool[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(NetDefs.MailPrefix prefix, params bool[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(ushort prefix, params char[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(NetDefs.MailPrefix prefix, params char[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(ushort prefix, params float[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(NetDefs.MailPrefix prefix, params float[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(ushort prefix, params double[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(NetDefs.MailPrefix prefix, params double[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(ushort prefix, params int[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(NetDefs.MailPrefix prefix, params int[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(ushort prefix, params long[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(NetDefs.MailPrefix prefix, params long[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(ushort prefix, params short[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(NetDefs.MailPrefix prefix, params short[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(ushort prefix, params uint[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(NetDefs.MailPrefix prefix, params uint[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(ushort prefix, params ulong[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(NetDefs.MailPrefix prefix, params ulong[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(ushort prefix, params ushort[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(NetDefs.MailPrefix prefix, params ushort[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send string data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(ushort prefix, params string[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
        }
        /// <summary>
        /// Prep and send string data to the client.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        public static void SendData(NetDefs.MailPrefix prefix, params string[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, dataElements));
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
        public static void SendData(ushort prefix, bool skipNonASCII, params string[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, skipNonASCII, dataElements));
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
        public static void SendData(NetDefs.MailPrefix prefix, bool skipNonASCII, params string[] dataElements)
        {
            Connection.QueueSend(NetBits.AssemblePacket(prefix, skipNonASCII, dataElements));
        }
        #endregion

        /// <summary>
        /// Queue a message which will be sent to the server with the supplied data and prefix.
        /// Returns whether there is currently an open connection with the server.
        /// </summary>
        [Obsolete("SendNetGram is deprecated, please use SendData instead.")]
        public static bool SendNetGram(byte[] data, NetDefs.MailPrefix prefix)
        {
            return SendNetGram(data, (ushort)prefix);
        }
        
        /// <summary>
        /// Passthrough for tcpClient status.
        /// </summary>
        /// <returns></returns>
        private static bool CanSend()
        {
            return Connection.TcpConnected();
        }
        
        /// <summary>
        /// Represents a connection to a server. 
        /// </summary>
        public class ServerConnection : IDisposable
        {
            public Thread ClientThread;
            private readonly NetworkStream stream;
            private readonly TcpClient tcpClient;
            private readonly List<MailboxGrammar> _connectionHandlers = new List<MailboxGrammar>();
            private readonly Queue<byte[]> bytesToSend = new Queue<byte[]>();
            private Queue<byte[]> bytesToRead = new Queue<byte[]>();
            private byte[] _partialPacket = new byte[4];  // This is where the messages are assembled before being sent to handlers
            private int pPointer;
            private bool haveSize;
            private bool disposed;
            private readonly byte[] smallWriteBuffer = new byte[8];
            private readonly byte[] bigWriteBuffer = new byte[1024];

            public ServerConnection(TcpClient client, List<MailboxGrammar> connectionTypes)
            {
                tcpClient = client;
                foreach (MailboxGrammar connectionType in connectionTypes)
                {
                    MailboxGrammar handler = connectionType.GetClone();
                    _connectionHandlers.Add(handler);
                }
                stream = tcpClient.GetStream();
                ClientThread = new Thread(ConnectionLoop);
                ClientThread.Start();
                Debug.Log("Client thread started.");
            }
            
            public void Dispose()
            {
                Dispose(true);
                // TODO: Test finalizer error states? Also probably chain to room manager
            }

            private void Dispose(bool disposing)  // Yeah probably not needed, but everyone tells me to anyways
            {
                if (!disposed && disposing)
                {
                    tcpClient.Close();
                    ClientThread.Interrupt();
                }
                disposed = true;
            }


            /// <summary>
            /// Checks whether the tcpClient is connected.
            /// </summary>
            /// <returns></returns>
            public bool TcpConnected()
            {
                return tcpClient.Connected;
            }

            /// <summary>
            /// Queues bytes for sending.
            /// </summary>
            /// <param name="bytes"></param>
            public void QueueSend(byte[] bytes)
            {
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
                QueueSend(NetBits.AssemblePacket(bytes, (ushort)prefix));
            }
            

            /// <summary>
            /// The threaded send/receive loop for the client connection. 
            /// </summary>
            private void ConnectionLoop()
            {
                while (!disposed)
                {
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
                    
                    // Manage the mailbox handlers
                    ManageHandlers();
                    
                    // Send stuff
                    lock(bytesToSend)  // Can give this the swap treatment if necessary 
                        while (bytesToSend.Count > 0)
                        {
                            byte[] packet = bytesToSend.Dequeue();  // Also bad, but I'll deal with it later
                            stream.Write(packet, 0, packet.Length); 
                        }
                    
                    // Wait for half a second
                    Thread.Sleep(1000/NetDefs.GeneralTickRate);
                }
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
                        if (pPointer != _partialPacket.Length || haveSize) continue;
                        
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
                        if (connectionHandler.SelectMessage(prefix)) 
                            EnqueueFromThread(connectionHandler.HandleMessage, bytes);
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
    }
}