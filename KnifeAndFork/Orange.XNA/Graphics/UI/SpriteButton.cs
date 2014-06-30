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
    /// A button that uses a sprite
    /// </summary>
    public class SpriteButton : Button
    {
        /// <summary>
        /// The sprite to use as the button
        /// </summary>
        protected Sprite image;

        /// <summary>
        /// Override the position accessor to make sure the sprite position is updated
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
                image.position = value;
            }
        }

        /// <summary>
        /// Constructor uses the sprite to create a collision rectangle
        /// </summary>
        /// <param name="_sprite"></param>
        public SpriteButton(Sprite _sprite)
            : base(new Rectangle((int)(_sprite.position - _sprite.center).X, (int)(_sprite.position - _sprite.center).Y,
                (int)_sprite.size.X*(int)_sprite.scale.X, (int)_sprite.size.Y*(int)_sprite.scale.Y))
        {
            image = _sprite;
            image.center = new Vector2(0.0f, 0.0f);
        }

        /// <summary>
        /// Draw the sprite
        /// </summary>
        /// <param name="_spriteBatch"></param>
        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);

            _spriteBatch.Begin(0, BlendState.AlphaBlend);
            image.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}
