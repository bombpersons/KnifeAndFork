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

namespace Orange.XNA.Input
{
    public class Input
    {
        /// <summary>
        /// The keyboard states
        /// </summary>
        KeyboardState oldKeyboard;
        KeyboardState newKeyboard;
        public KeyboardState Keyboard
        {
            get
            {
                return newKeyboard;
            }
        }

        /// <summary>
        /// Whether or not a button was clicked
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public bool ClickedButton(Keys _key)
        {
            if (oldKeyboard.IsKeyUp(_key) && newKeyboard.IsKeyDown(_key))
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Whether or not the button was released
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public bool ReleasedButton(Keys _key)
        {
            if (oldKeyboard.IsKeyDown(_key) && newKeyboard.IsKeyUp(_key))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// The mouse state
        /// </summary>
        MouseState oldMouse;
        MouseState newMouse;
        public MouseState Mouse
        {
            get
            {
                return newMouse;
            }
        }

        /// <summary>
        /// Whether or not the left mouse button was clicked
        /// </summary>
        public bool ClickedLeftButton
        {
            get
            {
                if (oldMouse.LeftButton == ButtonState.Released && newMouse.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Whether or not the mouse was just released
        /// </summary>
        public bool ReleasedRightButton
        {
            get
            {
                if (oldMouse.RightButton == ButtonState.Pressed && newMouse.RightButton == ButtonState.Released)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Whether or not the left mouse button was clicked
        /// </summary>
        public bool ClickedRightButton
        {
            get
            {
                if (oldMouse.RightButton == ButtonState.Released && newMouse.RightButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Whether or not the mouse was just released
        /// </summary>
        public bool ReleasedLeftButton
        {
            get
            {
                if (oldMouse.LeftButton == ButtonState.Pressed && newMouse.LeftButton == ButtonState.Released)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// The XBOX gamepad states
        /// </summary>
        GamePadState[] oldGamePad = new GamePadState[4];
        GamePadState[] newGamePad = new GamePadState[4];
        public GamePadState GetGamePad(int _player)
        {
            return newGamePad[_player];
        }

        /// <summary>
        /// Whether or not a button was clicked
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public bool ClickedPadButton(Buttons _button, int _player)
        {
            if (oldGamePad[_player].IsButtonUp(_button) && newGamePad[_player].IsButtonDown(_button))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Whether or not a button was released
        /// </summary>
        /// <param name="_button"></param>
        /// <returns></returns>
        public bool ReleasedPadButton(Buttons _button, int _player)
        {
            if (oldGamePad[_player].IsButtonDown(_button) && newGamePad[_player].IsButtonUp(_button))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Input()
        {
        }

        /// <summary>
        /// Updates input
        /// </summary>
        public void Update()
        {
            // The mouse
            oldMouse = newMouse;
            newMouse = Microsoft.Xna.Framework.Input.Mouse.GetState();

            // The keyboard
            oldKeyboard = newKeyboard;
            newKeyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            // The gamepad
            for (int i = 0; i < newGamePad.Length; i++)
            {
                oldGamePad[i] = newGamePad[i];
                newGamePad[i] = Microsoft.Xna.Framework.Input.GamePad.GetState((PlayerIndex)i);
            }
        }
    }
}
