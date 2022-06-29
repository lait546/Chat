using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(Client.instance.color.r);
            _packet.Write(Client.instance.color.g);
            _packet.Write(Client.instance.color.b);

            Debug.Log("EnterChat: " + Client.instance.username + " " + Client.instance.color.r + " " + Client.instance.color.g + " " + Client.instance.color.b);

            _packet.Write(Client.instance.username);

            SendTCPData(_packet);
        }
    }

    public static void EnterChat(string nickName, float r, float g, float b)
    {
        using (Packet _packet = new Packet((int)ClientPackets.EnterChat))
        {
            _packet.Write(nickName);
            _packet.Write(r);
            _packet.Write(g);
            _packet.Write(b);

            SendTCPData(_packet);
        }
    }

    public static void SendMessage(string text)
    {
        using (Packet _packet = new Packet((int)ClientPackets.SendMessage))
        {
            _packet.Write(text);

            SendTCPData(_packet);
        }
    }
    #endregion
}
