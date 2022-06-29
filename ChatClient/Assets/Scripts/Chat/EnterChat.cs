using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterChat : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputFieldNickName;
    [SerializeField] private ColorPicker clientColor;
    [SerializeField] private GameObject MenuObj, ChatObj;

    public void SetNameAndColor()
    {
        Client.instance.username = inputFieldNickName.text;
        Client.instance.color = new Color(clientColor.GetCurrentColor().r, clientColor.GetCurrentColor().g, clientColor.GetCurrentColor().b, 255f);
    }

    public void JoinChat()
    {
        MenuObj.SetActive(false);
        ChatObj.SetActive(true);
    }
}
