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

namespace LibCut.Things.Bullet
{
    public class StaticBullet : Sprite.Sprite
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
        /// The thing that is holding this bullet
        /// </summary>
        protected Thing holder;
        public Thing Holder
        {
            get
            {
                return holder;
            }
            set
            {
                holder = value;
            }
        }

        /// <summary>
        /// Create a static bullet
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_sprite"></param>
        /// <param name="_damage"></param>
        /// <param name="_pos"></param>
        /// <param name="_depth"></param>
        public StaticBullet(Universe.Universe _universe, float _damage, Vector2 _pos, Thing _holder, float _radius, int _depth)
            : base(_universe, null, _depth)
        {
            // Create the physics thingy majig
            physics = new Physics.PhysicsObject(Universe.TheWorld);
            physics.Size = new Vector2(_radius);
            physics.Position = _pos;
            physics.userData = this;
            physics.Density = 1.0f;
            physics.ChangeShape(LibCut.Physics.PhysicsObject.Shapes.CIRCLE, null);

            // Set the position
            Position = _pos;

            // Set the damage
            Damage = _damage;

            // Set the holder
            Holder = _holder;
        } 
    }
}
