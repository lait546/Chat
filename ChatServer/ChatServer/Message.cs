public class Message
{
    public string userName, Text;
    public bool isFromClient = false;

    public Message(string _userName, string _text)
    {
        userName = _userName;
        Text = _text;
        isFromClient = true;
    }

    public Message(string text)
    {
        userName = "";
        Text = text;
    }
}