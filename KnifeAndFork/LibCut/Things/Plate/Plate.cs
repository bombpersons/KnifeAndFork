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

using Box2D.XNA;

namespace LibCut.Things.Plate
{
    public class Plate : Things.Sprite.Sprite
    {
        /// <summary>
        /// Set the position of the bounds too
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
                //bounds[0].Position = (value + new Vector2(0, (sprite.size.Y / 2))) / Physics.PhysicsObject.PixelsToMetres;
                //bounds[1].Position = (value + new Vector2((sprite.size.X / 2), 0)) / Physics.PhysicsObject.PixelsToMetres;
                //bounds[2].Position = (value + new Vector2(-(sprite.size.X / 2), 0)) / Physics.PhysicsObject.PixelsToMetres;
            }
        }

        /// <summary>
        /// The bounds
        /// </summary>
        Physics.PhysicsObject[] bounds = new Physics.PhysicsObject[3];

        /// <summary>
        /// The list of progress bars
        /// </summary>
        protected List<Health.CollectionProgress> progressBars = new List<Health.CollectionProgress>();
        public List<Health.CollectionProgress> ProgressBars
        {
            get
            {
                return progressBars;
            }
            set
            {
                progressBars = value;
            }
        }

        /// <summary>
        /// Creates a new plate
        /// </summary>
        /// <param name="_universe"></param>
        public Plate(Universe.Universe _universe, Orange.XNA.Sprite _sprite, int _depth)
            : base(_universe, _sprite, _depth)
        {
            // Create boundaries to keep in the stuff
            // The bottom bit of the plate
            /*bounds[0] = new Physics.PhysicsObject(Universe.TheWorld);
            bounds[0].Size = new Vector2(_sprite.size.X, 10);
            bounds[0].userData = this;
            bounds[0].ChangeShape(Physics.PhysicsObject.Shapes.BOX, null);

            bounds[1] = new Physics.PhysicsObject(Universe.TheWorld);
            bounds[1].Size = new Vector2(10, _sprite.size.Y);
            bounds[1].userData = this;
            bounds[1].ChangeShape(Physics.PhysicsObject.Shapes.BOX, null);

            bounds[2] = new Physics.PhysicsObject(Universe.TheWorld);
            bounds[2].Size = new Vector2(10, _sprite.size.Y);
            bounds[2].userData = this;
            bounds[2].ChangeShape(Physics.PhysicsObject.Shapes.BOX, null);*/

            // Set the position to 0
            Position = Vector2.Zero;

            // Make sure we don't get gargbage cleaned
            GarbageCleaned = false;
        }


        /// <summary>
        /// Update the positions of the grogress bars
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Set the positions
            for (int i = 0; i < progressBars.Count; i++)
            {
                progressBars[i].Position = new Vector2((i + 1) * 30, Universe.GraphicsDevice.Viewport.Height/2);
            }

            // The query AABB
            AABB bounds = new AABB()
            {
                upperBound = ((Position + ((new Vector2(sprite.texture.Width, sprite.texture.Height) / 2) * 0.6f))) / Physics.PhysicsObject.PixelsToMetres,
                lowerBound = ((Position - ((new Vector2(sprite.texture.Width, sprite.texture.Height) / 2) * 0.6f))) / Physics.PhysicsObject.PixelsToMetres,
            };

            // The bounds to check
            BoundingBox checkBounds = new BoundingBox(new Vector3((((Position + ((new Vector2(sprite.texture.Width, sprite.texture.Height) / 2) * 0.6f))) / Physics.PhysicsObject.PixelsToMetres), 0),
                                                      new Vector3((((Position - ((new Vector2(sprite.texture.Width, sprite.texture.Height) / 2) * 0.6f))) / Physics.PhysicsObject.PixelsToMetres), 0));
            

            // Find anything that isn't on the plate
            foreach (Things.Thing thing in Universe.Things)
            {
                if (thing is Shapes.PhysicsShape)
                {
                    if ((thing as Shapes.PhysicsShape).ThePhysics.body != null)
                    {
                        if (checkBounds.Contains(new Vector3((thing as Shapes.PhysicsShape).ThePhysics.body.GetWorldCenter(), 0)) == ContainmentType.Disjoint)
                        {
                            (thing as Shapes.PhysicsShape).ThePhysics.body.SetLinearDamping(0.4f);
                            (thing as Shapes.PhysicsShape).ThePhysics.body.SetAngularDamping(0.4f);
                        }
                    }
                }
            }

            // Find anything in the plate
            Universe.TheWorld.QueryAABB(found =>
            {
                if (found.fixture.GetUserData() is Shapes.PhysicsShape)
                {
                    if (!(found.fixture.GetUserData() as Thing).Escapable)
                    {
                        (found.fixture.GetUserData() as Shapes.PhysicsShape).ThePhysics.body.SetLinearDamping(20000.0f);
                        (found.fixture.GetUserData() as Shapes.PhysicsShape).ThePhysics.body.SetAngularDamping(20000.0f);
                    }
                }
                return true;
            }, ref bounds);
        }

        /// <summary>
        /// Gets the amount of mass on the plate of a certain type
        /// </summary>
        /// <returns></returns>
        public float GetMass(Type _type)
        {
            // The query AABB
            AABB bounds = new AABB()
            {
                upperBound = ((Position + ((new Vector2(sprite.texture.Width, sprite.texture.Height) / 2) * 0.6f))) / Physics.PhysicsObject.PixelsToMetres,
                lowerBound = ((Position - ((new Vector2(sprite.texture.Width, sprite.texture.Height) / 2) * 0.6f))) / Physics.PhysicsObject.PixelsToMetres,
            };

            // A float to store the mass counted up
            float totalMass = 0.0f;

            // Find anything in the plate
            Universe.TheWorld.QueryAABB(found =>
                {
                    if (found.fixture.GetUserData() is Shapes.PhysicsShape)
                    {
                        if ((found.fixture.GetUserData() as Shapes.PhysicsShape).Parent.GetType() == _type)
                        {
                            if (!(found.fixture.GetUserData() as Shapes.PhysicsShape).Escapable)
                            {
                                totalMass += (found.fixture.GetUserData() as Shapes.PhysicsShape).ThePhysics.body.GetMass();
                            }
                        }
                    }
                    return true;
                }, ref bounds);

            // Okay =) return the end result
            return totalMass;
        }
    }
}
