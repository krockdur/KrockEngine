
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMGPixelEngine;

internal class Button
{
    public int[,] pixelsButton { get; }

    private int _position_x;
    public int PositonX => _position_x;

    private int _position_y;
    public int PositonY => _position_y;

    private int _width;
    public int Width => _width;

    private int _height;
    public int Height => _height;


    public bool IsPressed { get; set; }
    public string Text { get; set; }


    public Button(int positionX, int positionY, int width, int height, string text)
    {
        pixelsButton = new int[height, width];

        _position_x = positionX;
        _position_y = positionY;
        _width = width;
        _height = height;
        Text = text;

        // Cadre et fond
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                pixelsButton[j, i] = ColorList.White;
                if (i == 0 || j == 0 || i == width - 1 || j == height - 1) pixelsButton[j, i] = ColorList.Red;
            }
        }

        int textPixelsLength = text.Length * 7; // longueur en pixel du text

        // le texte ne peut pas sortir du bouton
        if (textPixelsLength <= width)
        {
            addText(text, width / 2 - textPixelsLength / 2, height / 2 - 3, ColorList.Red, ColorList.White);
        }

        IsPressed = false;

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
                pixelsButton[destY, destX] = tmpPix;

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


    private bool _wasPressed = false;
public void Update(MouseState currentMouseState, MouseState previousMouseState)
{
    IsPressed = false;

    int mouseX = currentMouseState.Position.X / Config.SCALE;
    int mouseY = currentMouseState.Position.Y / Config.SCALE;

    bool isHovered = mouseX >= _position_x && mouseX < _position_x + _width &&
                     mouseY >= _position_y && mouseY < _position_y + _height;

    // Déclenché à l'instant exact où le bouton est appuyé
    if (isHovered && 
        currentMouseState.LeftButton == ButtonState.Pressed && 
        previousMouseState.LeftButton == ButtonState.Released)
    {
        IsPressed = true;
    }
}
}