using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net.Sockets;
using System;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

public class TCPWatcher : MonoBehaviour
{
    private string serverIP = "192.168.0.85";
    //private string serverIP = "52.53.239.39";

    private int tcpPort = 13113;
    public static string uID = "";

    public bool running = false;
    public bool start = false;
    private bool autostart = false;
    private bool tcpConnected = false;
    private bool udpConnected = false;

    public GameObject prefabPuppet;
    public Transform playerTransform;


    private TcpClient tcpClient;
    private static double netTimeOffset;

    public Transform puppetHolder;


    private static Queue<ConvertableJSON> tcpCommands;

    private static float realTime = 0;

    private float lastTcpPacket = 2;
    private float tcpTimeout = 15;

    public bool debugSetConnected = false;
    public bool isServer = false;

    private float curTime;


    [Serializable]
    public class ConvertableJSON
    {
        public virtual string Command()
        {
            return "None";
        }
        public string HeadlessConvert()
        {
            string jsonString = JsonConvert.SerializeObject(this);
            return jsonString.Substring(1, jsonString.Length - 2); //chops off the first and final bracket, allowing property stacking
        }

        public string DirectConvert()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [Serializable]
    private class CommandSlice : ConvertableJSON
    {
        public List<string> Commands { get; set; }
    }


    private class TeleportPlayerSlice
    {
        public List<float> TeleportPlayerTo { get; set; }
    }

    private class ServerTimeSlice
    {
        public int ServerTimeSync { get; set; }
    }

    private class TimeSyncCommand : ConvertableJSON
    {
        public override string Command()
        {
            return "TimeSync";
        }
        public string TimeSyncPlaceholder = "sss";
    }

    [Serializable]
    private class PuppetUpdateCommand : ConvertableJSON
    {
        public override string Command()
        {
            return "PuppetUpdates";
        }
        public List<string> PuppetUpdates { get; set; }
    }

    private class EventsSlice
    {
        public List<string> Events { get; set; }
    }

    private class EventCmdSlice
    {
        public string EventCommand { get; set; }
    }

    private class PuppetUpdatesSlice
    {
        public List<string> PuppetUpdates { get; set; }
    }

    private class PuppetCmdSlice
    {
        public string PuppetCommand;
    }

    void OnApplicationQuit()
    {
        DisconnectAll();
        Application.runInBackground = false;
        Debug.LogWarning("Game closed.");
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.runInBackground = true;
        tcpCommands = new Queue<ConvertableJSON>();
    }
    private void Start()
    {
        uID = Guid.NewGuid().ToString();
        IpConfiguration();
    }
    private void Update()
    {
        curTime = Time.time;
        realTime = Time.realtimeSinceStartup;
        if (start) { start = false; running = true; StreamsInitialze(); }
        if ((Time.time > 2) & (autostart == false) & !debugSetConnected) { start = true; autostart = true; }
        if ((udpConnected == false) & (tcpConnected == true))
        {

        }
        if (udpConnected & tcpConnected)
        {
            //set connected
        }
        if ((realTime - lastTcpPacket > tcpTimeout) & tcpConnected) { Debug.LogError("TCP timeout."); DisconnectAll(); }
        if (debugSetConnected)
        {
            //set connected
        }
        if (Input.GetButton("Disconnect"))
        {
            DisconnectAll();
            return;
        }
    }

    public void DisconnectAll()
    {
        tcpConnected = false;
        tcpClient.GetStream().Flush();
        tcpClient.GetStream().Close();
        tcpClient.Close();
        //set player disconnected
    }

    private void IpConfiguration()
    {/*
        try
        {
            Debug.Log("Old ip: " + serverIP + serverIP.Length);
            string path = Application.dataPath + "/network.txt";
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path);
            string[] netConfig = reader.ReadToEnd().Split('\n');
            serverIP = netConfig[0].Trim();
            //4listenOnPort = int.Parse(netConfig[1].Trim());
            //sendToPort = int.Parse(netConfig[2].Trim());
            Debug.Log("New ip: " + serverIP + serverIP.Length);
            Debug.Log("Network config files loaded.");
        }
        catch
        {
            Debug.Log("Default network config loaded.");
            serverIP = "192.168.0.223";
            listenOnPort = 12321;
            sendToPort = 12321;//12322;
}*/
    }


    public static double GetNetOffset()
    {
        return netTimeOffset;
    }

    public static double GetServerTime()
    {
        return netTimeOffset + realTime;
    }

    public static void AddTcpCommand(ConvertableJSON command)
    {
        tcpCommands.Enqueue(command);
    }


    void StreamsInitialze()
    {
        TCPInitialize();
    }

    void TCPInitialize()
    {
        try
        {
            Thread TcpSendRecieveThread = new Thread(() => TcpSendListenLoop());
            TcpSendRecieveThread.Start();

        }
        catch
        {
            Debug.LogError("yowch but tcp");
        }
    }


    private string CompactConvertables(List<ConvertableJSON> convertables)
    {
        string compact = "{";
        CommandSlice cmdSlice = new CommandSlice();
        cmdSlice.Commands = new List<string>();
        foreach (ConvertableJSON cvjs in convertables)
        {
            cmdSlice.Commands.Add(cvjs.Command());
            compact += cvjs.HeadlessConvert();
            compact += ", ";
        }
        compact += cmdSlice.HeadlessConvert();
        compact += "}";
        return compact;
    }


    private void TcpSendListenLoop()
    {


        IPAddress remote = IPAddress.Parse(serverIP);

        //Debug.Log("TCP attempting to connect...");
        TcpClient playerClient = new TcpClient();
        playerClient.Connect(new IPEndPoint(remote, tcpPort));
        tcpClient = playerClient;
        //Debug.Log("TCP client created...");
        NetworkStream stream = tcpClient.GetStream();

        List<ConvertableJSON> cmdDataPile = new List<ConvertableJSON>();
        AddTcpCommand(new TimeSyncCommand());


        string datastring = CompactConvertables(cmdDataPile);
        Debug.Log(datastring);
        SendNetString(datastring, stream);

        string responseJSON;
        CommandSlice cmdSlice;
        bool initConnect = true;
        float lastTimeSync = 0;

        while (running)
        {
            if (initConnect) { tcpConnected = true; initConnect = false; }
            lastTcpPacket = realTime;
            if (curTime - lastTimeSync > 1)
            {
                AddTcpCommand(new TimeSyncCommand());
                lastTimeSync = curTime;
            }

            responseJSON = RecieveNetString(stream);
            //Debug.Log(responseJSON);
            cmdSlice = JsonConvert.DeserializeObject<CommandSlice>(responseJSON);

            if (cmdSlice.Commands.Contains("HandshakeReturn"))
            {
                ServerTimeSlice serverTime = JsonConvert.DeserializeObject<ServerTimeSlice>(responseJSON);
            }
            if (cmdSlice.Commands.Contains("ServerTimeSync"))
            {
                ServerTimeSlice serverTime = JsonConvert.DeserializeObject<ServerTimeSlice>(responseJSON);
            }

            if (cmdSlice.Commands.Contains("PuppetUpdates"))
            {
                PuppetUpdatesSlice puppetUpdates = JsonConvert.DeserializeObject<PuppetUpdatesSlice>(responseJSON);
                foreach (string updateJSON in puppetUpdates.PuppetUpdates)
                {
                    if (updateJSON != null)
                    {
                        ProcessPuppetUpdate(updateJSON);
                    }
                    else
                    {
                        Debug.LogWarning("UpdateJSON null warning.");
                    }
                }
            }

            if (cmdSlice.Commands.Contains("EventUpdate"))
            {
                EventsSlice events = JsonConvert.DeserializeObject<EventsSlice>(responseJSON);
                foreach (string eventJSON in events.Events)
                {
                    if (eventJSON != null)
                    {
                        ProcessEvent(eventJSON);
                    }
                    else
                    {
                        Debug.LogWarning("EventJSON null warning.");
                    }
                }
            }

            cmdDataPile = new List<ConvertableJSON>();
            while (tcpCommands.Count > 0)
            {
                cmdDataPile.Add(tcpCommands.Dequeue());
            }
            SendNetString(CompactConvertables(cmdDataPile), stream);
        }
    }


    private void ProcessEvent(string eventJSON)
    {
        EventCmdSlice cmdSlice = new EventCmdSlice();
        cmdSlice = JsonConvert.DeserializeObject<EventCmdSlice>(eventJSON);
        string command = cmdSlice.EventCommand;
        if (command == "ChatMessage")
        {

        }
    }    
    private void ProcessPuppetUpdate(string updateJSON)
    {
        PuppetCmdSlice cmdSlice;
        cmdSlice = JsonConvert.DeserializeObject<PuppetCmdSlice>(updateJSON);
        string command = cmdSlice.PuppetCommand;
        if (command == "Position")
        {
            PuppetManager.PuppetPositionData positionData = JsonConvert.DeserializeObject<PuppetManager.PuppetPositionData>(updateJSON);
            PuppetManager.PuppetPositionDataHandler(positionData);
        }
    }

    private void SendNetString(string data, NetworkStream stream)
    {
        //Debug.Log(NetPrefix(data));
        byte[] dqCommandData = Encoding.UTF8.GetBytes(NetPrefix(data));
        stream.Write(dqCommandData, 0, dqCommandData.Length);
    }

    private string RecieveNetString(NetworkStream stream)
    {
        string netString = "";
        bool msg_complete = false;
        byte[] data = new byte[1024];
        int bytes;
        while (msg_complete != true)
        {
            bytes = stream.Read(data, 0, data.Length);
            netString += Encoding.UTF8.GetString(data, 0, bytes);
            msg_complete = PrefixCheck(netString);
        }
        return PrefixTrim(netString);
    }

    private string NetPrefix(string netTask)
    {
        //8 zeroes
        netTask = (netTask.Length + 100000000).ToString() + netTask;
        return netTask;
    }

    private bool PrefixCheck(string netString)
    {
        //Debug.Log(netString);
        if (netString.Length < 2)
        {
            return false;
        }
        string prefix = netString.Substring(1, 8);
        int targetLength = int.Parse(prefix) + 9;
        if (netString.Length >= targetLength) { return true; }
        else { return false; }
    }

    private string PrefixTrim(string netString)
    {
        return netString.Substring(9, netString.Length - 9);
    }

    // 0: UID
    // 1: CMD
    // 2: DATA

    private void TcpTimeManager(string[] timeData)
    {
        netTimeOffset = double.Parse(timeData[2]) - realTime;

        Debug.Log("Set time to: " + timeData[2]);
    }

}
