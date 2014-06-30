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

using Orange.XNA.Input;

namespace Orange.XNA.Game
{
    /// <summary>
    /// A game state. Basically a screen in the game.
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Whether or not to call the update method
        /// </summary>
        bool active = true;
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }


        /// <summary>
        /// Whether or not to call the draw method
        /// </summary>
        bool visible = true;
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }

        /// <summary>
        /// Whether or not to call HandleInput
        /// </summary>
        bool focused = false;
        public bool Focused
        {
            get
            {
                return focused;
            }
            set
            {
                focused = value;
            }
        }

        /// <summary>
        /// The name of the gamestate
        /// </summary>
        string name = String.Empty;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// The game state manager
        /// </summary>
        GameStateManager manager;
        public GameStateManager Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
            }
        }

        /// <summary>
        /// Sprite batch for this screen to use
        /// </summary>
        protected SpriteBatch spriteBatch;

        /// <summary>
        /// The content manager to load stuff with
        /// </summary>
        protected ContentManager content;

        /// <summary>
        /// The graphics device
        /// </summary>
        protected GraphicsDevice graphicsDevice;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameState(GraphicsDevice _graphicsDevice, ContentManager _content)
        {
            spriteBatch = new SpriteBatch(_graphicsDevice);
            content = _content;
            graphicsDevice = _graphicsDevice;
        }

        /// <summary>
        /// Called when the screen is drawn
        /// </summary>
        public virtual void Draw()
        {
        }

        /// <summary>
        /// Handle input for the state
        /// </summary>
        public virtual void HandleInput(GameTime _gameTime, Input.Input _input)
        {
        }
        
        /// <summary>
        /// Updatet the logic for your screen
        /// </summary>
        /// <param name="_gameTime"></param>
        public virtual void Update(GameTime _gameTime)
        {
        }
    }
}
