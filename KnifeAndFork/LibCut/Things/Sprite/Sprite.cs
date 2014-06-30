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

namespace LibCut.Things.Sprite
{
    public class Sprite : Thing
    {
        /// <summary>
        /// Dispose of the texture
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
                //if (value)
                //    sprite.texture.Dispose();
            }
        }

        /// <summary>
        /// Whether or not the sprite is visible
        /// </summary>
        public bool Visible
        {
            get
            {
                if (sprite != null)
                {
                    return sprite.visible;
                }

                return true;
            }
            set
            {
                if (sprite != null)
                {
                    sprite.visible = value;
                }
            }
        }

        // The sprite to use
        protected Orange.XNA.Sprite sprite;
        public Orange.XNA.Sprite TheSprite
        {
            get
            {
                return sprite;
            }
            set
            {
                sprite = value;
            }
        }

        /// <summary>
        /// The sprite batch to use 
        /// </summary>
        protected SpriteBatch spriteBatch;

        /// <summary>
        /// Create a new object
        /// </summary>
        /// <param name="_content"></param>
        /// <param name="_name"></param>
        public Sprite(Universe.Universe _universe, Orange.XNA.Sprite _sprite, int _depth)
            : base(_universe, _depth)
        {
            sprite = _sprite;
            spriteBatch = new SpriteBatch(Universe.GraphicsDevice);
        }

        /// <summary>
        /// Update the sprite
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            if (sprite != null)
                sprite.Update(_gameTime);
        }

        /// <summary>
        /// Draw the sprite
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_graphicsDevice"></param>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            base.Draw(_camera, _graphicsDevice);

            // Only draw if we are alive
            if (sprite != null)
            {

                // Set position and such
                sprite.position = Vector2.Transform(Position, _camera.CameraTransform);
                sprite.position = new Vector2(sprite.position.X, -sprite.position.Y) + new Vector2(_graphicsDevice.Viewport.Width / 2, _graphicsDevice.Viewport.Height / 2);
                sprite.scale = Scale * _camera.Scale;
                sprite.rotation = Rotation + _camera.Rotation;

                // Now draw the sprite
                spriteBatch.Begin();
                sprite.Draw(spriteBatch);
                spriteBatch.End();
            
            }
        }
    }
}
