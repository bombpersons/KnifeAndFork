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

namespace Orange.XNA.Graphics.UI
{
    /// <summary>
    /// When hovered over the sprite will scale small and larger.
    /// </summary>
    public class BouncySpriteButton : SpriteButton
    {
        /// <summary>
        /// The scale the sprite is at when at rest
        /// </summary>
        protected float restScale = 1.0f;
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
        /// The amount to scale down
        /// </summary>
        protected float downScaleLimit;
        public float DownScaleLimit
        {
            get
            {
                return downScaleLimit;
            }
            set
            {
                if (value <= UpScaleLimit)
                    downScaleLimit = value;
            }
        }

        /// <summary>
        /// The upper scale limit
        /// </summary>
        protected float upScaleLimit;
        public float UpScaleLimit
        {
            get
            {
                return upScaleLimit;
            }
            set
            {
                if (value >= downScaleLimit)
                    upScaleLimit = value;
            }
        }

        /// <summary>
        /// Set the up and down scale limits to uniform values
        /// </summary>
        public float Variance
        {
            get
            {
                return (UpScaleLimit - DownScaleLimit);
            }
            set
            {
                upScaleLimit = RestScale + value;
                downScaleLimit = RestScale - value;
            }
        }

        /// <summary>
        /// The speed of the bounces.
        /// </summary>
        protected float speed;
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
        /// The direction that the scaling is going.
        /// </summary>
        bool direction = true;

        /// <summary>
        /// Create the button
        /// </summary>
        /// <param name="_sprite"></param>
        public BouncySpriteButton(Sprite _sprite, float _variance, float _speed)
            : base(_sprite)
        {
            Variance = _variance;
            Speed = _speed;
        }

        /// <summary>
        /// Do the scaling
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            if (Hover)
            {
                if (direction)
                {
                    if (image.scale.X > UpScaleLimit)
                    {
                        direction = !direction;
                    }
                }
                else
                {
                    if (image.scale.X < DownScaleLimit)
                    {
                        direction = !direction;
                    }
                }

                if (direction)
                    image.scale += new Vector2(GetGradient()) * Speed * (float)_gameTime.ElapsedGameTime.TotalSeconds;
                else
                    image.scale -= new Vector2(GetGradient()) * image.scale * Speed * (float)_gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                if (image.scale.X > RestScale)
                {
                    direction = false;
                }
                else if (image.scale.X < RestScale)
                {
                    direction = true;
                }

                if (image.scale.X > RestScale + 0.01f || image.scale.X < RestScale - 0.01f)
                {
                    // Scale with the gradient as X^2
                    if (direction)
                        image.scale += new Vector2(GetGradient()) * Speed * (float)_gameTime.ElapsedGameTime.TotalSeconds;
                    else
                        image.scale -= new Vector2(GetGradient()) * Speed * (float)_gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    direction = true;
                }
            }
        }

        /// <summary>
        /// Modify the drawing code so that we can scale around the actual center of the sprite.
        /// </summary>
        /// <param name="_spriteBatch"></param>
        public override void Draw(SpriteBatch _spriteBatch)
        {
            // Save the old position so that we can restore it later
            Vector2 savedPos = image.position;
            Vector2 newCenter = image.size / 2;

            image.position += newCenter;
            image.center = newCenter;

            base.Draw(_spriteBatch);

            image.position = savedPos;
            image.center = new Vector2(0.0f, 0.0f);
        }

        /// <summary>
        /// Gets the gradient to increase the scale.
        /// </summary>
        /// <returns></returns>
        protected float GetGradient()
        {
            /// Using the standard distribution probability density function for the gradient to increase the scale, since it looks pretty =)
            /// http://www.wolframalpha.com/input/?i=standard+distribution
            /// Y = (E^((X^ 2) / 2)) / SQRT(2*PI)
            ///
            /// We need to modify it slightly however. To make the scaling even between different variance, we need to scale the graph
            /// Y = (E^((((3 * X) / V) / 2) / SQRT(2*PI)
            /// Where V is the range between upper and lower limit scales.
            /// We divide by three because we want the limits of the function to be at 1 and -1 so that we can scale it easily.
            /// The Y part of the graph at around 3 on the X axis is very small. Any bigger than this and when the button is at the edge of the
            /// upper and lower limits it will be very slow.

            // Calculate the 'X' part
            // We need to adjust to get the middle of the scaling as 0
            float middle = DownScaleLimit + (Variance / 2);
            float X = image.scale.X - middle;

            // Now use the equation
            return (float)((Math.Pow(Math.E, -1 * Math.Pow(((3 * (double)X) / (double)Variance), 2) / 2)) / squareRootOf2TimesPi);
        }

        /// <summary>
        /// Pre calc this value
        /// </summary>
        static float squareRootOf2TimesPi = (float)Math.Sqrt(2 * Math.PI);
    }
}
