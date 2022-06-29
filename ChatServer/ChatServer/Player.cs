using System.Collections;
using System.Collections.Generic;

public enum Role
{
    Sheriff,
    SheriffAssistant,
    Civilian,
    Bandit
}

namespace ChatServer
{
    public class Player
    {
        public int id;
        public string username;
        public Color color;
        private bool canPlay = true;

        private int hp = 1;

        //public Color Color
        //{
        //    get { return color; }
        //    set
        //    {
        //        color = value;
        //        ServerSend.SetPlayerColor((int)color.r, (int)color.g, (int)color.b, (int)color.a, id, roomNumber);
        //    }
        //}

        public void Initialize(int _id, string _username)
        {
            id = _id;
            username = _username;
        }
    }
}