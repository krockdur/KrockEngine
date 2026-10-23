using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

namespace MyMGPixelEngine;

public class Game1 : Game
{
    private SceneManager _sceneManager;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private FrameBuffer framebuffer;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = Config.WINDOW_WIDTH * Config.SCALE;
        _graphics.PreferredBackBufferHeight = Config.WINDOW_HEIGHT * Config.SCALE;
        Content.RootDirectory = "Content";

        _sceneManager = new SceneManager();
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here


        framebuffer = new FrameBuffer(GraphicsDevice, Config.WINDOW_WIDTH, Config.WINDOW_HEIGHT);
        framebuffer.Clear();

        // Création des scènes
        _sceneManager.AddScene("menu", new MenuScene());
        _sceneManager.AddScene("level1", new Level1Scene());

        // choix d'une scène
        _sceneManager.Change("menu");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        _sceneManager.Update(gameTime);
        framebuffer.Clear();
        _sceneManager.Draw(ref framebuffer);
        

        // Chargement du framebuffer dans la texture à déssiner
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
