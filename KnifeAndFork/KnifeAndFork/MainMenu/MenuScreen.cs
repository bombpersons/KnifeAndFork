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

namespace KnifeAndFork.MainMenu
{
    public class MenuScreen : Orange.XNA.Game.GameState
    {
        /// <summary>
        /// The game universe
        /// </summary>
        MenuUniverse universe;

        /// <summary>
        /// Creates a menu screen
        /// </summary>
        /// <param name="_graphicsDevice"></param>
        /// <param name="_content"></param>
        public MenuScreen(GraphicsDevice _graphicsDevice, ContentManager _content)
            : base(_graphicsDevice, _content)
        {
            universe = new MenuUniverse(_content, _graphicsDevice);
        }

        /// <summary>
        /// Handle any input
        /// </summary>
        /// <param name="_gameTime"></param>
        /// <param name="_input"></param>
        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);
            universe.HandleInput(_gameTime, _input);
        }

        /// <summary>
        /// Updates
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
            universe.Update(_gameTime);
        }

        /// <summary>
        /// Draw the screen
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            universe.Draw();
        }
    }
}
