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

namespace LibCut.Things.Actors.Accessories.Thrusters
{
    public class SimpleThruster : Accessory
    {
        /// <summary>
        /// The power of the thruster
        /// </summary>
        protected float power = 20f;
        public float Power
        {
            get
            {
                return power;
            }
            set
            {
                power = value;
            }
        }

        /// <summary>
        /// Creates a new simplethruster
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_wearer"></param>
        /// <param name="_depth"></param>
        public SimpleThruster(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/ThrusterSimple", 50, 50, 0, 0), _depth)
        {
            sprite.AddAnimation("Thrust", 0, 3, 10, true);
            sprite.SetCurrentAnimation("Thrust");
        }

        /// <summary>
        /// Do thruster stuff
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(Microsoft.Xna.Framework.GameTime _gameTime)
        {
            base.Update(_gameTime);

            var shape = Wearer as Shapes.PhysicsShape;
            if (shape != null)
            {
                // Make the wearer escapable from the plate
                shape.Escapable = true;

                if (shape.ThePhysics != null)
                {
                    if (shape.ThePhysics.body != null)
                    {
                        if (shape.Parent.Target.X > shape.Position.X)
                        {
                            shape.ThePhysics.body.ApplyForce(new Vector2(1, 0) * Power, shape.ThePhysics.body.GetWorldCenter());
                        }
                        if (shape.Parent.Target.X < shape.Position.X)
                        {
                            shape.ThePhysics.body.ApplyForce(new Vector2(-1, 0) * Power, shape.ThePhysics.body.GetWorldCenter());
                        }
                    }
                }
            }
        }
    }
}
