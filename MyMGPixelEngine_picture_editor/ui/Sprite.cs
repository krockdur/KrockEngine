

using System.IO;
using MyMGPixelEngine;

internal class Sprite
{
    private int _positionX;
    public int PositionX => _positionX;
    private int _positionY;
    public int PositionY => _positionY;
    private int _width;
    public int Width => _width;
    private int _height;
    public int Height => _height;
    private PixelMap _pixelmap;

    public Sprite()
    {
        _pixelmap = new PixelMap();
    }

    public Sprite(string filepath)
    {
        _pixelmap = new PixelMap(filepath);
        this._width = _pixelmap.Width;
        this._height = _pixelmap.Height;
    }

    public Sprite(int width, int height,int[,] tabPixel)
    {
        _pixelmap = new PixelMap(width, height, tabPixel);
        this._width = _pixelmap.Width;
        this._height = _pixelmap.Height;
    }

    public void LoadFromFile(string path)
    {
        _pixelmap.SetPictureFromFile(path);
    }

    public void LoadFromPixelTab(int[,] tabPixel)
    {
        
    }

    public void SetPosition(int positionX, int positionY)
    {
        
    }

    public void AddToFramebuffer(ref FrameBuffer frameBuffer)
    {
        
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

}