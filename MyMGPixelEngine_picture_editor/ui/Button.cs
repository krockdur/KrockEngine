
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MyMGPixelEngine;

internal class Button
{
    private int[,] _pixelsButton;

    private int _position_x;
    public int PositonX => _position_x;

    private int _position_y;
    public int PositonY => _position_y;

    private int _width;
    public int Width => _width;

    private int _height;
    public int Height => _height;


    private bool _isHover;
    public bool IsPressed { get; set; }
    public string Text { get; set; }

    private int _borderColor;
    private int _backgroundColor;
    private int _textColor;
    private int _pressBorderColor;
    private int _pressBackgroundColor;
    private int _pressTextColor;


    public Button(
        int positionX,
        int positionY,
        int width,
        int height,
        string text,
        int borderColor,
        int backgroundColor,
        int textColor,
        int pressBorderColor,
        int pressBackgroundColor,
        int pressTextColor)
    {
        _pixelsButton = new int[height, width];

        _position_x = positionX;
        _position_y = positionY;
        _width = width;
        _height = height;
        Text = text;
        _backgroundColor = backgroundColor;
        _borderColor = borderColor;
        _textColor = textColor;
        _pressBackgroundColor = pressBackgroundColor;
        _pressBorderColor = pressBorderColor;
        _pressTextColor = pressTextColor;

        IsPressed = false;

        make();

    }

    private void make()
    {
        // Cadre et fond
        for (int j = 0; j < _height; j++)
        {
            for (int i = 0; i < _width; i++)
            {
                _pixelsButton[j, i] = (_isHover) ? _pressBackgroundColor : _backgroundColor;

                if (i == 0 || j == 0 || i == _width - 1 || j == _height - 1)
                {
                    _pixelsButton[j, i] = (_isHover) ? _pressBorderColor : _borderColor;
                }
                    
            }
        }

        int textPixelsLength = Text.Length * 7; // longueur en pixel du text

        // le texte ne peut pas sortir du bouton
        if (textPixelsLength <= _width)
        {
            addText(Text, _width / 2 - textPixelsLength / 2, _height / 2 - 3, _textColor, _backgroundColor);
        }
    }

    private void addCar(char c, int positionX, int positionY, int frontColor, int backColor)
    {
        if (!Font.Font8x7.TryGetValue(c, out var glyph))
            return;

        for (int y = 0; y < 7; y++)
        {
            bool[] bits = new bool[8];

            for (int i = 0; i < 8; i++)
            {
                bits[i] = (glyph[y] & (1 << (7 - i))) != 0;
            }

            for (int x = 0; x < 8; x++)
            {
                int destX = x + positionX;
                int destY = y + positionY;

                int tmpPix = backColor;
                if (bits[x] == true) tmpPix = frontColor;
                _pixelsButton[destY, destX] = tmpPix;

            }
        }
    }

    private void addText(string text, int positionX, int positionY, int frontColor, int backColor)
    {
        for (int i = 0; i < text.Length; i++)
        {
            addCar(text[i], positionX + i * 7, positionY, frontColor, backColor);
        }
    }

    bool _wasIsHover = false;
    public void Update(MouseState currentMouseState, MouseState previousMouseState)
    {
        IsPressed = false;

        int mouseX = currentMouseState.Position.X / Config.SCALE;
        int mouseY = currentMouseState.Position.Y / Config.SCALE;

        
        // 
        _isHover = mouseX >= _position_x && mouseX < _position_x + _width &&
                         mouseY >= _position_y && mouseY < _position_y + _height;
        if (_isHover)
        {
            make();
            _wasIsHover = true;        
        }
        if (!_isHover && _wasIsHover)
        {
            make();
            _wasIsHover = false;
        }

        // Déclenché à l'instant exact où le bouton est appuyé
        if (_isHover &&
            currentMouseState.LeftButton == ButtonState.Pressed &&
            previousMouseState.LeftButton == ButtonState.Released)
        {
            IsPressed = true;
        }
    }

    public void AddToFramebuffer(ref FrameBuffer frameBuffer)
    {
        PixelMap tmpPm = new PixelMap(_width, _height, _pixelsButton);
        frameBuffer.addPicture(_position_x, _position_y, tmpPm);
    }
}