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

namespace LibCut.Things.Cursor
{
    public class Cursor : Things.Sprite.Sprite
    {
        /// <summary>
        /// The status of the cursor
        /// </summary>
        public string Status
        {
            get
            {
                return sprite.CurrentAnimation;
            }
            set
            {
                sprite.SetCurrentAnimation(value);
            }
        }

        /// <summary>
        /// Creates a new cursor
        /// </summary>
        /// <param name="_universe"></param>
        public Cursor(Universe.Universe _universe, Orange.XNA.Sprite _sprite, int _depth)
            : base(_universe, _sprite, _depth)
        {
            // Create animations for the cursor
            sprite.AddAnimation("Default", 0, 4, 15, true);
            Status = "Default";
        }

        /// <summary>
        /// Update the cursor position
        /// </summary>
        /// <param name="_gameTime"></param>
        /// <param name="_input"></param>
        public override void HandleInput(Microsoft.Xna.Framework.GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);
            sprite.position = new Vector2(_input.Mouse.X, _input.Mouse.Y);
        }

        /// <summary>
        /// Draws the cursor at local position on the screen (don't need to do any transform stuff)
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_graphicsDevice"></param>
        public override void Draw(Camera.Camera _camera, Microsoft.Xna.Framework.Graphics.GraphicsDevice _graphicsDevice)
        {
            //base.Draw(_camera, _graphicsDevice);
            spriteBatch.Begin();
            sprite.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
