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

namespace CreatureEditor
{
    public class EditorUniverse : LibCut.Universe.Universe
    {
        public EditorUniverse(ContentManager _content, GraphicsDevice _graphicsDevice)
            : base(_content, _graphicsDevice)
        {
            Camera = new LibCut.Camera.DebugCamera(this, graphicsDevice, new Vector2(0));
            LibCut.Things.Thing testPoint = new LibCut.Things.Sprite.Sprite(this, new Orange.XNA.Sprite(content, "TestPoint"), 0);
            LibCut.Things.Thing shapeMaker = new LibCut.Things.DebugItems.ShapeMaker.ShapeMaker(this, 0);
            LibCut.Things.Thing cursor = new LibCut.Things.Cursor.Cursor(this, new Orange.XNA.Sprite(content, @"Cursor/Default", 50, 50, 0, 0), 9999);

        }
    }
}
