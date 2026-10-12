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
    }
}
