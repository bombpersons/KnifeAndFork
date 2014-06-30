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
    public class RocketThruster : Accessory
    {
        /// <summary>
        /// The power of the thruster
        /// </summary>
        protected float power = 1000f;
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
        /// Creates a new Rocket Thruster
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_wearer"></param>
        /// <param name="_depth"></param>
        public RocketThruster(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/RocketThruster", 100, 50, 0, 0), _depth)
        {
            sprite.AddAnimation("Thrust", 0, 2, 10, true);
            sprite.SetCurrentAnimation("Thrust");
        }

        /// <summary>
        /// Push the wearer forward
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
                        float angleToTarget = (float)Math.Atan2((shape.Parent.Target.Y - shape.ThePhysics.body.Position.Y*Physics.PhysicsObject.PixelsToMetres),
                                                                (shape.Parent.Target.X - shape.ThePhysics.body.Position.X*Physics.PhysicsObject.PixelsToMetres));
                        float shapeAngle = (shape.ThePhysics.body.GetAngle() % (2*(float)Math.PI));
                        if (shapeAngle > angleToTarget)
                        {
                            shape.ThePhysics.body.ApplyTorque(-1 * (Power) * Math.Abs(shapeAngle - angleToTarget));
                        }
                        else
                        {
                            shape.ThePhysics.body.ApplyTorque(1 * (Power) * Math.Abs(shapeAngle - angleToTarget));
                        }

                        if (Math.Abs(shapeAngle - angleToTarget) < 0.5f)
                        {
                            shape.ThePhysics.body.ApplyForce(new Vector2((float)Math.Cos(shapeAngle), (float)Math.Sin(shapeAngle))*Power, shape.ThePhysics.body.GetWorldCenter());
                            //shape.ThePhysics.body.ApplyForce(shape.Target - shape.ThePhysics.body.Position, shape.ThePhysics.body.GetWorldCenter());
                        }

                        // Check out how much mass the shape has, die if it has too little
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
