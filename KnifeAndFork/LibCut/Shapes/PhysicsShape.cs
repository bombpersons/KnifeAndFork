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

namespace LibCut.Shapes
{
    /// <summary>
    /// A shape that uses box2d to do physics and stuff =0
    /// </summary>
    public class PhysicsShape : Shape
    {
        /// <summary>
        /// The parent
        /// </summary>
        protected Things.Actors.Actor parent;
        public Things.Actors.Actor Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        /// <summary>
        /// Whether or not the shape is being held by the fork
        /// </summary>
        protected bool held;
        public bool Held
        {
            get
            {
                return held;
            }
            set
            {
                held = value;
            }
        }

        /// <summary>
        /// Kill the shape
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
                    DestroyBody();
                }
            }
        }

        /// <summary>
        /// The health of the shape
        /// </summary>
        protected float health = 1f;
        public float Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;

                // If health goes below 0, then destroy all the accessories on this object
                if (health < 0)
                {
                    foreach (Things.Actors.Accessories.Accessory acc in Accessories.ToArray())
                    {
                        acc.Dead = true;
                        Accessories.Remove(acc);
                    }
                }
            }
        }

        /// <summary>
        /// The target of this shape
        /// </summary>
        protected Vector2 target;
        public Vector2 Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        /// <summary>
        /// The power of a cut
        /// </summary>
        static float cutPower = 2;
        static public float CutPower
        {
            get
            {
                return cutPower;
            }
            set
            {
                cutPower = value;
            }
        }

        // The cut points
        protected Vector2[] cut = new Vector2[2];

        /// <summary>
        /// The physics object to use
        /// </summary>
        protected Physics.PhysicsObject physics;
        public Physics.PhysicsObject ThePhysics
        {
            get
            {
                return physics;
            }
            set
            {
                physics = value;
            }
        }

        /// <summary>
        /// Makes sure to set this before you call changeshape
        /// </summary>
        public World TheWorld
        {
            get
            {
                return physics.TheWorld;
            }
            set
            {
                physics.TheWorld = value;
            }
        }

        /// <summary>
        /// Sets the world
        /// </summary>
        /// <param name="_world"></param>
        public bool SetWorld(World _world)
        {
            physics = new Physics.PhysicsObject(_world);
            physics.Density = 1.0f;
            physics.linearDampening = 0.4f;
            physics.angularDampening = 0.4f;
            physics.friction = 2f;
            physics.Position = Position;
            physics.Rotation = Rotation;
            physics.userData = this;
            return CreateBody();
        }

        /// <summary>
        /// A random variable to use
        /// </summary>
        static protected Random rand = new Random();

        /// <summary>
        /// Creates a blank shape
        /// </summary>
        public PhysicsShape(Universe.Universe _universe)
            : base(_universe)
        {
        }

        /// <summary>
        /// Creates a shape with depth
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_depth"></param>
        public PhysicsShape(Universe.Universe _universe, int _depth)
            : base(_universe, _depth)
        {

        }

        /// <summary>
        /// Creates a physics shape from an existing shape
        /// </summary>
        /// <param name="_shape"></param>
        public PhysicsShape(Universe.Universe _universe, Shape _shape)
            : base(_universe, _shape.Depth)
        {
            Points = _shape.Points.ToList();
            Tex = _shape.Tex;
            TextureCenter = _shape.TextureCenter;
            Size = _shape.Size;
            Shader = _shape.Shader;
            Position = _shape.Position;
            Rotation = _shape.Rotation;
            Sound = _shape.Sound;
            Effect = _shape.Effect;
            FailSound = _shape.FailSound;
            FailEffect = _shape.FailEffect;
            ExplodeSound = _shape.ExplodeSound;

            // Copy over the accessories
            for (int i = 0; i < _shape.Accessories.Count; i++)
            {
                _shape.Accessories[i].Wearer = this;
                Accessories.Add(_shape.Accessories[i]);
            }
        }

        /// <summary>
        /// Creates a body from the current points
        /// </summary>
        public bool CreateBody()
        {
            // First make sure we are sorted
            Sort();

            // Now create the body 
            if (Points.Count() > 2)
            {
                return physics.ChangeShape(Physics.PhysicsObject.Shapes.POLYGON, Points.ToArray());
            }

            return false;
        }

        /// <summary>
        /// Destroys a body
        /// </summary>
        public void DestroyBody()
        {
            physics.RemoveShape();
        }

        /// <summary>
        /// Creates 2 new shapes given a cut vector
        /// </summary>
        /// <param name="_pos1"></param>
        /// <param name="_pos2"></param>
        /// <returns></returns>
        public new PhysicsShape[] Cut(Vector2 _pos1, Vector2 _pos2)
        {
            PhysicsShape[] shapes = new PhysicsShape[2];
            Shape[] baseShapes = null;
            if (Cuttable)
                baseShapes = base.Cut(_pos1, _pos2);

            // Calculate the normal to the cut
            Vector2 normal = (_pos1 - _pos2);
            normal = new Vector2(normal.Y, -normal.X);
            normal.Normalize();

            if (baseShapes != null)
            {
                for (int i = 0; i < shapes.Count(); i++)
                {
                    shapes[i] = new PhysicsShape(Universe, baseShapes[i]);
                    shapes[i].Parent = Parent;

                    // Kill of the baseshape
                    baseShapes[i].Dead = true;

                    // Figure out the mass of this shape, if it is less than the limit, then return null
                    if (!shapes[i].SetWorld(TheWorld))
                    {
                        Dead = false;

                        // Make sure both base shapes are dead
                        baseShapes[0].Dead = true;
                        baseShapes[1].Dead = true;

                        // Kill the others shapes too
                        //shapes[0].Dead = true;
                        //shapes[1].Dead = true;

                        return null;
                    }

                    if (ThePhysics.body != null)
                    {
                        // Make sure that the shapes created have the same velocity
                        shapes[i].ThePhysics.body.SetAngularVelocity(ThePhysics.body.GetAngularVelocity());
                        shapes[i].ThePhysics.body.SetLinearVelocity(ThePhysics.body.GetLinearVelocity());
                    }

                    normal *= -1;

                    // Apply a force to make the cut look like it did something
                    shapes[i].physics.body.ApplyForce(normal * CutPower, (_pos1 + _pos2) / 2);

                    // Show the cut effect
                    // Get the middle of the cut
                    Vector2 middle = (_pos1 + _pos2) / 2;

                    // Get the rotation of the cut
                    float rot = (float)Math.Atan2((_pos1 - _pos2).Y, (_pos1 - _pos2).X);

                    // Create the cut effect
                    var cut = System.Activator.CreateInstance(Effect, new Object[] { Universe, middle, rot, Depth });

                    // Play the cut sound
                    if (Sound != null)
                        Sound.Play();
                }

                // Remove ourselves
                Dead = true;
            }
            else
            {
                if (FailEffect != null)
                {
                    // Show the cut effect
                    // Get the middle of the cut
                    Vector2 middle = (_pos1 + _pos2) / 2;

                    // Get the rotation of the cut
                    float rot = (float)Math.Atan2((_pos1 - _pos2).Y, (_pos1 - _pos2).X);

                    // Create the cut effect
                    var cut = System.Activator.CreateInstance(FailEffect, new Object[] { Universe, middle, rot, Depth });
                }


                // Play the cut sound
                if (FailSound != null)
                    FailSound.Play();
            }

            return shapes;
        }

        /// <summary>
        /// Step the shape
        /// </summary>
        public override void Update(GameTime _gameTime)
        {
            Escapable = false;

            base.Update(_gameTime);

            // Reduce the red alpha
            if (RedAlpha > 0)
            {
                RedAlpha -= 1 * (float)_gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (physics != null)
            {
                if (physics.body != null)
                {
                    Transform t = physics.Step();
                    Rotation = t.R.GetAngle();
                    Position = t.Position * Physics.PhysicsObject.PixelsToMetres;
                }
            }

            // Send any shooter accessory the new target
            foreach (Things.Actors.Accessories.Accessory acc in Accessories)
            {
                if (acc as Things.Actors.Accessories.Shooters.Shooter != null)
                {
                    (acc as Things.Actors.Accessories.Shooters.Shooter).Target = Parent.Target;
                }
            }
        }

        /// <summary>
        /// Take damage from bullets if neccessarily
        /// </summary>
        /// <param name="_other"></param>
        public override void Collide(object _other)
        {
            base.Collide(_other);

            // Start a cut if the colliding object is the knife
            if (_other is Things.Players.CutDummy)
            {
                if ((_other as Things.Players.CutDummy).Knife.Cutting)
                {
                    cut[0] = ((_other as Things.Players.CutDummy).Position);
                    cut[0] += new Vector2((float)Math.Cos((_other as Things.Players.CutDummy).Knife.Rotation), (float)Math.Sin((_other as Things.Players.CutDummy).Knife.Rotation)) * -110;
                }
            }

            if (_other is Things.Bullet.BulletShooterProxy)
            {
                if ((_other as Things.Bullet.BulletShooterProxy).Bullet.EnemyHarmful &&
                   ((_other as Things.Bullet.BulletShooterProxy).Shooter.Wearer as Shapes.PhysicsShape) != this)
                {
                    TakeDamage((_other as Things.Bullet.BulletShooterProxy).Bullet.Damage);
                }
            }

            else if (_other is Things.Bullet.StaticBullet)
            {
                if ((_other as Things.Bullet.StaticBullet).EnemyHarmful &&
                    (_other as Things.Bullet.StaticBullet).Holder != this)
                {
                    TakeDamage((_other as Things.Bullet.StaticBullet).Damage);
                }
            }
        }

        /// <summary>
        /// Do cuts
        /// </summary>
        /// <param name="_other"></param>
        public override void UnCollide(object _other)
        {
            base.UnCollide(_other);

            // Do the cut on uncollide
            if (_other is Things.Players.CutDummy)
            {
                if ((_other as Things.Players.CutDummy).Knife.Cutting)
                {
                    cut[1] = (_other as Things.Players.CutDummy).Position;
                    cut[1] += new Vector2((float)Math.Cos((_other as Things.Players.CutDummy).Knife.Rotation), (float)Math.Sin((_other as Things.Players.CutDummy).Knife.Rotation)) * 110;

                    Cut(cut[0], cut[1]);
                }
            }
        }

        /// <summary>
        /// When we take damage
        /// </summary>
        /// <param name="_damage"></param>
        public virtual void TakeDamage(float _damage)
        {
            Health -= _damage;

            redAlpha = 1.0f;
        }

        /// <summary>
        /// Makes the shape explode
        /// </summary>
        public void Explode()
        {
            // Make sure we have a world
            if (TheWorld == null)
                return;

            // A list to store all the cut shapes
            List<PhysicsShape> shapes = new List<PhysicsShape>();
            shapes.Add(this);

            // Do some random cuts
            for (int i = 0; i <= 2; i++)
            {
                // Pick a random angle
                float angle = (float)rand.NextDouble() * (float)Math.PI * 2;

                // Generate 2 points to cut with
                Vector2 pos1 = Position + new Vector2((float)Math.Cos(angle)*99999, (float)Math.Sin(angle)*99999);
                Vector2 pos2 = Position - new Vector2((float)Math.Cos(angle)*99999, (float)Math.Sin(angle)*99999);

                // Cut through everything in the list
                foreach (PhysicsShape s in shapes.ToArray())
                {
                    PhysicsShape[] cuts = s.Cut(pos1, pos2);
                    if (cuts != null)
                    {
                        shapes.Remove(s);

                        if (cuts[0] != null)
                            shapes.Add(cuts[0]);

                        if (cuts[1] != null)
                            shapes.Add(cuts[1]);
                    }
                }
            }

            // Now propel all the shapes out with a force
            foreach (PhysicsShape s in shapes)
            {
                // Find out the vector from the center to this shape
                Vector2 diff = s.ThePhysics.body.GetWorldCenter() - (Position / Physics.PhysicsObject.PixelsToMetres);
                diff.Normalize();

                // Now propel it in that direction
                s.ThePhysics.body.ApplyForce(diff * 20000, s.ThePhysics.body.GetWorldCenter());
            }

            // Play the explode sound
            if (ExplodeSound != null)
                ExplodeSound.Play();

            // Create a explosion bullet
            var explosion = new Things.Bullet.Explosion(Universe, 0.001f, Position, this, Depth);
        }
    }
}
