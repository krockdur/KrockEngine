using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

namespace MyMGPixelEngine;

public class Game1 : Game
{
    const int WINDOW_WIDTH = 100;
    const int WINDOW_HEIGHT = 100;
    const int scale = 5;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private FrameBuffer framebuffer;


    PixelMap myPicture;
    


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

        myPicture = new PixelMap(@"pictures/test");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        framebuffer = new FrameBuffer(GraphicsDevice, WINDOW_WIDTH, WINDOW_HEIGHT);
        framebuffer.Clear();
    }

    int pos_pixel_x, pos_pixel_y = 0;
    double time_elapsed = 0;
    int activeColor = ColorList.White;

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        if (Keyboard.GetState().IsKeyDown(Keys.D1))
        {
            activeColor = ColorList.White;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.D2))
        {
            activeColor = ColorList.Red;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.D3))
        {
            activeColor = ColorList.Blue;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.D4))
        {
            activeColor = ColorList.Yellow;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.D5))
        {
            activeColor = ColorList.Black;

        }

        MouseState mouseState = Mouse.GetState();

        time_elapsed = gameTime.ElapsedGameTime.TotalMilliseconds + time_elapsed;

        if (time_elapsed >= 66)
        {
            //framebuffer.UpdateGravity();
            time_elapsed = 0;
        }


        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            framebuffer.SetPixel(mouseState.Position.X / scale, mouseState.Position.Y / scale, activeColor);
        }

        framebuffer.addPicture(WINDOW_WIDTH-16, WINDOW_HEIGHT-15, myPicture);

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
