using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MyMGPixelEngine;

internal class EditorMenuScene : Scene
{

    private Button[] _buttons;
    Textbox tbOpenFile;
    public override void Initialize(object payload = null)
    {
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
                        sceneManager.Change("editor", new EditorPayload(16));
                        break;
                    case "16 X 16":
                        sceneManager.Change("editor", new EditorPayload(8));
                        break;
                    case "32 X 32":
                        sceneManager.Change("editor", new EditorPayload(4));
                        break;
                    case "64 X 64":
                        sceneManager.Change("editor", new EditorPayload(2));
                        break;
                    case "128 X 128":
                        sceneManager.Change("editor", new EditorPayload(1));
                        break;
                    case "OPEN":
                        if (OpenFile(sceneManager)) sceneManager.Change("editor", new EditorPayload(8, tabPixelPayload));
                        break;
                    case "EXIT":
                        break;
                }
            }
        }



        _previousMouseState = mouseState;
    }

    int[,] tabPixelPayload;
    private bool OpenFile(SceneManager sceneManager)
    {
        bool success = false;
        using (StreamReader sr = new StreamReader("pictures/TOTO"))
        {
            int pictureWidth = int.Parse(sr.ReadLine());
            int pictureHeight = int.Parse(sr.ReadLine());
            tabPixelPayload = new int[pictureHeight, pictureWidth];

            int j = 0;
            while(sr.ReadLine() != null)
            {
                string line = sr.ReadLine();
                string[] tabLine = line.Split(' ');
                for (int i = 0; i < tabLine.Length; i++)
                {
                    tabPixelPayload[j, i] = int.Parse(tabLine[i]);
                }
                j++;
            }

            if (pictureHeight == 8 && pictureWidth == 8)
            {
                success = true;
            }
        }
        return success;
    }

    public override void Draw(ref FrameBuffer framebuffer)
    {
        foreach (var button in _buttons)
        {
            button.AddToFramebuffer(ref framebuffer);
        }

        tbOpenFile.AddToFramebuffer(ref framebuffer);
    }
}

internal class EditorPayload
{
    public EditorPayload(){}
    public EditorPayload(int nbEdgePixels)
    {
        NbEdgePixels = nbEdgePixels;
    }
    public EditorPayload(int nbEdgePixels, int[,] tabPixels)
    {
        NbEdgePixels = nbEdgePixels;
        TabPixels = tabPixels;
    }
    public int NbEdgePixels { get; set; } = 0;
    public int[,] TabPixels { get; set; } = null;
}