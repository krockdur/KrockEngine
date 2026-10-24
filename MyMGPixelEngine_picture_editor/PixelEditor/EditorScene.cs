using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MyMGPixelEngine;

internal class EditorScene : Scene
{
        Sprite mSprite;

    public override void Initialize()
    {

        mSprite = new Sprite(@"pictures/test", 50, 100);


    }

    public override void LoadContent()
    {

    }

    float posX = 0f;
    float posY = 0f;
    public override void Update(GameTime gameTime, SceneManager sceneManager)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Q))
        {
            posX -= 0.5f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            posX += 0.5f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Z))
        {
            posY -= 0.5f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            posY += 0.5f;
        }

        mSprite.SetPosition((int)posX, (int)posY);
    }

    public override void Draw(ref FrameBuffer framebuffer)
    {

        mSprite.AddToFramebuffer(ref framebuffer);


    }
}