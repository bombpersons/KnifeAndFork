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

namespace Orange.XNA.Graphics.Text
{
    public class SimpleText : PhysicsObject
    {
        /// <summary>
        /// The text to show
        /// </summary>
        protected string text;
        public virtual string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        /// <summary>
        /// The color of the text
        /// </summary>
        protected Color tint = Color.White;
        public virtual Color Tint
        {
            get
            {
                return tint;
            }
            set
            {
                tint = value;
            }
        }

        // How transparent the text is
        float alpha = 1.0f;
        public virtual float Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                tint = Color.White * value;
                alpha = value;
            }
        }

        /// <summary>
        /// The text
        /// </summary>
        public SimpleText(string _text)
        {
            Text = _text;
        }

        /// <summary>
        /// Draw the text.
        /// </summary>
        /// <param name="_spriteBatch"></param>
        public virtual void Draw(SpriteBatch _spriteBatch, SpriteFont _font)
        {
            _spriteBatch.DrawString(_font, Text, position, tint, rotation, _font.MeasureString(Text) / 2, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Update the text
        /// </summary>
        /// <param name="_gameTime"></param>
        public virtual void Update(GameTime _gameTime)
        {
        }
    }
}
