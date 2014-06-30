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

namespace LibCut.Things.TextBox
{
    public class TextBox : Thing
    {
        /// <summary>
        ///  Make sure the speech bubble dies with us.
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
                    speechBubble.Dead = true;
                }
            }
        }

        /// <summary>
        /// Set the position of the speech bubble as well
        /// </summary>
        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                SpeechBubble.Position = value;
            }
        }

        /// <summary>
        /// The speech bubble to draw underneath it
        /// </summary>
        protected SpeechBubble speechBubble;
        public SpeechBubble SpeechBubble
        {
            get
            {
                return speechBubble;
            }
            set
            {
                speechBubble = value;
            }
        }

        /// <summary>
        /// Creates a new text box
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_pos"></param>
        /// <param name="_depth"></param>
        public TextBox(Universe.Universe _universe, Vector2 _size, float _speed, Vector2 _pos, SpriteFont _font, int _depth)
            : base(_universe, _depth)
        {
            // Create the speechbubble
            SpeechBubble = new SpeechBubble(Universe, _pos, _size / 2, _speed, _font, 0.4f);

            Position = _pos;
        }

        /// <summary>
        /// Update the text box
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
        }

        /// <summary>
        /// Take input for the textbox
        /// </summary>
        /// <param name="_gameTime"></param>
        /// <param name="_input"></param>
        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);

            // If player one presses B then continue
            if (_input.ClickedPadButton(Buttons.B, 0) || _input.ClickedPadButton(Buttons.B, 1))
            {
                SpeechBubble.TheTextBox.Paused = false;
            }
            
            // Speed up the text when the button is held down.
            if (_input.GetGamePad(0).Buttons.B == ButtonState.Pressed || _input.GetGamePad(1).Buttons.B == ButtonState.Pressed)
            {
                SpeechBubble.TheTextBox.SpeedUp = true;
            }
            else
            {
                SpeechBubble.TheTextBox.SpeedUp = false;
            }
        }

        /// <summary>
        /// Draw the text
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_graphicsDevice"></param>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            base.Draw(_camera, _graphicsDevice);
        }
    }
}
