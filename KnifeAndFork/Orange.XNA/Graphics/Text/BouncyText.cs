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
    public class BouncyText : SimpleText
    {
        /// <summary>
        ///  Override the text property to bounce the text when it changes.
        /// </summary>
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                Bounce = true;
                base.Text = value;
                if (value == "")
                {
                    Fade = true;
                }
                else
                {
                    Fade = false;
                }
            }
        }

        /// <summary>
        /// Makes the text fade out. Automatically set when the text is set to ""
        /// </summary>
        bool fade;
        public bool Fade
        {
            get
            {
                return fade;
            }
            set
            {
                fade = value;
            }
        }

        /// <summary>
        /// Makes the text bounce!
        /// </summary>
        public virtual bool Bounce
        {
            set
            {
                if (value == true)
                {
                    X = -1.0f;
                }
                else
                {
                    X = 5.0f;
                }
            }
        }

        /// <summary>
        /// The rest scale
        /// </summary>
        float restScale = 1.0f;
        public float RestScale
        {
            get
            {
                return restScale;
            }
            set
            {
                restScale = value;
            }
        }

        /// <summary>
        /// The maximum scale
        /// </summary>
        float maxScale = 1.5f;
        public float MaxScale
        {
            get
            {
                return maxScale;
            }
            set
            {
                if (value > RestScale)
                    maxScale = value;
            }
        }

        /// <summary>
        /// The speed of the bounce
        /// </summary>
        float speed = 10.0f;
        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

        /// <summary>
        /// The speed at which the text fades
        /// </summary>
        float fadeSpeed = 0.05f;
        public float FadeSpeed
        {
            get
            {
                return fadeSpeed;
            }
            set
            {
                fadeSpeed = value;
            }
        }

        /// <summary>
        /// The x coordinate of the graph
        /// </summary>
        protected float X = 1.0f;

        /// <summary>
        /// Initialize the text
        /// </summary>
        /// <param name="_text"></param>
        public BouncyText(string _text)
            : base(_text)
        {
        }

        /// <summary>
        /// Scale the text up when bounce is true
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            if (X < 5.0f)
            {
                X += Speed * (float)_gameTime.ElapsedGameTime.TotalSeconds;
            }

            scale = new Vector2(RestScale + GetGradient()*MaxScale);

            if (Fade && Alpha > 0.0f)
            {
                Alpha -= FadeSpeed;
                Console.WriteLine(Alpha);
            }
            else if (!Fade && Alpha < 1.0f)
            {
                Alpha += FadeSpeed;
            }
        }

        /// <summary>
        /// Calculates the gradient to move the scale by
        /// </summary>
        /// <returns></returns>
        float GetGradient()
        {
            return (float)((Math.Abs(X + 1) + (X + 1)) * Math.Pow(Math.E, -1*(X + 1)) * (Math.E / 2));
        }
    }
}
