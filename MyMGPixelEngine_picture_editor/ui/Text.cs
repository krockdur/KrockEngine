namespace MyMGPixelEngine;

internal class Text
{
    private int _positionX;
    private int _positionY;
    private string _text;
    private int _scale;
    private int _frontColor;
    private int _backColor;

    public Text(string text, int positionX, int positionY, int scale, int frontColor, int backColor)
    {
        _positionX = positionX;
        _positionY = positionY;
        _text = text;
        _scale = scale;
        _frontColor = frontColor;
        _backColor = backColor;
    }

    public void SetText(string text)
    {
        _text = text;
    }

    public void SetFrontColor(int color)
    {
        _frontColor = color;
    }
    public void SetBackColor(int color)
    {
        _backColor = color;
    }
    public void Update()
    {
        // Squelette
    }

    public void AddToFramebuffer(ref FrameBuffer frameBuffer)
    {
        // Squelette
        AddText(ref frameBuffer);
    }

    private void AddText(ref FrameBuffer framebuffer)
    {
        for (int i = 0; i < _text.Length; i++)
        {
            AddCar(_text[i], _positionX + i * 7, _positionY, _frontColor, _backColor, ref framebuffer);
        }
    }

    private void AddCar(char c, int positionX, int positionY, int frontColor, int backColor, ref FrameBuffer framebuffer)
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

                if (destX >= 0 && destX < Config.WINDOW_WIDTH && destY >= 0 && destY < Config.WINDOW_HEIGHT)
                {
                    int tmpPix = backColor;
                    if (bits[x] == true) tmpPix = frontColor;
                    framebuffer.SetPixel(destX, destY, tmpPix);
                    //pixelsCopy[destY * Width + destX] = tmpPix;
                }
            }
        }
    }
}
