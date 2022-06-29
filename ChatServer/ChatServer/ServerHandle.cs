using System;
using System.Collections;
using System.Collections.Generic;

namespace ChatServer
{
    public class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();

            float _r = _packet.ReadFloat();
            float _g = _packet.ReadFloat();
            float _b = _packet.ReadFloat();

            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clientsNetwork[_fromClient].tcp.socket.Client.RemoteEndPoint} : his name {_username}, connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }

            Server.chat.SendAllClientsToClient(_clientIdCheck);

            Server.clientsNetwork[_fromClient].color = new Color(_r, _g, _b, 255f);
            Server.clientsNetwork[_fromClient].username = _username;

            Server.chat.TryAddClient(Server.clientsNetwork[_clientIdCheck]);

            ServerSend.JoinChat(_clientIdCheck);

            Server.chat.SendLastMessages(_clientIdCheck);

            Server.chat.SendMessageFromServer($"{_username} connected successfully.");
        }



        public static void SendMessageFromClient(int _fromClient, Packet _packet)
        {
            string message = _packet.ReadString();

            Server.chat.AddMessage(_fromClient, message);
        }
    }
}