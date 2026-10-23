

using System.IO;
namespace MyMGPixelEngine;

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

    public Sprite(string filepath)
    {
        _pixelmap = new PixelMap(filepath);
        this._width = _pixelmap.Width;
        this._height = _pixelmap.Height;
        this._positionX = 0;
        this._positionY = 0;
    }

    public Sprite(string filepath, int positionX, int positionY)
    {
        _pixelmap = new PixelMap(filepath);
        this._width = _pixelmap.Width;
        this._height = _pixelmap.Height;
        this._positionX = positionX;
        this._positionY = positionY;
    }

    public Sprite(int width, int height, int[,] tabPixel)
    {
        _pixelmap = new PixelMap(width, height, tabPixel);
        this._width = _pixelmap.Width;
        this._height = _pixelmap.Height;
        this._positionX = 0;
        this._positionY = 0;
    }

    public void LoadFromFile(string path)
    {
        _pixelmap.SetPictureFromFile(path);
        this._width = _pixelmap.Width;
        this._height = _pixelmap.Height;
        this._positionX = 0;
        this._positionY = 0;
    }

    public void LoadFromPixelTab(int[,] tabPixel)
    {
        _pixelmap = new PixelMap(tabPixel.GetLength(1), tabPixel.GetLength(0), tabPixel);
        this._width = _pixelmap.Width;
        this._height = _pixelmap.Height;
        this._positionX = 0;
        this._positionY = 0;
    }

    public void SetPosition(int positionX, int positionY)
    {
        this._positionX = positionX;
        this._positionY = positionY;
    }

    public void AddToFramebuffer(ref FrameBuffer frameBuffer)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                int destX = x + _positionX;
                int destY = y + _positionY;

                if (destX >= 0 && destX < Config.WINDOW_WIDTH && destY >= 0 && destY < Config.WINDOW_HEIGHT)
                {
                    frameBuffer.SetPixel(destX, destY, _pixelmap.getPicture()[y, x]);
                }
            }
        }
    }

}