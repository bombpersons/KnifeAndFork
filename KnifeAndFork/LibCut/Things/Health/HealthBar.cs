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

namespace LibCut.Things.Health
{
    public class HealthBar : Things.Sprite.Sprite
    {
        /// <summary>
        /// The amount of health this health bar has
        /// </summary>
        float health = 1.0f;
        public float Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
                if (Side)
                {
                    sprite.clippingRect = new Rectangle(sprite.texture.Width - (int)(sprite.texture.Width * health), 0, sprite.texture.Width, (int)sprite.size.Y);
                }
                else
                {
                    sprite.clippingRect = new Rectangle(0, 0, (int)(sprite.texture.Width * health), (int)sprite.size.Y);
                }

                sprite.tint = new Color(new Vector4(1, 1*(health), 1*(health), 1));

            }
        }

        /// <summary>
        /// Which side the health bar is on
        /// </summary>
        protected bool side = false;
        public bool Side
        {
            get
            {
                return side;
            }
            set
            {
                side = value;
            }
        }

        /// <summary>
        /// Creates a new HealthBar
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_sprite"></param>
        public HealthBar(Universe.Universe _universe, Orange.XNA.Sprite _sprite)
            : base(_universe, _sprite, 90001)
        {
        }

        /// <summary>
        /// Update the health bar
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
        }

        /// <summary>
        /// Draw the sprite to a fixed local position
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_graphicsDevice"></param>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            //base.Draw(_camera, _graphicsDevice);
            spriteBatch.Begin();
            sprite.position = Position;
            sprite.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
