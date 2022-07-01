using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Chat : MonoBehaviour
{
    public GameObject messageContainer, clientContainer;
    [SerializeField] private ClientView clientPref;
    [SerializeField] private Message messagePref;
    [SerializeField] private TMP_InputField inputField;
    public List<ClientView> clientObjects = new List<ClientView>();

    public void AddClient(Client client)
    {
        ClientView clientView = Instantiate(clientPref, clientContainer.transform);
        clientView.Init(client.username, client.color);
        client.clientView = clientView;
        clientObjects.Add(clientView);
    }

    public void AddMessage(string clientUserName, string _message)
    {
        Message message = Instantiate(messagePref, messageContainer.transform);
        message.text.color = new Color(NetworkManager.players[clientUserName].color.r, NetworkManager.players[clientUserName].color.g, NetworkManager.players[clientUserName].color.b);
        message.text.text = clientUserName + ": " + _message;
    }

    public void AddMessageFromServer(string _message)
    {
        Message message = Instantiate(messagePref, messageContainer.transform);
        message.text.text = _message;
    }

    public void SendMessage()
    {
        ClientSend.SendMessage(inputField.text);
        inputField.text = "";
    }

    public void ExitChat()
    {
        Application.Quit();
    }
}
