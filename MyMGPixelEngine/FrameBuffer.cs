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

        private readonly Color[] pixels;
        private readonly Texture2D texture;

        public Texture2D Texture => texture;

        public FrameBuffer(GraphicsDevice graphicsDevice, int width, int height)
        {
            Width = width;
            Height = height;

            pixels = new Color[width * height];
            texture = new Texture2D(graphicsDevice, width, height);
        }

        public void Clear(Color color)
        {
            Array.Fill(pixels, color);
            Array.Fill(pixels, Color.Transparent);
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return;

            pixels[y * Width + x] = color;
        }

        public Color GetPixel(int x, int y)
        {
            return pixels[y * Width + x];
        }

        public void Upload()
        {
            texture.SetData(pixels);
        }

        /// <summary>
        /// Déplace vers le bas (1 pixel par frame) tous les pixels égaux à fallingColor
        /// en les faisant tomber dans des pixels égaux à emptyColor. Balayage de bas en haut
        /// pour éviter double-déplacement.
        /// </summary>
        public void UpdateGravity(Color fallingColor, Color emptyColor)
        {
            // Vérifiez si la hauteur est suffisante pour que les pixels puissent tomber
            if (Height <= 1)
                return;

            // Déplacez les pixels égaux à fallingColor vers le bas (1 pixel par frame)
            // en les faisant tomber dans des pixels égaux à emptyColor.
            // Balayage de bas en haut pour éviter double-déplacement.
            for (int y = Height - 2; y >= 0; y--)   
            {
                int rowOffset = y * Width;
                int belowOffset = (y + 1) * Width;
                for (int x = 0; x < Width; x++)
                {
                    int idx = rowOffset + x;
                    if (pixels[idx] == fallingColor)
                    {
                        int bIdx = belowOffset + x;
                        if (pixels[bIdx] == emptyColor)
                        {
                            pixels[bIdx] = fallingColor;
                            pixels[idx] = emptyColor;
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
            UpdateGravity(Color.Yellow, Color.Transparent);
        }

        public Color[] BufferToPixels(int[] mapbuffer)
        {
            Color[] pixels = new Color[mapbuffer.Length];
            for (int i = 0; i < mapbuffer.Length; i++)
            {
                switch (mapbuffer[i])
                {
                    case 0:
                        pixels[i] = Color.Transparent;
                        break;
                    case 1:
                        pixels[i] = Color.White;
                        break;
                    case 2:
                        pixels[i] = Color.Red;
                        break;
                    case 3:
                        pixels[i] = Color.Blue;
                        break;
                    case 4:
                        pixels[i] = Color.Yellow;
                        break;
                }
            }
            return pixels;
        }
    }
}

