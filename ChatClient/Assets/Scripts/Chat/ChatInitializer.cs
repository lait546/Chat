using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatInitializer : MonoBehaviour
{
    public static ChatInitializer instance;
    public EnterChat enterChat;
    public Chat chat;

    void Awake()
    {
        instance = this;
    }
}
