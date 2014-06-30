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

namespace LibCut.Things.PauseScreen
{
    public class MissionBrief : Pause
    {
        /// <summary>
        /// A timer to show the mission screen for
        /// </summary>
        protected TimeSpan timer = new TimeSpan();
        protected TimeSpan fadeTimer = new TimeSpan();

        /// <summary>
        /// How long to show it for
        /// </summary>
        protected TimeSpan showTime = new TimeSpan(0, 0, 3);
        protected TimeSpan fadeTime = new TimeSpan(0, 0, 4);

        /// <summary>
        /// Creates a new mission brief screen
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_sprite"></param>
        public MissionBrief(Universe.Universe _universe, Orange.XNA.Sprite _sprite)
            : base(_universe, _sprite)
        {
            sprite.visible = true;
            Universe.Locked = true;
        }

        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
        }

        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);

            if (Universe.Locked)
            {
                if (timer < showTime)
                {
                    timer += _gameTime.ElapsedGameTime;
                }
                else if (fadeTimer < fadeTime)
                {
                    fadeTimer += _gameTime.ElapsedGameTime;
                }

                if (fadeTimer > fadeTime)
                {
                    Dead = true;
                }
            }
            else
            {
                Dead = true;
            }
        }

        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            sprite.tint = Color.White * (1 - ((float)fadeTimer.Ticks / (float)fadeTime.Ticks));
            base.Draw(_camera, _graphicsDevice);
        }
    }
}
