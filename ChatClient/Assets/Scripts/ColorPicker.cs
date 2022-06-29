using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private RectTransform _texture;
    [SerializeField] private Image choosedColor;
    [SerializeField] private Texture2D _refSprite;

    private void SetColor()
    {
        Vector3 imagePos = _texture.position;
        float globalPosX = Input.mousePosition.x - imagePos.x;
        float globalPosY = Input.mousePosition.y - imagePos.y;

        int localPosX = (int)(globalPosX * (_refSprite.width / _texture.rect.width));
        int localPosY = (int)(globalPosY * (_refSprite.height / _texture.rect.height));

        Debug.Log("Color: " + "globalPosX: " + globalPosX + " " + "globalPosY: " + globalPosY + " " + "localPosX: + " + localPosX + " " + "localPosY: " + localPosY);

        Color c = _refSprite.GetPixel(localPosX, localPosY);
        SetActualColor(c);
    }

    private void SetActualColor(Color c)
    {
        choosedColor.color = c;
    }

    public void OnClickPickerColor()
    {
        SetColor();
    }

    public Color GetCurrentColor()
    {
        return choosedColor.color;
    }
}
