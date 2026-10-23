using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyMGPixelEngine;

internal class FrameBuffer
{
    public readonly int Width;
    public readonly int Height;

    private readonly int[] _framebuffer;
    private readonly Texture2D texture;

    public Texture2D Texture => texture;

    public FrameBuffer(GraphicsDevice graphicsDevice, int width, int height)
    {
        Width = width;
        Height = height;

        _framebuffer = new int[width * height];

        texture = new Texture2D(graphicsDevice, width, height);
    }

    public void Clear()
    {
        Array.Fill(_framebuffer, ColorList.Black);  // Remplir le tableau de pixels avec le pixel de fond
    }

    public void SetPixel(int x, int y, int color)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
            return;

        _framebuffer[y * Width + x] = color;
    }

    public int GetPixel(int x, int y)
    {
        return _framebuffer[y * Width + x];
    }

    public void addPicture(int positionX, int positionY, PixelMap picture)
    {
        for (int y = 0; y < picture.Height; y++)
        {
            for (int x = 0; x < picture.Width; x++)
            {
                int destX = x + positionX;
                int destY = y + positionY;

                if (destX >= 0 && destX < Width && destY >= 0 && destY < Height)
                {
                    _framebuffer[destY * Width + destX] = picture.getPicture()[y, x];
                }
                else
                {
                    Debug.WriteLine("dring");
                }
            }
        }
    }

    public void AddCar(char c, int positionX, int positionY, int frontColor, int backColor)
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

                if (destX >= 0 && destX < Width && destY >= 0 && destY < Height)
                {
                    int tmpPix = backColor;
                    if (bits[x] == true) tmpPix = frontColor;
                    _framebuffer[destY * Width + destX] = tmpPix;
                }
            }
        }
    }

    public void AddText(string text, int positionX, int positionY, int frontColor, int backColor)
    {
        for (int i = 0; i < text.Length; i++)
        {
            AddCar(text[i], positionX + i * 7, positionY, frontColor, backColor);
        }
    }

    public void UpdateSandGravity(int fallingColor, int emptyColor)
    {
        if (Height <= 1)
            return;

        Random rnd = Random.Shared;

        for (int y = Height - 2; y >= 0; y--)
        {
            int rowOffset = y * Width;
            int belowOffset = (y + 1) * Width;

            for (int x = 0; x < Width; x++)
            {
                int idx = rowOffset + x;

                if (_framebuffer[idx] != fallingColor)
                    continue;

                int downIdx = belowOffset + x;

                // Chute verticale
                if (_framebuffer[downIdx] == emptyColor)
                {
                    _framebuffer[downIdx] = fallingColor;
                    _framebuffer[idx] = emptyColor;
                    continue;
                }

                // Ordre aléatoire gauche/droite
                bool tryLeftFirst = rnd.Next(2) == 0;

                if (tryLeftFirst)
                {
                    // Bas-gauche
                    if (x > 0)
                    {
                        int downLeft = belowOffset + x - 1;
                        if (_framebuffer[downLeft] == emptyColor)
                        {
                            _framebuffer[downLeft] = fallingColor;
                            _framebuffer[idx] = emptyColor;
                            continue;
                        }
                    }

                    // Bas-droite
                    if (x < Width - 1)
                    {
                        int downRight = belowOffset + x + 1;
                        if (_framebuffer[downRight] == emptyColor)
                        {
                            _framebuffer[downRight] = fallingColor;
                            _framebuffer[idx] = emptyColor;
                        }
                    }
                }
                else
                {
                    // Bas-droite
                    if (x < Width - 1)
                    {
                        int downRight = belowOffset + x + 1;
                        if (_framebuffer[downRight] == emptyColor)
                        {
                            _framebuffer[downRight] = fallingColor;
                            _framebuffer[idx] = emptyColor;
                            continue;
                        }
                    }

                    // Bas-gauche
                    if (x > 0)
                    {
                        int downLeft = belowOffset + x - 1;
                        if (_framebuffer[downLeft] == emptyColor)
                        {
                            _framebuffer[downLeft] = fallingColor;
                            _framebuffer[idx] = emptyColor;
                        }
                    }
                }
            }
        }
    }

    public void UpdateWaterGravity(int waterColor, int emptyColor)
    {
        if (Height <= 1)
            return;

        Random rnd = Random.Shared;

        for (int y = Height - 2; y >= 0; y--)
        {
            int rowOffset = y * Width;
            int belowOffset = (y + 1) * Width;

            for (int x = 0; x < Width; x++)
            {
                int idx = rowOffset + x;

                if (_framebuffer[idx] != waterColor)
                    continue;

                // 1. Chute verticale
                int down = belowOffset + x;

                if (_framebuffer[down] == emptyColor)
                {
                    _framebuffer[down] = waterColor;
                    _framebuffer[idx] = emptyColor;
                    continue;
                }

                bool leftFirst = rnd.Next(2) == 0;

                // 2. Diagonales descendantes
                if (leftFirst)
                {
                    if (x > 0)
                    {
                        int downLeft = belowOffset + x - 1;
                        if (_framebuffer[downLeft] == emptyColor)
                        {
                            _framebuffer[downLeft] = waterColor;
                            _framebuffer[idx] = emptyColor;
                            continue;
                        }
                    }

                    if (x < Width - 1)
                    {
                        int downRight = belowOffset + x + 1;
                        if (_framebuffer[downRight] == emptyColor)
                        {
                            _framebuffer[downRight] = waterColor;
                            _framebuffer[idx] = emptyColor;
                            continue;
                        }
                    }
                }
                else
                {
                    if (x < Width - 1)
                    {
                        int downRight = belowOffset + x + 1;
                        if (_framebuffer[downRight] == emptyColor)
                        {
                            _framebuffer[downRight] = waterColor;
                            _framebuffer[idx] = emptyColor;
                            continue;
                        }
                    }

                    if (x > 0)
                    {
                        int downLeft = belowOffset + x - 1;
                        if (_framebuffer[downLeft] == emptyColor)
                        {
                            _framebuffer[downLeft] = waterColor;
                            _framebuffer[idx] = emptyColor;
                            continue;
                        }
                    }
                }

                // 3. Écoulement horizontal
                if (leftFirst)
                {
                    if (x > 0 && _framebuffer[idx - 1] == emptyColor)
                    {
                        _framebuffer[idx - 1] = waterColor;
                        _framebuffer[idx] = emptyColor;
                        continue;
                    }

                    if (x < Width - 1 && _framebuffer[idx + 1] == emptyColor)
                    {
                        _framebuffer[idx + 1] = waterColor;
                        _framebuffer[idx] = emptyColor;
                    }
                }
                else
                {
                    if (x < Width - 1 && _framebuffer[idx + 1] == emptyColor)
                    {
                        _framebuffer[idx + 1] = waterColor;
                        _framebuffer[idx] = emptyColor;
                        continue;
                    }

                    if (x > 0 && _framebuffer[idx - 1] == emptyColor)
                    {
                        _framebuffer[idx - 1] = waterColor;
                        _framebuffer[idx] = emptyColor;
                    }
                }
            }
        }
    }

    public void UpdateGravity()
    {
        UpdateSandGravity(ColorList.Yellow, ColorList.Black);
        UpdateWaterGravity(ColorList.Blue, ColorList.Black);

    }

    public void Upload()
    {
        texture.SetData(BufferToPixels(_framebuffer));
    }
    public Color[] BufferToPixels(int[] mapbuffer)
    {
        Color[] pixels = new Color[mapbuffer.Length];
        for (int i = 0; i < mapbuffer.Length; i++)
        {
            switch (mapbuffer[i])
            {
                case ColorList.Black:
                    pixels[i] = Color.Black;
                    break;
                case ColorList.White:
                    pixels[i] = Color.White;
                    break;
                case ColorList.Red:
                    pixels[i] = Color.Red;
                    break;
                case ColorList.Blue:
                    pixels[i] = Color.Blue;
                    break;
                case ColorList.Yellow:
                    pixels[i] = Color.Yellow;
                    break;
            }
        }
        return pixels;
    }

}


