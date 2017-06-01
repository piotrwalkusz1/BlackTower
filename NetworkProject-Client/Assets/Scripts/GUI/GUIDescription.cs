using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;

public class GUIDescription : GUIObject
{
    public GUIObject BoundObject { get; set; }

    public GUITexture _top;
    public GUITexture _bot;
    public GUITexture _left;
    public GUITexture _right;
    public GUIText _text;

    public int HeightText
    {
        get { return (int)_text.GetScreenRect().height; }
    }

    private const int WIDTH = 150;
    private const int TEXT_BORDER = 3;

    protected override void OnMouseDown()
    {
        // empty
    }

    public void Show(Vector2 positionInPixel, string text, int widthIcon, int heightIcon)
    {
        SetText(text);

        int totalHeight = HeightText + 2 * TEXT_BORDER;
    
        SetScaleTextures(WIDTH, totalHeight);

        Vector2 position = GetPositionInsideScreen(positionInPixel, WIDTH, totalHeight, widthIcon, heightIcon);

        Vector2 transformPosition = GetTransformPosition(position);

        transform.position = new Vector3(transformPosition.x, transformPosition.y, transform.position.z);
    }

    private void SetText(string text)
    {
        TextUtility.SetMultilineText(text, _text, WIDTH - 2 * TEXT_BORDER);
    }

    private Vector2 GetPositionInsideScreen(Vector2 position, int width, int height, int widthIcon, int heightIcon)
    {
        float posX, posY;

        if (position.x + width < Screen.width)
        {
            posX = position.x;
        }
        else
        {
            posX = position.x - widthIcon - WIDTH;
        }

        if (position.y + height < Screen.height)
        {
            posY = position.y;
        }
        else
        {
            posY = position.y - heightIcon - height;
        }

        return new Vector2(posX, posY);
    }

    private void SetScaleTextures(int width, int height)
    {
        guiTexture.pixelInset = new Rect(0, 0, width, height);

        _bot.pixelInset = new Rect(0, 0, width, 1);
        _top.pixelInset = new Rect(0, height - 1, width, 1);
        _left.pixelInset = new Rect(0, 0, 1, height);
        _right.pixelInset = new Rect(width - 1, 0, 1, height);

        _text.pixelOffset = new Vector2(TEXT_BORDER, height - TEXT_BORDER);
    }

    private Vector2 GetTransformPosition(Vector2 positionInPixel)
    {
        return new Vector2(positionInPixel.x / Screen.width, positionInPixel.y / Screen.height);
    }
}
