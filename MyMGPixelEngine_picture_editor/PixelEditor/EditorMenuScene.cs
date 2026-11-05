using System.Collections.Specialized;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MyMGPixelEngine;

internal class EditorMenuScene : Scene
{

    private Button[] _buttons;
    private Textbox tbOpenFile;
    private Sprite _bgSprite;
    private int[,] tabPixelPayload; // tableau de pixels de l'image à transmettre à l'éditeur - a refaire à l'occase
    public override void Initialize(object payload = null)
    {
        _bgSprite = new Sprite("pictures/chat");

        _buttons = new Button[]
        {
            new Button(90, 10, 100, 15, "8 X 8", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(90, 40, 100, 15, "16 X 16", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(90, 70, 100, 15, "32 X 32", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(90, 100, 100, 15, "64 X 64", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(90, 130, 100, 15, "128 X 128", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black),
            new Button(5, 40, 75, 15, "OPEN", ColorList.White, ColorList.Yellow, ColorList.Blue, ColorList.Red, ColorList.White, ColorList.Black)
        };

        tbOpenFile = new Textbox(5, 10, 75, 15, ColorList.DarkGray, ColorList.LightGray, ColorList.Black, ColorList.MidnightBlue, ColorList.LightGray);

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

        tbOpenFile.Update(mouseState, _previousMouseState);

        foreach (var button in _buttons)
        {
            if (button.IsPressed)
            {
                switch (button.Text)
                {
                    case "8 X 8":
                        sceneManager.Change("editor", new EditorPayload("8X8"));
                        break;
                    case "16 X 16":
                        sceneManager.Change("editor", new EditorPayload("16X16"));
                        break;
                    case "32 X 32":
                        sceneManager.Change("editor", new EditorPayload("32X32"));
                        break;
                    case "64 X 64":
                        sceneManager.Change("editor", new EditorPayload("64X64"));
                        break;
                    case "128 X 128":
                        sceneManager.Change("editor", new EditorPayload("128X128"));
                        break;
                    case "OPEN":
                        sceneManager.Change("editor", new EditorPayload("", tbOpenFile.Text));
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
        _bgSprite.AddToFramebuffer(ref framebuffer);

        foreach (var button in _buttons)
        {
            button.AddToFramebuffer(ref framebuffer);
        }

        tbOpenFile.AddToFramebuffer(ref framebuffer);
    }
}

internal class EditorPayload
{
    public EditorPayload(string type, string file = null)
    {
        Type = type;
        if (file != null)
        {
            File = file;
        }
    }
    public string Type { get; set; }
    public string File{ get; set; } = null;
}