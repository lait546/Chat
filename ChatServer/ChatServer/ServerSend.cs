using System;
using System.Collections;
using System.Collections.Generic;

namespace ChatServer
{
    public class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clientsNetwork[_toClient].tcp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxClients; i++)
            {
                Server.clientsNetwork[i].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxClients; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clientsNetwork[i].tcp.SendData(_packet);
                }
            }
        }

        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void JoinChat(int toClientId)
        {
            using (Packet _packet = new Packet((int)ServerPackets.joinChat))
            {
                SendTCPData(toClientId, _packet);
            }
        }

        public static void EnteredChatClientToClient(int toClientId, string nickName, Color _color, bool isOnline)
        {
            using (Packet _packet = new Packet((int)ServerPackets.enteredClientToClient))
            {
                _packet.Write(nickName);
                _packet.Write(isOnline);

                _packet.Write(_color.r);
                _packet.Write(_color.g);
                _packet.Write(_color.b);

                SendTCPData(toClientId, _packet);
            }
        }

        public static void ClientEnteredChatToAll(string nickName, Color _color, bool isOnline)
        {
            using (Packet _packet = new Packet((int)ServerPackets.enteredClientToAll))
            {
                _packet.Write(nickName);
                _packet.Write(isOnline);

                _packet.Write(_color.r);
                _packet.Write(_color.g);
                _packet.Write(_color.b);

                SendTCPDataToAll(_packet);
            }
        }

        public static void SendMessage(string fromClientUserName, string message)
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendMessageFromClient))
            {
                _packet.Write(fromClientUserName);
                _packet.Write(message);

                SendTCPDataToAll(_packet);
            }
        }

        public static void SendMessageToClient(int toClient, int fromClientId, string message)
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendMessageToClient))
            {
                _packet.Write(fromClientId);
                _packet.Write(message);

                SendTCPData(toClient, _packet);
            }
        }

        public static void SendMessageFromServer(string message)
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendMessageFromServer))
            {
                _packet.Write(message);

                SendTCPDataToAll(_packet);
            }
        }

        public static void ClientDisconnected(string _playerName)
        {
            using (Packet _packet = new Packet((int)ServerPackets.clientDisconnected))
            {
                _packet.Write(_playerName);
                SendTCPDataToAll(_packet);
            }
        }

        #endregion
    }
}