using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

namespace MyMGPixelEngine;

public class Game1 : Game
{
    const int WINDOW_WIDTH = 100;
    const int WINDOW_HEIGHT = 50;
    const int scale = 5;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private FrameBuffer framebuffer;



    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = WINDOW_WIDTH * scale;
        _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT * scale;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        framebuffer = new FrameBuffer(GraphicsDevice, WINDOW_WIDTH, WINDOW_HEIGHT);
        framebuffer.Clear(Color.Black);
    }

    int pos_pixel_x, pos_pixel_y = 0;
    double time_elapsed = 0;

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        MouseState mouseState = Mouse.GetState();




        time_elapsed = gameTime.ElapsedGameTime.TotalMilliseconds + time_elapsed;

        if (time_elapsed >= 66)
        {
            pos_pixel_x = pos_pixel_x + 4;
            if (pos_pixel_x >= WINDOW_WIDTH)
            {
                pos_pixel_x = 0;
                pos_pixel_y++;
                if (pos_pixel_y >= WINDOW_HEIGHT)
                {
                    pos_pixel_y = 0;
                }
            }
            time_elapsed = 0;
        }


        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            framebuffer.SetPixel(mouseState.Position.X, mouseState.Position.Y, Color.White);
        }


        //framebuffer.Clear(Color.Black);

        //framebuffer.SetPixel(pos_pixel_x, pos_pixel_y, Color.Red);
        //framebuffer.SetPixel(pos_pixel_x - 1, pos_pixel_y, Color.Red);
        //framebuffer.SetPixel(pos_pixel_x - 2, pos_pixel_y, Color.Red);
        //framebuffer.SetPixel(pos_pixel_x - 3, pos_pixel_y, Color.Red);


        framebuffer.Upload();


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _spriteBatch.Draw(
            framebuffer.Texture,
            new Rectangle(
                0,
                0,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height),
            Color.White);

        _spriteBatch.End();


        base.Draw(gameTime);
    }
}
