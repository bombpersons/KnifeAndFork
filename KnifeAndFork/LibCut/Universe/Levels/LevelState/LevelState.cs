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

namespace LibCut.Universe.Levels.LevelState
{
    public class LevelState : Orange.XNA.Game.GameState
    {
        /// <summary>
        /// The current level
        /// </summary>
        protected Level level;
        protected Level newLevel;
        public Level Level
        {
            get
            {
                return level;
            }
            set
            {
                // Set the newLevel to this and start the timer
                newLevel = value;
                levelSwitch = fadeTime.Duration();
            }
        }

        /// <summary>
        /// A timer to control the fade when changing levels
        /// </summary>
        protected TimeSpan levelSwitch;
        protected TimeSpan fadeTime = new TimeSpan(0, 0, 1);

        /// <summary>
        /// A rectangle to go over the whole screen (for fading)
        /// </summary>
        protected Orange.XNA.Graphics.Primitives.Rect fader;

        /// <summary>
        /// A splash to draw in the background
        /// </summary>
        protected Orange.XNA.Sprite splash;

        /// <summary>
        /// Creates a new level state. A generic game state that can play levels
        /// </summary>
        /// <param name="_graphicsDevice"></param>
        /// <param name="_content"></param>
        public LevelState(GraphicsDevice _graphicsDevice, ContentManager _content)
            : base(_graphicsDevice, _content)
        {
            // Create the rectangle
            fader = new Orange.XNA.Graphics.Primitives.Rect(_graphicsDevice.Viewport.Bounds);

            // Create the splash dealy 
            splash = new Orange.XNA.Sprite(content, @"PausedScreens/Splash");
            splash.center = Vector2.Zero;
        }

        /// <summary>
        /// Update the level
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
            if (level != null)
                level.Update(_gameTime);

            // If the level fade timer is bigger than 0 ticks then update it
            if (levelSwitch.Ticks > 0)
            {
                levelSwitch -= _gameTime.ElapsedGameTime;
            }
            else
            {
                level = newLevel;
            }
        }

        /// <summary>
        /// Send input to the level
        /// </summary>
        /// <param name="_gameTime"></param>
        /// <param name="_input"></param>
        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);
            if (level != null)
                level.HandleInput(_gameTime, _input);
        }

        /// <summary>
        /// Draw the level
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            
            // Draw the splash thingy
            spriteBatch.Begin();
            splash.Draw(spriteBatch);
            spriteBatch.End();

            if (level != null)
                level.Draw();

            // Draw the rectangle fader
            if (newLevel != level)
                fader.tint = Color.Black * (1 - ((float)levelSwitch.Ticks / (float)fadeTime.Ticks));
            else
                fader.tint = Color.Black * 0.0f;

            spriteBatch.Begin();
            fader.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
