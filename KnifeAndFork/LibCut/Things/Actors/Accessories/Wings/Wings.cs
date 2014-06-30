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

namespace LibCut.Things.Actors.Accessories.Wings
{
    public class Wings : Accessory
    {
        /// <summary>
        /// The force of a flap
        /// </summary>
        protected float flapForce = 100.0f;
        public float FlapForce
        {
            get
            {
                return flapForce;
            }
            set
            {
                flapForce = value;
            }
        }

        /// <summary>
        /// The frame to flap on
        /// </summary>
        protected int flapFrame = 0;
        public int FlapFrame
        {
            get
            {
                return flapFrame;
            }
            set
            {
                flapFrame = value;
            }
        }

        /// <summary>
        /// Create a new instance of wings
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_wearer"></param>
        public Wings(Universe.Universe _universe, Thing _wearer, Orange.XNA.Sprite _sprite, int _depth)
            : base(_universe, _wearer, _sprite, _depth)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Check if the wearer is a physics shape
            if (Wearer is Shapes.PhysicsShape)
            {
                Shapes.PhysicsShape shape = (Shapes.PhysicsShape)Wearer;

                // Make the wearer escapable from the plate
                shape.Escapable = true;

                // Make sure the physics are there
                if (shape.ThePhysics != null)
                {
                    if (shape.ThePhysics.body != null)
                    {

                        // Make a upward force, every time the wings flap
                        if ((int)sprite.frame.frame == FlapFrame)
                        {
                            if (shape.Position.Y > shape.Parent.Target.Y)
                            {
                                shape.ThePhysics.body.ApplyForce(new Vector2(0, -1) * FlapForce * (shape.ThePhysics.body.GetMass()), shape.ThePhysics.body.GetWorldCenter());
                            }
                        }

                        // If the mass of the shape is too small then kill the wings
                        if (shape.ThePhysics.body.GetMass() < 10)
                        {
                            Dead = true;
                        }
                    }
                }

            }
                
        }
    }
}
