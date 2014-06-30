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

namespace Orange.XNA.Game
{
    public class GameStateManager
    {
        /// <summary>
        /// A list of game states to manage
        /// </summary>
        List<GameState> gameStates = new List<GameState>();
        List<GameState> newStates = new List<GameState>();
        public List<GameState> GameStates
        {
            get
            {
                return newStates;
            }
            set
            {
                newStates = value;
            }
        }

        /// <summary>
        /// A graphics device to draw with.
        /// </summary>
        GraphicsDevice graphicsDevice;

        /// <summary>
        /// The input to the game
        /// </summary>
        Input.Input InputState;

        /// <summary>
        /// Whether or not to darken unfocus game states
        /// </summary>
        bool darkenUnfocused = false;
        public bool DarkenUnfocused
        {
            get
            {
                return darkenUnfocused;
            }
            set
            {
                darkenUnfocused = value;
            }
        }

        /// <summary>
        /// How much to darken the unfocused states
        /// </summary>
        float darkenAlpha = 1.0f;
        public float DarkenAlpha
        {
            get
            {
                return darkenAlpha;
            }
            set
            {
                if (value < 1.0f)
                {
                    darkenAlpha = value;
                }
                else
                {
                    darkenAlpha = 1.0f;
                }

                darkenPlane.tint = darkenColor * darkenAlpha;
            }
        }

        /// <summary>
        /// What color to darken unfocused states
        /// </summary>
        Color darkenColor = Color.Black;
        public Color DarkenColor
        {
            get
            {
                return darkenColor;
            }
            set
            {
                darkenColor = value;
                darkenPlane.tint = darkenColor;
                DarkenAlpha = darkenAlpha;
            }
        }

        /// <summary>
        /// The rect used to darken unfocused screens
        /// </summary>
        Graphics.Primitives.Rect darkenPlane;

        /// <summary>
        /// A spritebatch to draw sprites with
        /// </summary>
        SpriteBatch spriteBatch;

        /// <summary>
        /// Creates the state manager
        /// </summary>
        /// <param name="_graphicsDevice"></param>
        public GameStateManager(GraphicsDevice _graphicsDevice)
        {
            // Save the graphics device
            graphicsDevice = _graphicsDevice;

            // The input state
            InputState = new Input.Input();

            // Create a sprite batch to draw the darken plane and other things
            spriteBatch = new SpriteBatch(_graphicsDevice);

            // Generate textures for the primitives just in case it wasn't done by the user.
            Graphics.Primitives.TextureGen.GenerateTexture(_graphicsDevice);

            // Create the plane that's used to darken unfocused screens
            darkenPlane = new Graphics.Primitives.Rect(new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height));

            // Some default values
            
            // Darken values
            DarkenAlpha = 0.3f;
            DarkenColor = Color.Black;
        }

        /// <summary>
        /// Adds a game state to the manager. Returns a pointer to the gamestate
        /// </summary>
        /// <param name="_gameState"></param>
        public GameState AddGameState(GameState _gameState)
        {
            if (!GameStates.Contains(_gameState))
            {
                _gameState.Manager = this;
                GameStates.Add(_gameState);
            }
            return _gameState;
        }

        /// <summary>
        /// Removes a gamestate from the manager
        /// </summary>
        /// <param name="_index"></param>
        public void RemoveGameState(int _index)
        {
            if (GameStates.Count > _index)
            {
                GameStates.RemoveAt(_index);
            }
        }

        /// <summary>
        /// Remove gamestates by name
        /// </summary>
        /// <param name="_name"></param>
        public void RemoveGameState(string _name)
        {
            for (int i = 0; i < GameStates.Count; i++)
            {
                if (GameStates[i].Name == _name)
                {
                    RemoveGameState(i);
                }
            }
        }
        
        /// <summary>
        /// Sets a screen to focus
        /// </summary>
        /// <param name="_name"></param>
        public void SetFocus(string _name)
        {
            int index = GetIndexFromName(_name);
            GameState state = GameStates[index];
            RemoveGameState(index);
            GameStates.Insert(0, state);
        }

        /// <summary>
        /// Draws the gamestates
        /// </summary>
        public virtual void Draw()
        {
            for (int i = gameStates.Count - 1; i >= 0; i--)
            {
                if (gameStates[i].Visible)
                {
                    gameStates[i].Draw();
                }

                // Draw the darkened plane if necessary
                if (i != 0)
                {
                    spriteBatch.Begin(0, BlendState.AlphaBlend);
                    darkenPlane.Draw(spriteBatch);
                    spriteBatch.End();
                }
            }
        }

        /// <summary>
        /// Updates the states.
        /// </summary>
        public virtual void Update(GameTime _gameTime)
        {
            // Update input
            InputState.Update();

            // Change for the new list, so that we don't modify the list we are enumerating
            gameStates.Clear();
            for (int i = 0; i < newStates.Count; i++)
            {
                gameStates.Add(newStates[i]);
            }

            // Set the first screen as the focused screen
            foreach (GameState state in gameStates)
            {
                state.Focused = false;
            }
            gameStates[0].Focused = true;

            foreach (GameState state in gameStates)
            {
                if (state.Focused)
                {
                    state.HandleInput(_gameTime, InputState);
                }
                if (state.Active)
                {
                    state.Update(_gameTime);
                }
            }
        }

        /// <summary>
        /// Gets a game state object with _name name
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public GameState GetState(string _name)
        {
            return GameStates[GetIndexFromName(_name)];
        }

        /// <summary>
        /// Gets the index of a game state from it's name. Returns -1 if the name could not be found.
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        protected int GetIndexFromName(string _name)
        {
            for (int i = 0; i < GameStates.Count; i++)
            {
                if (GameStates[i].Name == _name)
                {
                    return i;
                }
            }

            // If we can't find it, return -1
            return -1;
        }
    }
}
