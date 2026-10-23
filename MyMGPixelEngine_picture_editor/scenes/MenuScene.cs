using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyMGPixelEngine;


internal class MenuScene : Scene
{
    private Button[] _buttons;
    public override void Initialize()
    {
        _buttons = new Button[]
        {
            new Button(50, 40, 100, 15, "JOUER", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(50, 70, 100, 15, "OPTIONS", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(50, 100, 100, 15, "SCORES", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black)
        };
    }

    public override void LoadContent()
    {

    }

    private MouseState _previousMouseState;
    public override void Update(GameTime gameTime, SceneManager sceneManager)
    {
        MouseState mouseState = Mouse.GetState();
        foreach (var button in _buttons)
        {
            button.Update(mouseState, _previousMouseState);
        }

        foreach (var button in _buttons)
        {
            if (button.IsPressed)
            {
                Debug.WriteLine($"boutton appuyé : '{button.Text}'");
            }
        }

        if (_buttons[0].IsPressed)
        {
            sceneManager.Change("level1");
        }

        _previousMouseState = mouseState;

    }

    public override void Draw(ref FrameBuffer framebuffer)
    {
        foreach (var button in _buttons)
        {
            //framebuffer.addButton(button);
            button.AddToFramebuffer(ref framebuffer);
        }
    }
}