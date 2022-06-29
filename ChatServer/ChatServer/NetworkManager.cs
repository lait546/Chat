using System;
using System.Collections;
using System.Collections.Generic;

namespace ChatServer
{
    public class NetworkManager
    {
        public static NetworkManager instance;

        public int port;

        public static int COUNT_LOBBY = 100;

        public void Start()
        {
            Server.Start(50, 26950);
        }
    }
}