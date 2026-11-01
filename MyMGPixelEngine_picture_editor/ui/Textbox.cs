
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MyMGPixelEngine;

internal class Textbox
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
    public bool IsFocused { get; set; }
    public string Text { get; set; }

    private int _borderColor;
    private int _backgroundColor;
    private int _textColor;
    private int _focusBorderColor;
    private int _focusBackgroundColor;



    public Textbox(
        int positionX,
        int positionY,
        int width,
        int height,
        int borderColor,
        int backgroundColor,
        int textColor,
        int focusBorderColor,
        int focusBackgroundColor)
    {
        _pixelsButton = new int[height, width];

        _position_x = positionX;
        _position_y = positionY;
        _width = width;
        _height = height;

        _backgroundColor = backgroundColor;
        _borderColor = borderColor;
        _textColor = textColor;
        _focusBackgroundColor = focusBackgroundColor;
        _focusBorderColor = focusBorderColor;

        IsFocused = false;

        make();

    }

    private void make()
    {
        // Cadre et fond
        for (int j = 0; j < _height; j++)
        {
            for (int i = 0; i < _width; i++)
            {
                _pixelsButton[j, i] = (_isHover) ? _focusBackgroundColor : _backgroundColor;

                if (i == 0 || j == 0 || i == _width - 1 || j == _height - 1)
                {
                    _pixelsButton[j, i] = (_isHover) ? _focusBorderColor : _borderColor;
                }

            }
        }

        if (Text != null && Text.Length > 0)
        {
            int textPixelsLength = Text.Length * 8; // longueur en pixel du text

            // le texte ne peut pas sortir du bouton
            if (textPixelsLength < _width)
            {
                addText(Text, _width / 2 - textPixelsLength / 2, _height / 2 - 3, _textColor, _backgroundColor);
            }
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
    bool press = false;
    public void Update(MouseState currentMouseState, MouseState previousMouseState)
    {

        int mouseX = currentMouseState.Position.X / Config.SCALE;
        int mouseY = currentMouseState.Position.Y / Config.SCALE;


        bool wasHover = _isHover;
        _isHover = mouseX >= _position_x && mouseX < _position_x + _width &&
                   mouseY >= _position_y && mouseY < _position_y + _height;

        // Redessiner uniquement si l'état de survol change pour économiser des ressources
        if (_isHover != wasHover)
        {
            make();
        }

        // Focus si on clique dedans
        if (_isHover && currentMouseState.LeftButton == ButtonState.Pressed)
        {
            IsFocused = true;
        }

        // perte focus si on clique en dehors
        if (!_isHover && IsFocused && currentMouseState.LeftButton == ButtonState.Pressed)
        {
            IsFocused = false;
        }


        if (IsFocused)
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (Keyboard.GetState().IsKeyDown((Keys)c) && !press)
                {
                    Text = Text + c;
                    press = true;
                    make();
                }
            }

            for (char c = '0'; c <= '9'; c++)
            {
                if (Keyboard.GetState().IsKeyDown((Keys)c) && !press)
                {
                    Text = Text + c;
                    press = true;
                    make();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !press)
            {
                Text = Text + ' ';
                press = true;
                make();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && Keyboard.GetState().IsKeyDown(Keys.OemMinus) && !press)
            {
                Text = Text + '_';
                press = true;
                make();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus) && !press)
            {
                Text = Text + '-';
                press = true;
                make();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Back) && !press && Text.Length >= 1)
            {
                Text = Text.Remove(Text.Length - 1, 1);
                press = true;
                make();
            }
        }

        if (Keyboard.GetState().GetPressedKeyCount() == 0)
        {
            press = false;
        }
    }

    public void AddToFramebuffer(ref FrameBuffer frameBuffer)
    {
        PixelMap tmpPm = new PixelMap(_width, _height, _pixelsButton);
        frameBuffer.addPicture(_position_x, _position_y, tmpPm);
    }
}