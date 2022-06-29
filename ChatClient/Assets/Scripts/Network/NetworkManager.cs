using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    public static Dictionary<string, Client> players = new Dictionary<string, Client>();

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void InitPlayer(string _username, Color _color, bool isOnline)
    {
        Client _client = new Client();
        if (!players.ContainsKey(_username))
        {
            _client.username = _username;
            _client.color = _color;

            players.Add(_username, _client);

            ChatInitializer.instance.chat.AddClient(_client);
        }
        players[_username].clientView.ChangeStatus(isOnline, _color);
    }

    public void PlayerDisconnected(string _userName)
    {
        players[_userName].clientView.ChangeStatus(false);
    }

    public void ConnectToServer()
    {
        Client.instance.ConnectToServer();
    }

    public void Disconnect()
    {
        Client.instance.Disconnect();
    }
}