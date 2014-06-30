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
    public class Pause : Sprite.StaticSprite
    {
        /// <summary>
        /// Override dead to unlock the universe when we are done.
        /// </summary>
        public override bool Dead
        {
            get
            {
                return base.Dead;
            }
            set
            {
                base.Dead = value;
                if (value)
                {
                    Universe.Locked = false;
                }
            }
        }

        /// <summary>
        /// Create a new pause screen
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_sprite"></param>
        public Pause(Universe.Universe _universe, Orange.XNA.Sprite _sprite)
            : base(_universe, _sprite, 10000000)
        {
            // Lock the universe
            //Universe.Locked = true;

            // Set the center of the sprite
            sprite.center = Vector2.Zero;
            sprite.visible = false;
        }

        /// <summary>
        /// Unpause when either players press start
        /// </summary>
        /// <param name="_gameTime"></param>
        /// <param name="_input"></param>
        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);

            if (_input.ClickedPadButton(Buttons.Start, 0) || _input.ClickedPadButton(Buttons.Start, 1))
            {
                Universe.Locked = !Universe.Locked;
                sprite.visible = Universe.Locked;
            }
        }
    }
}
