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

namespace LibCut.Things.Bullet
{
    public class Explosion : StaticBullet
    {
        /// <summary>
        /// The timers
        /// </summary>
        protected TimeSpan timer = new TimeSpan();
        protected TimeSpan fullTime = new TimeSpan(0, 0, 2);

        /// <summary>
        /// Creates an explosion bullet
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_damage"></param>
        /// <param name="_pos"></param>
        /// <param name="_holder"></param>
        /// <param name="_depth"></param>
        public Explosion(Universe.Universe _universe, float _damage, Vector2 _pos, Thing _holder, int _depth)
            : base(_universe, _damage, _pos, _holder, 1, _depth)
        {
            // Load the sprite
            sprite = new Orange.XNA.Sprite(Universe.Content, @"Effects/Explosion");
        }

        /// <summary>
        /// Update the timer
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            if (timer < fullTime)
            {
                timer += _gameTime.ElapsedGameTime;
            }
            else
            {
                Dead = true;
            }
            
            // See if any things are in range
            foreach (Thing thing in Universe.Things.ToArray())
            {
                if (Vector2.Distance(thing.Position, Position) < 400)
                {
                    thing.Collide(this);
                }
            }
        }

        /// <summary>
        /// Make the explosion fade out and get bigger
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_graphicsDevice"></param>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            sprite.tint = Color.White * (1 - ((float)timer.Ticks / (float)fullTime.Ticks));
            sprite.scale = new Vector2(1) + new Vector2((1 - ((float)timer.Ticks / (float)fullTime.Ticks)) * 2);
            base.Draw(_camera, _graphicsDevice);
        }
    }
}
