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

namespace Orange.XNA.Graphics.UI
{
    /// <summary>
    /// This widget does nothing. Please inherit from it if you want to make your own widgets!
    /// </summary>
    public class Widget
    {
        /// <summary>
        /// Whether or not the mouse is hovering over the widget
        /// </summary>
        protected bool hover = false;
        public bool Hover
        {
            get
            {
                if (hover)
                {
                    hover = false;
                    return true;
                }
                    
                return hover;
            }
        }

        /// <summary>
        /// Whether or not the widget has been clicked on
        /// </summary>
        protected bool clicked = false;
        public bool Clicked
        {
            get
            {
                if (clicked)
                {
                    clicked = false;
                    return true;
                }

                return clicked;
            }
        }

        /// <summary>
        /// The hit box
        /// </summary>
        protected Rectangle collision;
        public Vector2 Size
        {
            get
            {
                return new Vector2(collision.Width, collision.Height);
            }
        }

        /// <summary>
        /// Mouse states
        /// </summary>
        protected MouseState mouse;
        protected MouseState oldMouse;

        /// <summary>
        /// Change the position of the widget
        /// </summary>
        public virtual Vector2 Position
        {
            get
            {
                return new Vector2(collision.Left,  collision.Top);
            }
            set
            { 
                collision = new Rectangle((int)value.X, (int)value.Y, collision.Width, collision.Height);
                Console.WriteLine(collision);
            }
        }


        /// <summary>
        /// Empty constructor
        /// </summary>
        public Widget()
        {
        }

        /// <summary>
        /// Sets up the hit box
        /// </summary>
        /// <param name="_rect"></param>
        public Widget(Rectangle _rect)
        {
            collision = _rect;
        }

        /// <summary>
        /// Updates the widget. Call backs will occur here.
        /// </summary>
        /// <param name="_gameTime"></param>
        public virtual void Update(GameTime _gameTime)
        {
            // Update the old mousestate
            oldMouse = mouse;
            if (oldMouse == null)
            {
                oldMouse = Mouse.GetState();
            }

            // Check if the mouse is in the hit box
            mouse = Mouse.GetState();
            if (mouse.X > collision.Left && mouse.X < collision.Right
                && mouse.Y > collision.Top && mouse.Y < collision.Bottom)
            {
                hover = true;
                
                // Check if we were clicked on
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                {
                    clicked = true;
                }
            }
        }

        /// <summary>
        /// Draws the widget to the screen
        /// </summary>
        /// <param name="_spriteBatch"></param>
        public virtual void Draw(SpriteBatch _spriteBatch)
        {
        }
    }
}
