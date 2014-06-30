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

namespace LibCut.Things.Effects
{
    public class Effect : Sprite.Sprite
    {
        /// <summary>
        /// The frame to kill the effect
        /// </summary>
        protected int dieFrame;

        /// <summary>
        /// Creates a new generic effect
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_sprite"></param>
        /// <param name="_pos"></param>
        /// <param name="_dieFrame"></param>
        /// <param name="_depth"></param>
        public Effect(Universe.Universe _universe, Orange.XNA.Sprite _sprite, Vector2 _pos, int _dieFrame, int _depth)
            : base(_universe, _sprite, _depth)
        {
            // Set the position
            Position = _pos;

            // Set the die frame
            dieFrame = _dieFrame;
        }

        /// <summary>
        /// Kill the effect if it's time is up.
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
            if (sprite.frame.frame >= dieFrame)
            {
                Dead = true;
            }
        }
    }
}
