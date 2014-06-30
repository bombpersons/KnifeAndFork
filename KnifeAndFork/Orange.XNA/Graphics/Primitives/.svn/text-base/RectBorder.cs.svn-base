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
    public class RectBorder : Orange.XNA.Graphics.Primitives.Rect
    {
        /// <summary>
        /// The shader to use to get the border effect
        /// </summary>
        Effect shader;

        /// <summary>
        /// Set the width of the border. The boarder is a percentage of the size
        /// </summary>
        public float Width
        {
            get
            {
                if (shader != null)
                    return shader.Parameters["Width"].GetValueSingle();
                return 0.0f;
            }
            set
            {
                if (shader != null)
                    shader.Parameters["Width"].SetValue(value);
            }
        }

        /// <summary>
        /// The color of the fill
        /// </summary>
        Color fill = Color.White;
        public Color Fill
        {
            get
            {
                return fill;

            }
            set
            {
                fill = value;
            }
        }

        /// <summary>
        /// The color of the border
        /// </summary>
        public Color Border
        {
            get
            {
                if (shader != null)
                    return new Color(shader.Parameters["Border"].GetValueVector4());
                return Color.White;
            }
            set
            {
                if (shader != null)
                    shader.Parameters["Border"].SetValue(value.ToVector4());
            }
        }

        /// <summary>
        /// The border rectangle
        /// </summary>
        protected Orange.XNA.Graphics.Primitives.Rect border;

        // Constructor
        public RectBorder(Rectangle _rect, int _width, ContentManager _content)
            : base(_rect)
        {
            shader = _content.Load<Effect>("Border");
        }

        /// <summary>
        /// Overriden draw method to draw the other rect
        /// </summary>
        /// <param name="_spriteBatch"></param>
        public override void Draw(SpriteBatch _spriteBatch)
        {
            // Update shader values
            UpdateShader();

            Begin(_spriteBatch);
            base.Draw(_spriteBatch);
            End(_spriteBatch);
        }

        /// <summary>
        /// Updates values in the shader
        /// </summary>
        public void UpdateShader()
        {
            shader.Parameters["Fill"].SetValue(fill.ToVector4());
        }

        /// <summary>
        /// Begin drawing a batch
        /// </summary>
        /// <param name="_spriteBatch"></param>
        public void Begin(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin(0, BlendState.AlphaBlend, null, null, null, shader);
        }

        /// <summary>
        /// End drawing a batch
        /// </summary>
        /// <param name="_spriteBatch"></param>
        public void End(SpriteBatch _spriteBatch)
        {
            _spriteBatch.End();
        }

        /// <summary>
        /// Draws without calling 
        /// </summary>
        /// <param name="_spriteBatch"></param>
        public void DrawWithoutBegin(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }
    }
}
