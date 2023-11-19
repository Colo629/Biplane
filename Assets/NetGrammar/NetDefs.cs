
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace NetGrammar
{
    public static class NetDefs
    {
        public const string ServerAddressString = "192.168.0.52";
        public static readonly IPAddress ServerAddress = IPAddress.Parse(ServerAddressString); 
        public const int GeneralPortTCP = 19885;
        public const int GeneralPortUDP = 19880;
        public const int VoicePortTCP = 19886;
        public const int MessageHeaderBytes = 4;

        public const int GeneralTickRate = 70;
        
        public enum MailPrefix : ushort
        {
            None = 0,
            KeepWarm = 1,
            CloseConnection = 2,
            
            LogMessage = 10,
            Example = 9,
            
            PhotonID = 20,
            PhotonRoomManage = 21,
            JoinRoom = 22,
            LeaveRoom = 23,
            CreateRoom = 24,
            JoinOrCreateRoom = 25,
            KickFromRoom = 26,
            CloseRoom = 27,
            
            RegisterSpeaker = 1000,
            TTSRequest = 101,  // 101-132 reserved for TTS requests.
            AudioData = 133,   // 133-164 reserved for AudioData transfer.
            TTSSetup = 165,    // 165-196 reserved for TTS setup.
            TTSSequencer = 200, // 200-231 reserved for TTS sequencing.
            TTSTranscript = 232, // 232-263 reserved for TTS transcripts.
            
            UnifiedTTSRequest = 300, 
            
            SinglePosition = 350,
            DoublePosition = 351,

        }
        
        /*
        /// <summary>
        /// Create a network-ready packet from the data and the prefix.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(byte[] data, ushort prefix)
        {
            int length = MessageHeaderBytes + sizeof(ushort) + data.Length;
            var packet = new byte[length];

            // Outputs big-endian when given the output of a GetBytes
            void InsetBytes(int startIndex, IReadOnlyList<byte> bytes)
            {
                for (var i = 0; i < bytes.Count; i++) packet[startIndex + i] = bytes[i];
            }
            InsetBytes(0, NetBits.GetBytes(length));
            InsetBytes(sizeof(int), NetBits.GetBytes(prefix));
            InsetBytes(sizeof(int) + sizeof(ushort), data);
            return packet;
        }*/

        /// <summary>
        /// Grabs a prefix from a byte array.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ushort GetPrefix(ref byte[] message)
        {
            return (ushort)((message[0] << 8) + message[1]); 
        }
        
        /// <summary>
        /// Creates a TCPListener on the selected port. 
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static TcpListener NewTcpListener(int port = GeneralPortTCP)
        {
            return new TcpListener(IPAddress.Any, port);
        }
    }
}