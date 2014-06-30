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

namespace LibCut.Things.Actors.Accessories.Utility
{
    public class SelfRighter : Accessory
    {
        /// <summary>
        /// How much to adjust by
        /// </summary>
        protected float adjustment = 20f;
        public float Adjustment
        {
            get
            {
                return adjustment;
            }
            set
            {
                adjustment = value;
            }
        }

        /// <summary>
        /// Creates a new selfrighter
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_wearer"></param>
        /// <param name="_depth"></param>
        public SelfRighter(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/SelfRighter"), _depth)
        {
        }

        /// <summary>
        /// Self right the wearing shape
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            var shape = Wearer as Shapes.PhysicsShape;
            if (shape != null)
            {
                if (shape.ThePhysics != null)
                {
                    if (shape.ThePhysics.body != null)
                    {
                        if (shape.ThePhysics.Rotation % ((float)Math.PI * 2) > 0)
                        {
                            shape.ThePhysics.body.ApplyTorque(-Adjustment * shape.ThePhysics.Rotation);
                        }
                        if (shape.ThePhysics.Rotation % ((float)Math.PI * 2) < 0)
                        {
                            shape.ThePhysics.body.ApplyTorque(Adjustment * -shape.ThePhysics.Rotation);
                        }
                    }
                }
            }
        }
    }
}
