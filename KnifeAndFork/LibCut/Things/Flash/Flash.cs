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

namespace LibCut.Things.Flash
{
    public class Flash : Things.Sprite.Sprite
    {
        /// <summary>
        /// The alpha of the sprite
        /// </summary>
        protected float alpha = 1.0f;
        public float Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = value;
                sprite.tint = Color.White * alpha;
            }
        }

        /// <summary>
        /// The timer
        /// </summary>
        protected TimeSpan timer;
        public TimeSpan Timer
        {
            get
            {
                return timer;
            }
            set
            {
                timer = value;
            }
        }

        /// <summary>
        /// Creates a new flash
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_sprite"></param>
        /// <param name="_position"></param>
        /// <param name="_depth"></param>
        public Flash(Universe.Universe _universe, Orange.XNA.Sprite _sprite, Vector2 _position, TimeSpan _time, int _depth)
            : base(_universe, _sprite, _depth)
        {
            Position = _position;
            Timer = _time;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Tick down the timer
            Timer -= _gameTime.ElapsedGameTime;

            // Change the alpha
            Alpha = (float)Math.Sin(Timer.Ticks / 1000000.0f);

            // Die if the timer is dead
            if (Timer.Ticks < 0)
                Dead = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_graphicsDevice"></param>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            //base.Draw(_camera, _graphicsDevice);
            spriteBatch.Begin();
            sprite.position = Position;
            sprite.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
