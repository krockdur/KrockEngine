using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

namespace MyMGPixelEngine;

public class Game1 : Game
{

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private FrameBuffer framebuffer;


    PixelMap myPicture;


    Button[] buttons;



    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = Config.WINDOW_WIDTH * Config.SCALE;
        _graphics.PreferredBackBufferHeight = Config.WINDOW_HEIGHT * Config.SCALE;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        framebuffer = new FrameBuffer(GraphicsDevice, Config.WINDOW_WIDTH, Config.WINDOW_WIDTH);
        framebuffer.Clear();

        buttons = new Button[]
        {
            new Button(0, 0, 50, 10, "CLICK ME"),
            //new Button(10, 80, 100, 50, "CLIQUE MOI"),
            //new Button(10, 120, 100, 50, "ET MOI")
        };

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        foreach (var button in buttons)
        {
            framebuffer.addButton(button);
        }


    }
    int pos_pixel_x, pos_pixel_y = 0;
    double time_elapsed = 0;
    int activeColor = ColorList.White;
    private MouseState _previousMouseState;
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
        else if (Keyboard.GetState().IsKeyDown(Keys.F5))
        {
            // Save to file
        }


        MouseState mouseState = Mouse.GetState();

        foreach (var button in buttons)
        {
            button.Update(mouseState, _previousMouseState);
        }

        /*         if (mouseState.LeftButton == ButtonState.Pressed)
        {
            framebuffer.SetPixel(mouseState.Position.X / Config.SCALE, mouseState.Position.Y / Config.SCALE, activeColor);
        } */

        foreach (var button in buttons)
        {
            if (button.IsPressed)
            {
                Debug.WriteLine($"boutton appuyé : '{button.Text}'");
            }
        }

        //framebuffer.AddText("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 10, 10, ColorList.Blue, ColorList.White);
        //framebuffer.AddText("0123456789", 10, 20, ColorList.Red, ColorList.Yellow);
        //framebuffer.AddText(" -:", 10, 30, ColorList.White, ColorList.Black);

        //framebuffer.AddCar('B', 0,0, ColorList.Black);



        framebuffer.Upload();
        // OBLIGATOIRE : Sauvegarder l'état pour la frame suivante
        _previousMouseState = mouseState;

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
