using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MyMGPixelEngine;

internal class EditorMenuScene : Scene
{

    private Button[] _buttons;
    public override void Initialize()
    {
        _buttons = new Button[]
        {
            new Button(50, 10, 100, 15, "8 X 8", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(50, 40, 100, 15, "16 X 16", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(50, 70, 100, 15, "32 X 32", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(50, 100, 100, 15, "64 X 64", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(50, 130, 100, 15, "128 X 128", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black)
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
                switch (button.Text)
                {
                    case "8 X 8":
                    sceneManager.Change("editor_8x8");
                    break;
                    case "16 X 16":
                    sceneManager.Change("editor_16x16");
                    break;
                    case "32 x 32":
                    sceneManager.Change("editor_32x32");
                    break;
                    case "64 X 64":
                    sceneManager.Change("editor_64x64");
                    break;
                    case "128 X 128":
                    sceneManager.Change("editor_128x128");
                    break;
                    case "OPEN":
                    break;
                    case "EXIT":
                    break;
                }
            }
        }


        _previousMouseState = mouseState;
    }

    public override void Draw(ref FrameBuffer framebuffer)
    {
        foreach (var button in _buttons)
        {
            button.AddToFramebuffer(ref framebuffer);
        }
    }
}