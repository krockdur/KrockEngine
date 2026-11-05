using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyMGPixelEngine;


internal class EditorScene : Scene
{

    Sprite back_draw_zone_sprite;

    private int _pixelSize;     // largeur/hauteur en pixel d'une case dans un carré de 128 pixels 

    private Button[] _buttons;
    Textbox testTextbox;

    private List<Pixel> _pixelsList = new List<Pixel>();
    //private int[,] tabPixelsToSave;
    private const int EDITOR_EDGE_PIXELS = 128;
    private const int EDITOR_COLOR_SURFACE = 1;
    private int _nbPixelsXY; // Nombre de pixels de l'image en X et Y
    public override void Initialize(object payload = null)
    {
        // Traitement du payload
        EditorPayload editorPayload = (EditorPayload)payload;
        if (editorPayload.File != null && editorPayload.File.Length > 0)
        {
            // Cas 1 - Ouverture d'un fichier existant
            string path = "pictures/" + editorPayload.File;
            if (File.Exists(path))
            {
                LoadFile(path);
            }
        }
        else
        {
            // Cas 2 - nouvelle image
            InitNewImage(editorPayload);

        }

        back_draw_zone_sprite = new Sprite(@"pictures/8x8_interface", 0, 0);



        _buttons = new Button[]
        {
            // Couleurs
            new Button(150, 0, 10, 10, "Black", ColorList.White, ColorList.Black, ColorList.Red, ColorList.Red, ColorList.Black, ColorList.Red),
            new Button(150, 10, 10, 10, "Brown", ColorList.White, ColorList.Brown, ColorList.Red, ColorList.Red, ColorList.Brown, ColorList.Red),
            new Button(150, 20, 10, 10, "Red", ColorList.White, ColorList.Red, ColorList.Red, ColorList.Red, ColorList.Red, ColorList.Red),
            new Button(150, 30, 10, 10, "Blue", ColorList.White, ColorList.Blue, ColorList.Red, ColorList.Red, ColorList.Blue, ColorList.Red),
            new Button(150, 40, 10, 10, "MidnightBlue", ColorList.White, ColorList.MidnightBlue, ColorList.Red, ColorList.Red, ColorList.MidnightBlue, ColorList.Red),
            new Button(150, 50, 10, 10, "TaupeGray", ColorList.White, ColorList.TaupeGray, ColorList.Red, ColorList.Red, ColorList.TaupeGray, ColorList.Red),
            new Button(150, 60, 10, 10, "Orange", ColorList.White, ColorList.Orange, ColorList.Red, ColorList.Red, ColorList.Orange, ColorList.Red),
            new Button(150, 70, 10, 10, "DarkGray", ColorList.White, ColorList.DarkGray, ColorList.Red, ColorList.Red, ColorList.DarkGray, ColorList.Red),

            new Button(160, 0, 10, 10, "Purple", ColorList.White, ColorList.Purple, ColorList.Red, ColorList.Red, ColorList.Purple, ColorList.Red),
            new Button(160, 10, 10, 10, "LightGray", ColorList.White, ColorList.LightGray, ColorList.Red, ColorList.Red, ColorList.LightGray, ColorList.Red),
            new Button(160, 20, 10, 10, "Yellow", ColorList.White, ColorList.Yellow, ColorList.Red, ColorList.Red, ColorList.Yellow, ColorList.Red),
            new Button(160, 30, 10, 10, "Pink", ColorList.White, ColorList.Pink, ColorList.Red, ColorList.Red, ColorList.Pink, ColorList.Red),
            new Button(160, 40, 10, 10, "ForestGreen", ColorList.White, ColorList.ForestGreen, ColorList.Red, ColorList.Red, ColorList.ForestGreen, ColorList.Red),
            new Button(160, 50, 10, 10, "White", ColorList.White, ColorList.White, ColorList.Red, ColorList.Red, ColorList.White, ColorList.Red),
            new Button(160, 60, 10, 10, "Green", ColorList.White, ColorList.Green, ColorList.Red, ColorList.Red, ColorList.Green, ColorList.Red),
            new Button(160, 70, 10, 10, "Peach", ColorList.White, ColorList.Peach, ColorList.Red, ColorList.Red, ColorList.Peach, ColorList.Red),

            new Button(10, 140, 40, 15, "SAVE", ColorList.White, ColorList.LightGray, ColorList.Black, ColorList.Red, ColorList.DarkGray, ColorList.Black)

        };

        testTextbox = new Textbox(100, 140, 50, 15, ColorList.Blue, ColorList.Yellow, ColorList.Black, ColorList.Red, ColorList.White);
    }

    private void InitNewImage(EditorPayload editorPayload)
    {
        switch (editorPayload.Type)
        {
            case "8X8":
                _pixelSize = 16;
                break;
            case "16X16":
                _pixelSize = 8;
                break;
            case "32X32":
                _pixelSize = 4;
                break;
            case "64X64":
                _pixelSize = 2;
                break;
            case "128X128":
                _pixelSize = 1;
                break;
            default:
                _pixelSize = 16;
                break;
        }

        // calcul du nombres de pixels de l'image
        _nbPixelsXY = EDITOR_EDGE_PIXELS / _pixelSize;
        // initialisation de _pixelsList
        // Nouvelle image : on créé une liste de pixels avec la couleur de fond noire.
        for (int j = 0; j < _nbPixelsXY; j++)
        {
            for (int i = 0; i < _nbPixelsXY; i++)
            {
                // dans l'éditeur 1 pixel de l'image = x pixels d'affichage suivant le format choisi
                // donc pour chaque pixel de l'image, il faut créer X pixels pour l'affichage (ex: 1 pixel de l'image = 8 pixels pour une image 8x8)
                for (int thispix_y = 0; thispix_y < _pixelSize; thispix_y++)
                {
                    for (int thispix_x = 0; thispix_x < _pixelSize; thispix_x++)
                    {
                        _pixelsList.Add(new Pixel(i * _pixelSize + thispix_x, j * _pixelSize + thispix_y, ColorList.Black));
                    }
                }

            }
        }
    }
    private void LoadFile(string file)
    {
        using (StreamReader sr = new StreamReader(file))
        {
            // retrouver le type de l'image
            int width = int.Parse(sr.ReadLine());
            int height = int.Parse(sr.ReadLine());
            
            if (width == 8 && height == 8)
            {
                _pixelSize = 16;
            }
            else if (width == 16 && height == 16)
            {
                _pixelSize = 8;
            }
            else if (width == 32 && height == 32)
            {
                _pixelSize = 4;
            }
            else if (width == 64 && height == 64)
            {
                _pixelSize = 2;
            }
            else if (width == 128 && height == 128)
            {
                _pixelSize = 1;
            }
            else
            {
                InitNewImage(new EditorPayload("8X8"));
                return;
            }

            // obtenir le tableau de pixels du fichier
            int[,] tabPixels = new int[height, width];
            
            int fj = 0;
            while (sr.ReadLine() != null)
            {
                string line = sr.ReadLine();
                string[] tabLine = line.Split(' ');
                for (int fi = 0; fi < tabLine.Length; fi++)
                {
                    tabPixels[fj, fi] = int.Parse(tabLine[fi]);
                }
                fj++;
            }

            // 
            // MAJ ombres de pixels de l'image
            _nbPixelsXY = width;
            // initialisation de _pixelsList
            // 
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    // dans l'éditeur 1 pixel de l'image = x pixels d'affichage suivant le format choisi
                    // donc pour chaque pixel de l'image, il faut créer X pixels pour l'affichage (ex: 1 pixel de l'image = 8 pixels pour une image 8x8)
                    for (int thispix_y = 0; thispix_y < _pixelSize; thispix_y++)
                    {
                        for (int thispix_x = 0; thispix_x < _pixelSize; thispix_x++)
                        {
                            _pixelsList.Add(new Pixel(i * _pixelSize + thispix_x, j * _pixelSize + thispix_y, tabPixels[j, i]));
                        }
                    }

                }
            }
        }
    }
    public override void LoadContent()
    {
        // initial load content
        //
    }

    int clickPositonX = 0;
    int clickPositonY = 0;

    private int _colorPicked = 0;
    private MouseState _previousMouseState;
    public override void Update(GameTime gameTime, SceneManager sceneManager)
    {
        MouseState mouseState = Mouse.GetState();
        foreach (var button in _buttons)
        {
            button.Update(mouseState, _previousMouseState);
        }

        testTextbox.Update(mouseState, _previousMouseState);

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            clickPositonX = mouseState.Position.X / Config.SCALE;
            clickPositonY = mouseState.Position.Y / Config.SCALE;
            int caseX = clickPositonX / _pixelSize;
            int caseY = clickPositonY / _pixelSize;

            if (clickPositonX >= 0 && clickPositonX <= 128 && clickPositonY >= 0 && clickPositonY <= 128)
            {

                for (int i = 0; i < _pixelSize; i++)
                {
                    for (int j = 0; j < _pixelSize; j++)
                    {
                        _pixelsList.Add(new Pixel(caseX * _pixelSize + j, caseY * _pixelSize + i, _colorPicked));
                    }
                }

                // renseigner le tableau à sauvegarder dans un fichier
                //tabPixelsToSave[caseY, caseX] = _colorPicked;

            }

        }

        foreach (var button in _buttons)
        {
            if (button.IsPressed)
            {
                switch (button.Text)
                {
                    case "Black":
                        _colorPicked = ColorList.Black;
                        break;
                    case "Brown":
                        _colorPicked = ColorList.Brown;
                        break;
                    case "Red":
                        _colorPicked = ColorList.Red;
                        break;
                    case "Blue":
                        _colorPicked = ColorList.Blue;
                        break;
                    case "MidnightBlue":
                        _colorPicked = ColorList.MidnightBlue;
                        break;
                    case "TaupeGray":
                        _colorPicked = ColorList.TaupeGray;
                        break;
                    case "Orange":
                        _colorPicked = ColorList.Orange;
                        break;
                    case "DarkGray":
                        _colorPicked = ColorList.DarkGray;
                        break;
                    case "Purple":
                        _colorPicked = ColorList.Purple;
                        break;
                    case "LightGray":
                        _colorPicked = ColorList.LightGray;
                        break;
                    case "Yellow":
                        _colorPicked = ColorList.Yellow;
                        break;
                    case "Pink":
                        _colorPicked = ColorList.Pink;
                        break;
                    case "ForestGreen":
                        _colorPicked = ColorList.ForestGreen;
                        break;
                    case "White":
                        _colorPicked = ColorList.White;
                        break;
                    case "Green":
                        _colorPicked = ColorList.Green;
                        break;
                    case "Peach":
                        _colorPicked = ColorList.Peach;
                        break;
                    case "SAVE":
                        PrepareSave();
                        break;
                }
            }
        }
        _previousMouseState = mouseState;
    }
    public override void Draw(ref FrameBuffer framebuffer)
    {

        back_draw_zone_sprite.AddToFramebuffer(ref framebuffer);

        foreach (Pixel pixel in _pixelsList)
        {
            framebuffer.SetPixel(pixel.X, pixel.Y, pixel.Color);

        }

        foreach (var button in _buttons)
        {
            button.AddToFramebuffer(ref framebuffer);
        }

        testTextbox.AddToFramebuffer(ref framebuffer);


    }

    private void PrepareSave()
    {
        if (testTextbox.Text != null && testTextbox.Text.Length > 0)
        {
            Save(EDITOR_EDGE_PIXELS / _pixelSize, EDITOR_EDGE_PIXELS / _pixelSize, tabPixelsToSave, testTextbox.Text);
        }
    }

    private void Save(int width, int height, int[,] tabPixel, string filename)
    {
        string filePath = "pictures/" + filename;

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Ligne 1 : Nombre de colonnes
                writer.WriteLine(width);

                // Ligne 2 : Nombre de lignes
                writer.WriteLine(height);

                // Lignes suivantes : Chaque ligne du tableau
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        writer.Write(tabPixel[i, j]);

                        // Ajoute un espace entre les colonnes, sauf après la dernière
                        if (j < width - 1)
                        {
                            writer.Write(" ");
                        }
                    }
                    // Retour à la ligne après chaque ligne du tableau
                    writer.WriteLine();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors de l'écriture : " + ex.Message);
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