using System;
using System.Collections.Generic;
using System.Text;

namespace MyPixelEngine
{
    internal class ScreenManager
    {
        void Initialize()
        {
        }

        void Update()
        {
        }

        void Draw()
        {
        }

        void Run()
        {
            Initialize();
            while (true)
            {
                Update();
                Draw();
            }
        }
}
