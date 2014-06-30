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

namespace RPGText
{
    public class TextBoxDrawing : RPGText
    {
        /// <summary>
        /// The color of the text
        /// </summary>
        protected Color color = Color.Black;
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        /// <summary>
        /// The scale of the text
        /// </summary>
        protected Vector2 scale = new Vector2(1, 1);
        public Vector2 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }

        /// <summary>
        /// The position of the text.
        /// </summary>
        protected Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        /// <summary>
        /// The center to draw at
        /// </summary>
        protected Vector2 center;
        public Vector2 Center
        {
            get
            {
                return center;
            }
            set
            {
                center = value;
            }
        }

        /// <summary>
        /// The rotation of the text box.
        /// </summary>
        protected float rotation;
        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }

        /// <summary>
        /// The font itself
        /// </summary>
        protected SpriteFont font;
        public SpriteFont Font
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
                CalcCharSize();
            }
        }

        /// <summary>
        /// The size of the box in pixels
        /// </summary>
        protected Vector2 size;
        public Vector2 Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                CalcCharSize();
            }
        }

        /// <summary>
        /// The size of one character
        /// </summary>
        protected Vector2 textSize;
        public Vector2 TextSize
        {
            get
            {
                return textSize;
            }
        }

        /// <summary>
        /// The spacing of the characters
        /// </summary>
        protected Vector2 spacing;
        public Vector2 Spacing
        {
            get
            {
                return spacing;
            }
            set
            {
                spacing = value;
                CalcCharSize();
            }
        }

        /// <summary>
        /// Speeds up the text
        /// </summary>
        protected bool speedUp;
        public bool SpeedUp
        {
            get
            {
                return speedUp;
            }
            set
            {
                if (value && !speedUp)
                {
                    speed *= 3;
                }
                else if (!value && speedUp)
                {
                    speed /= 3;
                }
                speedUp = value;
            }
        }

        /// <summary>
        /// Create a new text box.
        /// </summary>
        /// <param name="_size"></param>
        /// <param name="_speed"></param>
        /// <param name="_font"></param>
        public TextBoxDrawing(Vector2 _size, float _speed, SpriteFont _font)
            : base(0, _speed)
        {
            // Load the font
            Font = _font;

            // Set the size
            Size = _size;

            // Set the center
            Center = Size / 2;
        }

        /// <summary>
        /// Calcs the size of the text box in characters
        /// </summary>
        public void CalcCharSize()
        {
            // Don't try this if we don't have a font
            if (font != null)
            {
                textSize = font.MeasureString("a");
                charWidth = (int)((size.X + spacing.X) / textSize.X);
                charHeight = (int)((size.Y + spacing.Y) / textSize.Y);

                // Rewordwrap based on these settings
                FormatString();
            }
        }

        /// <summary>
        /// Draw the text
        /// </summary>
        /// <param name="_spriteBatch"></param>
        public void Draw(SpriteBatch _spriteBatch)
        {
            if (PrintLines != null)
            {
                _spriteBatch.Begin();
                for (int i = 0; i < PrintLines.Count; i++)
                {
                    _spriteBatch.DrawString(Font, PrintLines[i], Position + new Vector2(0, i * (textSize.Y + spacing.Y) * scale.Y), color, rotation, center, scale, SpriteEffects.None, 0);
                }
                _spriteBatch.End();
            }
        }
    }
}
