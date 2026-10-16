
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMGPixelEngine;

internal class Button
{
    public int[,] pixelsButton { get; }
    private int Width;
    private int Height;
    public Button(int width, int height, string text)
    {
        pixelsButton = new int[height, width];

        Width = width;
        Height = height;

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
            addText(text, width/2 - textPixelsLength/2, height/2-3, ColorList.Red, ColorList.White);            
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

}