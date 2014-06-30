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

namespace Orange.XNA.Graphics.UI.Menu
{
    /// <summary>
    /// A menu with buttons. Inherit this to customize your own menu.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// A list of buttons to show.
        /// </summary>
        List<Button> buttons = new List<Button>();

        /// <summary>
        /// The position of the menu
        /// </summary>
        protected Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                OrganizeButtons();
            }
        }

        /// <summary>
        /// The spacing between the menu items
        /// </summary>
        protected Vector2 spacing;
        public Vector2 Spacing
        {
            get
            {
                return spacing;
            }
            set
            {
                spacing = value;
            }
        }

        /// <summary>
        /// Create the menu
        /// </summary>
        /// <param name="_graphicsDevice"></param>
        public Menu()
        {
            // Defualt values
            spacing = new Vector2(0.0f, 20.0f);
        }

        /// <summary>
        /// Draw the menu
        /// </summary>
        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            foreach (Button button in buttons)
            {
                button.Draw(_spriteBatch);
            }
        }

        /// <summary>
        /// Updates everything
        /// </summary>
        /// <param name="_gameTime"></param>
        public virtual void Update(GameTime _gameTime)
        {
            foreach (Button button in buttons)
            {
                button.Update(_gameTime);
            }
        }

        /// <summary>
        /// Add a button
        /// </summary>
        /// <param name="_button"></param>
        public virtual void AddButton(Button _button)
        {
            buttons.Add(_button);

            // Update the list
            OrganizeButtons();
        }

        /// <summary>
        /// Removes a button
        /// </summary>
        /// <param name="i"></param>
        public virtual void RemoveButton(int i)
        {
            if (i < buttons.Count)
            {
                buttons.RemoveAt(i);
            }

            OrganizeButtons();
        }

        /// <summary>
        /// Organizes the buttons
        /// </summary>
        protected virtual void OrganizeButtons()
        {
            Vector2 lastPos = position;
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Position = lastPos + (i * Spacing);
                lastPos += new Vector2(0.0f, buttons[i].Size.Y);
            }
        }
    }
}
