using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientView : MonoBehaviour
{
    public int clientId;
    public TextMeshProUGUI nickname;
    [SerializeField] private Image indicator;
    [SerializeField] private Color onlineColor, offlineColor;
    private bool isOnline = false;

    public void Init(string _nickname, Color _color)
    {
        nickname.text = _nickname;
        nickname.color = _color;
    }

    public void ChangeStatus(bool _isOnline, Color _color)
    {
        isOnline = _isOnline;
        nickname.color = _color;

        if(_isOnline)
            indicator.color = onlineColor;
        else
            indicator.color = offlineColor;
    }

    public void ChangeStatus(bool _isOnline)
    {
        isOnline = _isOnline;

        if (_isOnline)
            indicator.color = onlineColor;
        else
            indicator.color = offlineColor;
    }
}
