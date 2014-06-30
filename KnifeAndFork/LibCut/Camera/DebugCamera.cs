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

namespace LibCut.Camera
{
    public class DebugCamera : Camera
    {
        /// <summary>
        /// The mouse states
        /// </summary>
        MouseState oldState;
        MouseState state;

        /// <summary>
        /// Creates a new debug camera
        /// </summary>
        /// <param name="_graphicsDevice"></param>
        /// <param name="_pos"></param>
        public DebugCamera(Universe.Universe _universe, GraphicsDevice _graphicsDevice, Vector2 _pos)
            : base(_universe, _graphicsDevice, _pos)
        {
        }

        /// <summary>
        /// Handle input
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);

            oldState = state;
            state = Mouse.GetState();

            // Move the camera if the middle mouse button is pressed
            if (state.MiddleButton == ButtonState.Pressed && oldState.MiddleButton == ButtonState.Pressed)
            {
                Position -= new Vector2((new Vector2(state.X, state.Y) - new Vector2(oldState.X, oldState.Y)).X * position.Right.X / Scale.X,
                                        (new Vector2(state.X, state.Y) - new Vector2(oldState.X, oldState.Y)).Y * position.Up.Y / Scale.Y);
            }

            Scale += new Vector2(state.ScrollWheelValue - oldState.ScrollWheelValue)*0.001f;
        }
    }
}
