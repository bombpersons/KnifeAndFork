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
    public class TextureGen
    {
        // Generates the texture for the primitives to use
        public static void GenerateTexture(GraphicsDevice _graphicsDevice)
        {
            texture = new Texture2D(_graphicsDevice, 1, 1);
            texture.SetData<Color>(new Color[] { Color.White });
        }

        /// <summary>
        /// The texture to use
        /// </summary>
        public static Texture2D texture;
    }
}
