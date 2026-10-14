using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyMGPixelEngine
{
    internal class FrameBuffer
    {
        public readonly int Width;
        public readonly int Height;

        private readonly Color[] pixels;    // Tableau de pixels à rendre
        private readonly int[] pixelsCopy;  // Copie du tableau de pixels à rendre
        private readonly Texture2D texture;

        public Texture2D Texture => texture;

        public FrameBuffer(GraphicsDevice graphicsDevice, int width, int height)
        {
            Width = width;
            Height = height;

            pixels = new Color[width * height];
            pixelsCopy = new int[width * height];

            texture = new Texture2D(graphicsDevice, width, height);
        }

        public void Clear()
        {
            Array.Fill(pixelsCopy, ColorList.Black);  // Remplir le tableau de pixels avec le pixel de fond
        }

        public void SetPixel(int x, int y, int color)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return;

            pixelsCopy[y * Width + x] = color;
        }

        public int GetPixel(int x, int y)
        {
            return pixelsCopy[y * Width + x];
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
                        pixelsCopy[destY * Width + destX] = picture.getPicture()[y, x];
                    }
                }
            }
        }

        public void addCar(char c, int positionX, int positionY, uint color)
        {
            if (!Font.Font5x7.TryGetValue(c, out var glyph))
                return;

            for (int y = 0; y < 7; y++)
            {
                bool[] bits = new bool[8];

                for (int i = 0; i < 8; i++)
                {
                    bits[i] = (glyph[y] & (1 << i)) != 0;
                }

                for (int x = 0; x < 5; x++)
                {
                    int destX = x + positionX;
                    int destY = y + positionY;

                    if (destX >= 0 && destX < Width && destY >= 0 && destY < Height)
                    {
                        int tmpPix = 0;
                        if (bits[x] == true) tmpPix = 1;
                        pixelsCopy[destY * Width + destX] = tmpPix;
                    }
                }
            }
        }

        public void Upload()
        {

            texture.SetData(BufferToPixels(pixelsCopy));
        }

        /// <summary>
        /// Déplace vers le bas (1 pixel par frame) tous les pixels égaux à fallingColor
        /// en les faisant tomber dans des pixels égaux à emptyColor. Balayage de bas en haut
        /// pour éviter double-déplacement.
        /// </summary>

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

                    if (pixelsCopy[idx] != fallingColor)
                        continue;

                    int downIdx = belowOffset + x;

                    // Chute verticale
                    if (pixelsCopy[downIdx] == emptyColor)
                    {
                        pixelsCopy[downIdx] = fallingColor;
                        pixelsCopy[idx] = emptyColor;
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
                            if (pixelsCopy[downLeft] == emptyColor)
                            {
                                pixelsCopy[downLeft] = fallingColor;
                                pixelsCopy[idx] = emptyColor;
                                continue;
                            }
                        }

                        // Bas-droite
                        if (x < Width - 1)
                        {
                            int downRight = belowOffset + x + 1;
                            if (pixelsCopy[downRight] == emptyColor)
                            {
                                pixelsCopy[downRight] = fallingColor;
                                pixelsCopy[idx] = emptyColor;
                            }
                        }
                    }
                    else
                    {
                        // Bas-droite
                        if (x < Width - 1)
                        {
                            int downRight = belowOffset + x + 1;
                            if (pixelsCopy[downRight] == emptyColor)
                            {
                                pixelsCopy[downRight] = fallingColor;
                                pixelsCopy[idx] = emptyColor;
                                continue;
                            }
                        }

                        // Bas-gauche
                        if (x > 0)
                        {
                            int downLeft = belowOffset + x - 1;
                            if (pixelsCopy[downLeft] == emptyColor)
                            {
                                pixelsCopy[downLeft] = fallingColor;
                                pixelsCopy[idx] = emptyColor;
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

                    if (pixelsCopy[idx] != waterColor)
                        continue;

                    // 1. Chute verticale
                    int down = belowOffset + x;

                    if (pixelsCopy[down] == emptyColor)
                    {
                        pixelsCopy[down] = waterColor;
                        pixelsCopy[idx] = emptyColor;
                        continue;
                    }

                    bool leftFirst = rnd.Next(2) == 0;

                    // 2. Diagonales descendantes
                    if (leftFirst)
                    {
                        if (x > 0)
                        {
                            int downLeft = belowOffset + x - 1;
                            if (pixelsCopy[downLeft] == emptyColor)
                            {
                                pixelsCopy[downLeft] = waterColor;
                                pixelsCopy[idx] = emptyColor;
                                continue;
                            }
                        }

                        if (x < Width - 1)
                        {
                            int downRight = belowOffset + x + 1;
                            if (pixelsCopy[downRight] == emptyColor)
                            {
                                pixelsCopy[downRight] = waterColor;
                                pixelsCopy[idx] = emptyColor;
                                continue;
                            }
                        }
                    }
                    else
                    {
                        if (x < Width - 1)
                        {
                            int downRight = belowOffset + x + 1;
                            if (pixelsCopy[downRight] == emptyColor)
                            {
                                pixelsCopy[downRight] = waterColor;
                                pixelsCopy[idx] = emptyColor;
                                continue;
                            }
                        }

                        if (x > 0)
                        {
                            int downLeft = belowOffset + x - 1;
                            if (pixelsCopy[downLeft] == emptyColor)
                            {
                                pixelsCopy[downLeft] = waterColor;
                                pixelsCopy[idx] = emptyColor;
                                continue;
                            }
                        }
                    }

                    // 3. Écoulement horizontal
                    if (leftFirst)
                    {
                        if (x > 0 && pixelsCopy[idx - 1] == emptyColor)
                        {
                            pixelsCopy[idx - 1] = waterColor;
                            pixelsCopy[idx] = emptyColor;
                            continue;
                        }

                        if (x < Width - 1 && pixelsCopy[idx + 1] == emptyColor)
                        {
                            pixelsCopy[idx + 1] = waterColor;
                            pixelsCopy[idx] = emptyColor;
                        }
                    }
                    else
                    {
                        if (x < Width - 1 && pixelsCopy[idx + 1] == emptyColor)
                        {
                            pixelsCopy[idx + 1] = waterColor;
                            pixelsCopy[idx] = emptyColor;
                            continue;
                        }

                        if (x > 0 && pixelsCopy[idx - 1] == emptyColor)
                        {
                            pixelsCopy[idx - 1] = waterColor;
                            pixelsCopy[idx] = emptyColor;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Par défaut : jaune tombe et l'espace vide est Color.Transparent.
        /// </summary>
        public void UpdateGravity()
        {
            UpdateSandGravity(ColorList.Yellow, ColorList.Black);
            UpdateWaterGravity(ColorList.Blue, ColorList.Black);

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
}

