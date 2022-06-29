using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;

        ClientSend.WelcomeReceived();
    }

    public static void JoinChat(Packet _packet)
    {
        ChatInitializer.instance.enterChat.JoinChat();
    }

    public static void AddMessage(Packet _packet)
    {
        string _clientUserName = _packet.ReadString();
        string _message = _packet.ReadString();

        ChatInitializer.instance.chat.AddMessage(_clientUserName, _message);
    }

    public static void AddMessageFromServer(Packet _packet)
    {
        string _message = _packet.ReadString();

        ChatInitializer.instance.chat.AddMessageFromServer(_message);
    }

    public static void EnteredChatClient(Packet _packet)
    {
        string _nickname = _packet.ReadString();
        bool isOnline = _packet.ReadBool();

        float _r = _packet.ReadFloat();
        float _g = _packet.ReadFloat();
        float _b = _packet.ReadFloat();

        NetworkManager.instance.InitPlayer(_nickname, new Color(_r, _g, _b), isOnline);
    }

    public static void PlayerDisconnected(Packet _packet)
    {
        string _username = _packet.ReadString();

        NetworkManager.instance.PlayerDisconnected(_username);
    }
}
