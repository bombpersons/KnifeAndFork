using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace KnifeAndFork.MainMenu
{
    public class MenuUniverse : LibCut.Universe.Universe
    {
        public MenuUniverse(ContentManager _content, GraphicsDevice _graphicsDevice)
            : base(_content, _graphicsDevice)
        {
        }
    }
}
