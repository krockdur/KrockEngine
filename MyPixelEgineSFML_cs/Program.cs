using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Security.Principal;
using System.Numerics;

namespace window;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("Press ESC key to close window");
        var window = new SimpleWindow();
        window.Run();

        Console.WriteLine("All done");
    }
}

internal class SimpleWindow
{
    Texture mainTexture;
    const int gameWidth = 320;
    const int gameHeight = 200;
    const int gameScale = 5;
    byte[] frameBuffer = new byte[gameWidth * gameHeight * 4]; // un pixel c'est 4 octets RGBA
    
    
    public void Run()
    {
        var mode = new VideoMode((gameWidth * gameScale, gameHeight * gameScale));
        var window = new RenderWindow(mode, "SFML works!");

        mainTexture = new Texture(new Vector2u(gameWidth, gameHeight));
        Sprite mainSprite = new Sprite(mainTexture);
        mainSprite.Scale = new Vector2f(gameScale, gameScale);

        Array.Fill(frameBuffer, (byte)0xFF);

        // pixel 0
        putPixel(0, 0, 0xFF, 0x00, 0x00, 0xFF); // red pixel at (0, 0)

        // pixel 319
        putPixel(319, 0, 0xFF, 0x00, 0x00, 0xFF); // red pixel at (319, 0)


        frameBuffer[319 * 4 + 3] = 0xFF;

        window.KeyPressed += Window_KeyPressed;
        window.Closed += (_, _) => window.Close();

        var circle = new CircleShape(10f)
        {
            FillColor = Color.Blue
        };

        // Start the game loop
        //Time elapsedTIme = Time.Zero;
        while (window.IsOpen)
        {
            // Process events
            window.DispatchEvents();

            float direction_x = 0f, direction_y = 0f;
            float speed = 0.1f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                direction_x = 1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                direction_x = -1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                direction_y = -1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                direction_y = 1f;
            }

            //Console.WriteLine("Mouse posx : " + SFML.Window.Mouse.GetPosition(window).X.ToString() + " " + "Mouse posy : " + SFML.Window.Mouse.GetPosition(window).Y.ToString());

            circle.Position += new Vector2f(direction_x * speed, direction_y * speed);


            Clock clock = new Clock();


            window.Clear(Color.White);

            window.Draw(circle);


            mainTexture.Update(frameBuffer);

            window.Draw(mainSprite);

            // Finally, display the rendered frame on screen
            window.Display();



            Time elapsedTIme = clock.ElapsedTime;
            Console.WriteLine("Elapsed time : " + elapsedTIme.AsMicroseconds().ToString());

        }
    }

    void putPixel(int x, int y, byte r, byte g, byte b, byte a)
    {
        if (x < 0 || x >= gameWidth || y < 0 || y >= gameHeight)
            return;
        int index = (y * gameWidth + x) * 4;
        frameBuffer[index + 0] = r;
        frameBuffer[index + 1] = g;
        frameBuffer[index + 2] = b;
        frameBuffer[index + 3] = a;
    }

    /// <summary>
    /// Function called when a key is pressed
    /// </summary>
    private void Window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
    {
        var window = (Window)sender;
        if (e.Code == Keyboard.Key.Escape)
        {
            window.Close();
        }
    }
}