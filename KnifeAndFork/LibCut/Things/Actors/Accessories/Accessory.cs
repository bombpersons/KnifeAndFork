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

namespace LibCut.Things.Actors.Accessories
{
    /// <summary>
    /// An accessory for an actor. Can be glasses, wings, mustache, etc
    /// </summary>
    public class Accessory : Things.Sprite.Sprite
    {
        /// <summary>
        /// The wearer of this accessory
        /// </summary>
        protected Things.Thing wearer;
        public Things.Thing Wearer
        {
            get
            {
                return wearer;
            }
            set
            {
                wearer = value;
            }
        }

        /// <summary>
        /// The offset from the center of the wearer
        /// </summary>
        protected Vector2 clamp = Vector2.Zero;
        public Vector2 Clamp
        {
            get
            {
                return clamp;
            }
            set
            {
                clamp = value;
            }
        }

        /// <summary>
        /// Creates a new accesory.
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_wearer"></param>
        /// <param name="_sprite"></param>
        public Accessory(Universe.Universe _universe, Things.Thing _wearer, Orange.XNA.Sprite _sprite, int _depth)
            : base(_universe, _sprite, _depth)
        {
            Wearer = _wearer;
        }

        /// <summary>
        /// Adds the accessory to the wearer
        /// </summary>
        public void AddToWearer()
        {
            // Add ourselves to the wearers accessory list
            Wearer.Accessories.Add(this);

            // Calculate the clamp
            Clamp = Position;
        }

        /// <summary>
        /// Kill ourselves if the wearer dies
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            if (Wearer.Dead)
                Dead = true;
        }
    }
}
