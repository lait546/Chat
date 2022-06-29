namespace ChatServer
{
    public class ClientChat
    {
        string userName;
        public Color color;
        public bool isOnline = false;

        public ClientChat(string _userName, Color _color)
        {
            userName = _userName;
            color = _color;
        }

        public void ChangeStatus(bool _isOnline, Color _color)
        {
            color = _color;
            isOnline = _isOnline;
        }

        public void ChangeStatus(bool _isOnline)
        {
            isOnline = _isOnline;
        }
    }
}