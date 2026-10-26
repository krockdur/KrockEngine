using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyMGPixelEngine;


internal class Editor8x8Scene : Scene
{

    Sprite back_draw_zone_sprite;
    public override void Initialize()
    {
        back_draw_zone_sprite = new Sprite(@"pictures/8x8_interface", 0, 0);
    }

    public override void LoadContent()
    {

    }

    public override void Update(GameTime gameTime, SceneManager sceneManager)
    {
    }

    public override void Draw(ref FrameBuffer framebuffer)
    {
        back_draw_zone_sprite.AddToFramebuffer(ref framebuffer);
    }
}