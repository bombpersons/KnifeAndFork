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

namespace LibCut.Things.Actors.Accessories.Menu
{
    public class MenuItem : Accessory
    {
        /// <summary>
        /// Set the position of the textbox with the same position as the menu item.
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
                text.Position = value;
            }
        }
        public override float Rotation
        {
            get
            {
                return base.Rotation;
            }
            set
            {
                base.Rotation = value;
                text.Rotation = value;
            }
        }

        /// <summary>
        /// The type of function that this menuItem will call
        /// </summary>
        public delegate void Function();

        /// <summary>
        /// The function
        /// </summary>
        protected Function func;
        public Function Func
        {
            get
            {
                return func;
            }
            set
            {
                func = value;
            }
        }

        /// <summary>
        /// Whether or not func has been called
        /// </summary>
        protected bool calledFunc;

        /// <summary>
        /// The textbox to use to draw text
        /// </summary>
        protected RPGText.TextBoxDrawing text;

        /// <summary>
        /// Creates a new menu item
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_wearer"></param>
        /// <param name="_text"></param>
        /// <param name="_func"></param>
        /// <param name="_depth"></param>
        public MenuItem(Universe.Universe _universe, Thing _wearer, string _text, Function _func, int _depth)
            : base(_universe, _wearer, null, _depth)
        {
            SpriteFont font = Universe.Content.Load<SpriteFont>(@"Font");
            text = new RPGText.TextBoxDrawing(font.MeasureString(_text) + new Vector2(20, 20), 2, font);
            text.Text = _text;
            text.Scale = new Vector2(4.0f);
            text.Center = Vector2.Zero;

            Func = _func;
            calledFunc = false;
        }

        /// <summary>
        /// Updatet the text
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            text.UpdateText(_gameTime);

            // Decide whether or not we have been activated
            var shape = Wearer as Shapes.PhysicsShape;
            if (shape != null)
            {
                // If the shape is held
                if (shape.Held && !calledFunc)
                {
                    Func();
                    calledFunc = true;
                }

                if (!shape.Held && calledFunc)
                {
                    calledFunc = false;
                }
            }
        }

        /// <summary>
        /// Draw the text
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_graphicsDevice"></param>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            //base.Draw(_camera, _graphicsDevice);

            // Draw the text
            text.Position = Position;
            text.Position = Vector2.Transform(text.Position, _camera.CameraTransform);
            text.Position = new Vector2(text.Position.X, -text.Position.Y) + new Vector2(_graphicsDevice.Viewport.Width / 2, _graphicsDevice.Viewport.Height / 2);
            Vector2 oldScale = text.Scale;
            text.Scale = text.Scale * _camera.Scale;
            text.Draw(spriteBatch);
            text.Scale = oldScale;
        }
    }
}
