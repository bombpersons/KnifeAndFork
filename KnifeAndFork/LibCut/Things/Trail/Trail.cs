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

namespace LibCut.Things.Trail
{
    public class Trail : Sprite.Sprite
    {
        /// <summary>
        /// The thing to follow
        /// </summary>
        protected Thing follow;
        public Thing Follow
        {
            get
            {
                return follow;
            }
            set
            {
                follow = value;
            }
        }

        /// <summary>
        /// Creates a new trail that will follow and object
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_follow"></param>
        /// <param name="_sprite"></param>
        /// <param name="_depth"></param>
        public Trail(Universe.Universe _universe, Thing _follow, Orange.XNA.Sprite _sprite,  int _depth)
            : base(_universe, _sprite, _depth)
        {
            // Change the center of the sprite
            sprite.center = new Vector2(0, sprite.size.Y / 2);
        }

        /// <summary>
        /// Change the rotation based on the rotation of the follow object
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Set the same rotation
            Rotation = follow.Rotation;
        }
    }
}
