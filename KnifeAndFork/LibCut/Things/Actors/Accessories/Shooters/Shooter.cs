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

using Orange.XNA;

namespace LibCut.Things.Actors.Accessories.Shooters
{
    public class Shooter : Accessory
    {
        /// <summary>
        /// The range of the shooter
        /// </summary>
        float range = 500.0f;
        public float Range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
            }
        }

        /// <summary>
        /// The amount of time to pause
        /// </summary>
        protected TimeSpan pausedTime = new TimeSpan();
        TimeSpan pauseTime = new TimeSpan(0, 0, 1);
        public TimeSpan PauseTime
        {
            get
            {
                return pauseTime;
            }
            set
            {
                pauseTime = value;
            }
        }

        /// <summary>
        /// The amount of time to shoot for
        /// </summary>
        protected TimeSpan shotTime = new TimeSpan();
        TimeSpan shootTime = new TimeSpan(0, 0, 0, 0, 500);
        public TimeSpan ShootTime
        {
            get
            {
                return shootTime;
            }
            set
            {
                shootTime = value;
            }
        }

        /// <summary>
        /// Whether or not we are shooting
        /// </summary>
        protected bool shooting;

        /// <summary>
        /// How fast to aim the gun
        /// </summary>
        float shootSpeed = 960f;
        public float ShootSpeed
        {
            get
            {
                return shootSpeed;
            }
            set
            {
                shootSpeed = value;
            }
        }

        /// <summary>
        /// Whether or not we have shot on this turn yet
        /// </summary>
        bool shot = false;

        /// <summary>
        /// Whether or not to only shoot once
        /// </summary>
        bool onlyShootOnce = false;
        public bool OnlyShootOnce
        {
            get
            {
                return onlyShootOnce;
            }
            set
            {
                onlyShootOnce = value;
            }
        }

        /// <summary>
        /// The target that we are shooting at
        /// </summary>
        Vector2 target;
        public Vector2 Target
        {
            get
            {
                return target;
            }
            set
            {
                if (target == Vector2.Zero)
                {
                    currentTarget = value;
                }
                target = value;
                if (targetUpdateTimer > targetUpdateSpeed)
                {
                    targetQueue.Enqueue(value);
                    targetUpdateTimer = new TimeSpan();
                }
            }
        }

        /// <summary>
        /// Timer to keep track of when we last updated the queue
        /// </summary>
        protected TimeSpan targetUpdateTimer = new TimeSpan();

        /// <summary>
        /// How fast the target is updated. Targets are put in a queue and when the guns aim is adjusted, 
        /// it interlopererererer whatever it's called between the points.
        /// </summary>
        TimeSpan targetUpdateSpeed = new TimeSpan(0, 0, 0, 0, 500);
        public TimeSpan TargetUpdateSpeed
        {
            get
            {
                return targetUpdateSpeed;
            }
            set
            {
                targetUpdateSpeed = value;
            }
        }

        /// <summary>
        /// A queue to contain all the target points
        /// </summary>
        protected Queue<Vector2> targetQueue = new Queue<Vector2>();

        /// <summary>
        /// The place where we are currently targeting
        /// </summary>
        protected Vector2 currentTarget = Vector2.Zero;

        /// <summary>
        /// True when we begin shooting
        /// </summary>
        protected bool fireAtWill = true;

        /// <summary>
        /// Create a new shooter
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_wearer"></param>
        /// <param name="_sprite"></param>
        /// <param name="_depth"></param>
        public Shooter(Universe.Universe _universe, Thing _wearer, Orange.XNA.Sprite _sprite, int _depth)
            : base(_universe, _wearer, _sprite, _depth)
        {
        }

        /// <summary>
        /// Code that follows the path
        /// </summary>
        /// <param name="_gameTime"></param>
        protected virtual void FollowPath(GameTime _gameTime)
        {
            if (target != null)
            {
                // No point in doing anything if there are no targetQueue points
                if (targetQueue.Count > 0)
                {
                    // Get the next point on the targetQueue
                    Vector2 next = targetQueue.Peek();

                    // Check if we are already at this point
                    if (Vector2.Distance(currentTarget, next) < shootSpeed * _gameTime.ElapsedGameTime.TotalSeconds)
                    {
                        // We are close enough to the position to start moving to the next one
                        // So take the first item in the queue
                        targetQueue.Dequeue();

                        // And grab the next item
                        if (targetQueue.Count > 0)
                            next = targetQueue.Peek();
                    }

                    if (targetQueue.Count > 0)
                    {
                        // Move towards the point
                        // First get the vector between us and that point
                        Vector2 move = next - currentTarget;

                        // Now make this into a unit vector
                        move.Normalize();

                        // Times up by our speed
                        move *= shootSpeed * (float)_gameTime.ElapsedGameTime.TotalSeconds;

                        // Move towards that
                        currentTarget += move;
                    }
                }
                else
                {
                    //currentTarget = target;
                }
            }
        }

        /// <summary>
        /// Function is called when we shoot. Inherit and put some useful code here.
        /// </summary>
        protected virtual void Shoot()
        {
        }

        /// <summary>
        /// Update the shooter
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Updatet the targetqueuetimer
            targetUpdateTimer += _gameTime.ElapsedGameTime;

            // Follow the path if it's there
            FollowPath(_gameTime);

            // Check of we can shoot
            if (Wearer is Shapes.PhysicsShape)
            {
                if ((Wearer as Shapes.PhysicsShape).Held)
                {
                    // Calculate a target straight infront of the shooter
                    Target = Position +
                             new Vector2((float)Math.Cos((Wearer as Shapes.PhysicsShape).ThePhysics.body.GetAngle()),
                                         (float)Math.Sin((Wearer as Shapes.PhysicsShape).ThePhysics.body.GetAngle()));
                    fireAtWill = true;
                }
                else if ((Wearer as Shapes.PhysicsShape).Target == (Wearer as Shapes.PhysicsShape).Parent.Target)
                {
                    fireAtWill = true;
                }
                else
                {
                    fireAtWill = false;
                }
            }

            // Shoot if we can
            if (fireAtWill)
            {
                if (shooting && Vector2.Distance(target, Position) < Range)
                {
                    if ((onlyShootOnce && !shot) || !onlyShootOnce)
                    {
                        Shoot();
                        shot = true;
                    }
                    shotTime += _gameTime.ElapsedGameTime;
                }
                else
                {
                    pausedTime += _gameTime.ElapsedGameTime;
                    shot = false;
                }

                if (shotTime > shootTime)
                {
                    shooting = false;
                    shotTime = new TimeSpan();
                }

                if (pausedTime > pauseTime)
                {
                    shooting = true;
                    pausedTime = new TimeSpan();
                }
            }
          
        }
    }
}
