using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Server
    {
        public static int MaxClients { get; private set; }
        public static int Port { get; private set; }
        public static Dictionary<int, Client> clientsNetwork = new Dictionary<int, Client>();
        public delegate void PacketHandler(int _fromClient, Packet _packet);
        public static Dictionary<int, PacketHandler> packetHandlers;

        private static TcpListener tcpListener;

        public static Chat chat = new Chat();
        public static void Start(int _maxPlayers, int _port)
        {
            MaxClients = _maxPlayers;
            Port = _port;

            Console.WriteLine("Starting server...");
            InitializeServerData();

            tcpListener = new TcpListener(IPAddress.Any, Port);

            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);

            Console.WriteLine($"Server started on port {Port}.");

            DoWork();
        }

        private static async void DoWork()
        {
            while (true)
            {

            }
        }

        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);

            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}...");

            for (int i = 1; i <= MaxClients; i++)
            {
                if (clientsNetwork[i].tcp.socket == null)
                {
                    clientsNetwork[i].tcp.Connect(_client);
                    return;
                }
            }

            Console.WriteLine($"{_client.Client.RemoteEndPoint} failed to connect: Server full!");
        }

        private static void InitializeServerData()
        {
            for (int i = clientsNetwork.Count; i <= MaxClients; i++)
            {
                clientsNetwork.Add(i, new Client(i));
            }

            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
                { (int)ClientPackets.sendMessage, ServerHandle.SendMessageFromClient },
            };
            Console.WriteLine("Initialized packets.");
        }

        public static void Stop()
        {
            tcpListener.Stop();
            tcpListener = null;
        }

        static void Main(string[] args)
        {
            Start(20, 26950);
        }
    }
}
