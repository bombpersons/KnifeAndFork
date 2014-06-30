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

namespace Orange.XNA.Graphics.Primitives
{
    public class Rect : Sprite
    {
        /// <summary>
        /// Constructs the rectangle
        /// </summary>
        public Rect(Rectangle _rect)
        {
            // Set the size of the rectangle
            size = new Vector2(1, 1);
            scale = new Vector2(_rect.Width, _rect.Height);
            position = new Vector2(_rect.X, _rect.Y);

            // If the texture isn't loaded, then load it.
            if (texture == null)
            {
                texture = TextureGen.texture;
            }

            frame = new Frame();

            frame.width = (uint)texture.Width;
            frame.height = (uint)texture.Height;
        }
    }
}
