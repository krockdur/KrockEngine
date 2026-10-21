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
    Text textHour1;


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

        framebuffer = new FrameBuffer(GraphicsDevice, Config.WINDOW_WIDTH, Config.WINDOW_HEIGHT);
        framebuffer.Clear();

        initializeUI();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
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

        // UI
        refreshFramebufferUI();

        Sprite sp = new Sprite(@"pictures/test");
        sp.SetPosition(15, 50);
        sp.AddToFramebuffer(ref framebuffer);

        // Chargement du framebuffer dans la texture à déssiner
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

    private void initializeUI()
    {
        buttons = new Button[]
        {
            new Button(0,   0,      50, 15, "UN", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(0,   50,     50, 15, "DEUX", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(0,  100,    50, 15, "TROIS", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(100,   0,      50, 15, "QUATRE", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(100,  50,     50, 15, "CINQ", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(100,  100,    50, 15, "SIX", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),

        };

        textHour1 = new Text("", 100, 75, 1, ColorList.White, ColorList.Black);
    }
    private void refreshFramebufferUI()
    {
        foreach (var button in buttons)
        {
            //framebuffer.addButton(button);
            button.AddToFramebuffer(ref framebuffer);

            string tmp = DateTime.Now.Hour.ToString().ToUpper()
            + " : " + DateTime.Now.Minute.ToString().ToUpper()
            + " : " + DateTime.Now.Second.ToString().ToUpper();

            textHour1.SetText(tmp);

            textHour1.SetBackColor(ColorList.Blue);
            textHour1.AddToFramebuffer(ref framebuffer);
        }
    }
}
