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

namespace LibCut.Things.Players
{
    public class Fork : Player
    {
        /// <summary>
        /// Kill the dummy fork if we die
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
                if (value)
                {
                    if (physics.body != null)
                    {
                        (physics.body.GetFixtureList().GetUserData() as DummyFork).Dead = true;
                    }
                }
            }
        }

        /// <summary>
        /// If the fork is in piercing mode or not.
        /// </summary>
        protected bool piercing = false;
        public bool Piercing
        {
            get
            {
                return piercing;
            }
            set
            {
                piercing = value;
            }
        }

        /// <summary>
        /// The sensor to grab objects with
        /// </summary>
        Physics.PhysicsObject pierceSensor;
        public Physics.PhysicsObject PierceSensor
        {
            get
            {
                return pierceSensor;
            }
            set
            {
                pierceSensor = value;
            }
        }

        /// <summary>
        /// The amount to rotate.
        /// </summary>
        protected float piercingRotation = -6*(float)Math.PI / 16;
        public float PiercingRotation
        {
            get
            {
                return piercingRotation;
            }
            set
            {
                piercingRotation = value;
            }
        }

        /// <summary>
        /// Stabbing sound
        /// </summary>
        protected SoundEffect stab;
        public SoundEffect Stab
        {
            get
            {
                return stab;
            }
            set
            {
                stab = value;
            }
        }

        /// <summary>
        /// Creates a new fork
        /// </summary>
        /// <param name="_universe"></param>
        public Fork(Universe.Universe _universe)
            : base(_universe, _universe.Content.Load<Model>(@"Models/Fork"), new Orange.XNA.Sprite(_universe.Content, @"HealthBars/ForkHealth"))
        {
            // Create the collision for the fork
            physics.Size = new Vector2(150, 50);
            CreateBody();

            // Create the collision for the grabbing
            // Add the sensor for grabbing food
            pierceSensor = new Physics.PhysicsObject(Universe.TheWorld);
            pierceSensor.Size = new Vector2(20, 20);
            pierceSensor.Density = 0;
            pierceSensor.Sensor = true;
            pierceSensor.userData = new DummyFork(Universe, this);
            pierceSensor.ChangeShape(LibCut.Physics.PhysicsObject.Shapes.CIRCLE, null);

            // Rotate the mesh a bit
            Rotation3D = new Vector3((float)Math.PI, 0, 0);

            // Change the position of the health bar
            Health.Position = new Vector2(Health.TheSprite.size.X / 2, Universe.GraphicsDevice.Viewport.Height - (Health.TheSprite.size.Y / 2));
            Health.TheSprite.flip = SpriteEffects.FlipHorizontally;

            // Load the sound
            stab = Universe.Content.Load<SoundEffect>(@"Sound/ForkStab");
        }

        /// <summary>
        /// Update stuff
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            //Rotation3D = new Vector3(Rotation3D.X + 0.01f, Rotation3D.Y, Rotation3D.Z);

            // Update pierce sensor position if we are piercing
            pierceSensor.Position = Position / LibCut.Physics.PhysicsObject.PixelsToMetres;

            if (Piercing && rotationVector.Y > PiercingRotation)
            {
                Rotation3D = new Vector3(rotationVector.X, rotationVector.Y - 0.2f, rotationVector.Z);
                if (rotationVector.Y < PiercingRotation)
                {
                    Rotation3D = new Vector3(rotationVector.X, PiercingRotation, rotationVector.Z);
                }
            }
            else if (!Piercing && rotationVector.Y < 0)
            {
                Rotation3D = new Vector3(rotationVector.X, rotationVector.Y + 0.05f, rotationVector.Z);
                if (rotationVector.Y > 0)
                {
                    Rotation3D = new Vector3(rotationVector.X, 0, rotationVector.Z);
                }
            }
        }

        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);

            // If the universe isn't locked
            if (!Universe.Locked)
            {
                // Make the fork pierce
                if (_input.GetGamePad(1).Triggers.Right > 0.5f && !Piercing)
                {
                    Piercing = true;
                }
                if (_input.GetGamePad(1).Triggers.Right < 0.5f && Piercing)
                {
                    Piercing = false;
                }

                // Rotate the fork
                {
                    UpdateRotation3D = new Vector3(rotationVector.X, rotationVector.Y, rotationVector.Z + (0.2f * _input.GetGamePad(1).ThumbSticks.Right.X));
                }

                Position += _input.GetGamePad(1).ThumbSticks.Left * new Vector2(1, -1) * 9;
            }

            //Position = Universe.Camera.GetWorldCoord(Universe.GraphicsDevice, new Vector2(_input.Mouse.X, _input.Mouse.Y));

            //Position += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left * 8 * new Vector2(1, -1);
            //Rotation3D = new Vector3(rotationVector.X, rotationVector.Y, rotationVector.Z + GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X * 0.2f);
            /*if (GamePad.GetState(PlayerIndex.One).Triggers.Right > 0.5f)
            {
                Piercing = true;
            }a
            else
            {
                Piercing = false;
            }*/
        }

        /// <summary>
        /// Do collisions
        /// </summary>
        /// <param name="_other"></param>
        public override void Collide(object _other)
        {
            base.Collide(_other);
        }

        /// <summary>
        /// The flash to show
        /// </summary>
        Things.Flash.ForkFlash flash;

        /// <summary>
        /// Take damage
        /// </summary>
        /// <param name="_damage"></param>
        public override void TakeDamage(float _damage)
        {
            base.TakeDamage(_damage);
            if (flash != null)
                if (flash.Dead)
                    flash = null;

            if (flash == null)
                flash = new Things.Flash.ForkFlash(Universe, new TimeSpan(0, 0, 1), Depth);
        }
    }

    /// <summary>
    /// A dummy class to use for piercing collisions
    /// </summary>
    public class DummyFork : Things.Thing
    {
        /// <summary>
        /// Pointer to the fork object
        /// </summary>
        Fork fork;
        public Fork Fork
        {
            get
            {
                return fork;
            }
            set
            {
                fork = value;
            }
        }

        /// <summary>
        /// The joint between an object and the fork
        /// </summary>
        Joint theJoint;
        public Joint TheJoint
        {
            get
            {
                return theJoint;
            }
            set
            {
                theJoint = value;
            }
        }

        public DummyFork(Universe.Universe _universe, Fork _fork)
            : base(_universe)
        {
            fork = _fork;
        }
        
        /// <summary>
        /// Update stuff
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Die if the fork died
            if (fork.Dead)
            {
                Dead = true;
            }

            if (TheJoint != null)
            {
                if (fork.Piercing)
                {
                    if (theJoint.GetBodyB().GetFixtureList() == null)
                    {
                        TheJoint = null;
                    }
                }

                if ((!fork.Piercing))
                {
                    // Let the shape know that we aren't holding it anymore
                    if (theJoint.GetBodyB().GetFixtureList() != null)
                    {
                        var shape = theJoint.GetBodyB().GetFixtureList().GetUserData() as Shapes.PhysicsShape;
                        if (shape != null)
                        {
                            shape.Held = false;
                            shape.ThePhysics.body.SetLinearVelocity(shape.LinearVelocity);
                            shape.ThePhysics.body.SetAngularVelocity(shape.AngularVelocity * 20);
                        }
                    }
                    if (Universe.TheWorld.GetJointList() != null)
                    {
                        Universe.TheWorld.DestroyJoint(TheJoint);
                        TheJoint = null;
                    }
                    else if (TheJoint.GetBodyB().GetFixtureList() == null)
                    {
                        TheJoint = null;
                    }
                }
            }

            if (fork.Piercing && TheJoint == null)
            {
                // The body to store the result
                Body shapeBody = null;

                // Loop through all the shapes and find any intersect
                foreach (Thing thing in Universe.Things.ToArray())
                {
                    var shape = thing as Shapes.PhysicsShape;
                    if (shape != null)
                    {
                        if (shape.ThePhysics != null)
                        {
                            if (shape.ThePhysics.body != null)
                            {
                                if (shape.ThePhysics.body.GetFixtureList() != null)
                                {
                                    if (shape.ThePhysics.body.GetFixtureList().TestPoint(fork.Physics.body.GetPosition()))
                                    {
                                        shapeBody = shape.ThePhysics.body;
                                    }
                                }
                            }
                        }
                    }
                }

                // If shape body isn't null there was a body underneath
                if (shapeBody != null)
                {
                    // Create a joint between the collision spot and the food
                    WeldJointDef jointDef = new WeldJointDef();
                    jointDef.bodyA = fork.Physics.body;
                    jointDef.bodyB = shapeBody;
                    jointDef.collideConnected = true;
                    jointDef.localAnchorA = Vector2.Zero;
                    jointDef.localAnchorB = shapeBody.GetLocalPoint(fork.Physics.body.GetPosition());
                    jointDef.referenceAngle = shapeBody.GetAngle() - fork.Physics.body.GetAngle();

                    TheJoint = Universe.TheWorld.CreateJoint(jointDef);

                    // Let the shape know that it's being held
                    var shape = shapeBody.GetFixtureList().GetUserData() as Shapes.PhysicsShape;
                    if (shape != null)
                    {
                        shape.Held = true;
                    }
                    
                    // Play a sound
                    fork.Stab.Play();
                }
            }
        }
    }
}
