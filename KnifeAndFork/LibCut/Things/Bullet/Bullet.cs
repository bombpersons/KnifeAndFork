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

namespace LibCut.Things.Bullet
{
    public class Bullet : Things.Sprite.Sprite
    {
        /// <summary>
        /// Sets the physics to be where we are
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
                physics.Position = value / LibCut.Physics.PhysicsObject.PixelsToMetres;
            }
        }
        public override float Rotation
        {
            get
            {
               return base.Rotation;
            }
            set
            {
                base.Rotation = value;
                physics.Rotation = value;
            }
        }

        /// <summary>
        /// Whether or not the bullet is harmful to the player
        /// </summary>
        protected bool playerHarmful = true;
        public bool PlayerHarmful
        {
            get
            {
                return playerHarmful;
            }
            set
            {
                playerHarmful = value;
            }
        }

        /// <summary>
        /// Whether or not the bullet is harmful to enemies
        /// </summary>
        protected bool enemyHarmful = true;
        public bool EnemyHarmful
        {
            get
            {
                return enemyHarmful;
            }
            set
            {
                enemyHarmful = value;
            }
        }

        /// <summary>
        /// The direction the bullet is going
        /// </summary>
        protected Vector2 direction;
        public Vector2 Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        /// <summary>
        /// Make sure we destroy the body when it dies
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
                    physics.RemoveShape();
                }
            }
        }

        /// <summary>
        /// After the timer expires the bullet will die
        /// </summary>
        protected TimeSpan timer;
        public TimeSpan Timer
        {
            get
            {
                return timer;
            }
            set
            {
                timer = value;
            }
        }
        
        /// <summary>
        /// The physics object to do collision with
        /// </summary>
        protected Physics.PhysicsObject physics;
        public Physics.PhysicsObject Physics
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
        /// The amount of damage to do
        /// </summary>
        protected float damage;
        public float Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }
        
        /// <summary>
        /// Creates a new bullet
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_sprite"></param>
        /// <param name="_depth"></param>
        public Bullet(Universe.Universe _universe, Orange.XNA.Sprite _sprite, Vector2 _position, Vector2 _direction, float _speed, TimeSpan _timer, Actors.Accessories.Shooters.Shooter _firer, float _damage, int _depth)
            : base(_universe, _sprite, _depth)
        {
            // Start the timer
            Timer = _timer;

            // Create the physics thingy majig
            physics = new Physics.PhysicsObject(Universe.TheWorld);
            physics.Size = new Vector2(10);
            physics.Position = _position;
            physics.userData = new BulletShooterProxy(Universe, _firer, this);
            physics.Density = 1.0f;
            physics.ChangeShape(LibCut.Physics.PhysicsObject.Shapes.CIRCLE, null);

            // Give it a push in the direction
            physics.body.ApplyForce(_direction * _speed, physics.body.GetWorldCenter());

            // Set the position
            Position = _position;

            // Set the damage
            Damage = _damage;
        }

        /// <summary>
        /// Update the position
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Reduce the timer
            Timer -= _gameTime.ElapsedGameTime;
            if (Timer.Ticks < 0)
            {
                Dead = true;
            }

            if (physics != null)
            {
                if (physics.body != null)
                {
                    Transform t = physics.Step();
                    Rotation = t.R.GetAngle();
                    Position = t.Position * LibCut.Physics.PhysicsObject.PixelsToMetres;
                }
            }
        }
    }
}
