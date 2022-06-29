using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatServer
{
    public class Chat
    {
        public Dictionary<string, ClientChat> clients = new Dictionary<string, ClientChat>();
        public Dictionary<int, Message> messages = new Dictionary<int, Message>();

        public void TryAddClient(Client client)
        {
            if (!clients.Keys.Contains(client.username))
            {
                clients.Add(client.username, new ClientChat(client.username, client.color));
            }
            ServerSend.ClientEnteredChatToAll(client.username, client.color, true);
            clients[client.username].ChangeStatus(true, client.color);

        }

        public void AddMessage(int clientId, string message)
        {
            messages.Add(messages.Count, new Message(Server.clientsNetwork[clientId].username, message));

            ServerSend.SendMessage(Server.clientsNetwork[clientId].username, message);
        }

        public void SendMessageFromServer(string message)
        {
            messages.Add(messages.Count, new Message(message));

            ServerSend.SendMessageFromServer(message);
        }

        public void SendLastMessages(int toClientId)
        {
            int countLastMessages = 20;

            for(int i = MyExtensions.Clamp(messages.Count, 0, countLastMessages); i > 0; i --)
            {
                if (messages.Reverse().ElementAt(i - 1).Value.isFromClient)
                    ServerSend.SendMessage(messages.Reverse().ElementAt(i - 1).Value.userName, messages.Reverse().ElementAt(i - 1).Value.Text);
                else
                    ServerSend.SendMessageFromServer(messages.Reverse().ElementAt(i - 1).Value.Text);
            }
        }

        public void SendAllClientsToClient(int toClientId)
        {
            foreach (var client in clients)
                ServerSend.EnteredChatClientToClient(toClientId, client.Key, client.Value.color, client.Value.isOnline);
        }
    }
}