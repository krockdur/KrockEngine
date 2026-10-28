using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyMGPixelEngine;


internal class Editor8x8Scene : Scene
{

    Sprite back_draw_zone_sprite;

    private int _pixelSize = 16;

    private List<Pixel> _pixelsList = new List<Pixel>();
    public override void Initialize()
    {
        back_draw_zone_sprite = new Sprite(@"pictures/8x8_interface", 0, 0);
    }

    public override void LoadContent()
    {

    }

    int clickPositonX = 0;
    int clickPositonY = 0;

    public override void Update(GameTime gameTime, SceneManager sceneManager)
    {
        MouseState mouseState = Mouse.GetState();

         _pixelsList.Add(new Pixel(0, 0, 4));
         _pixelsList.Add(new Pixel(127, 127, 4));

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            clickPositonX = mouseState.Position.X / Config.SCALE;
            clickPositonY = mouseState.Position.Y / Config.SCALE;
            int caseX = clickPositonX / _pixelSize;
            int caseY = clickPositonY / _pixelSize;

            if (clickPositonX >= 0 && clickPositonX <= 128 && clickPositonY >= 0 && clickPositonY <= 128 )
            {

                for (int i = 0; i < _pixelSize; i++)
                {
                    for (int j = 0; j < _pixelSize; j++)
                    {
                        _pixelsList.Add(new Pixel(caseX * _pixelSize + j, caseY * _pixelSize + i, 3));
                    }
                }

            }

        }
    }


    public override void Draw(ref FrameBuffer framebuffer)
    {

        back_draw_zone_sprite.AddToFramebuffer(ref framebuffer);

        foreach (Pixel pixel in _pixelsList)
        {
            framebuffer.SetPixel(pixel.X / Config.SCALE, pixel.Y / Config.SCALE, pixel.Color);

        }


    }
}

internal class Pixel
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Color { get; set; }
    public Pixel(int x, int y, int color)
    {
        X = x;
        Y = y;
        Color = color;
    }
}