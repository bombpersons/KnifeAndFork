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
    public class Player : Model3D.Model3D
    {
        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                if (Universe.Camera is Camera.FitOnScreenCamera || Universe.Camera is Camera.DebugCamera)
                {
                    base.Position = value;
                    physics.Position = value / LibCut.Physics.PhysicsObject.PixelsToMetres;
                }
                else
                {
                    if (Universe.GraphicsDevice.Viewport.Bounds.Contains((int)Universe.Camera.GetScreenCoord(Universe.GraphicsDevice, value).X, (int)Universe.Camera.GetScreenCoord(Universe.GraphicsDevice, value).Y))
                    {
                        base.Position = value;
                        physics.Position = value / LibCut.Physics.PhysicsObject.PixelsToMetres;
                    }
                }
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
                physics.Rotation = value;
                base.Rotation = value;
            }
        }
        public override Vector2 Scale
        {
            get
            {
                return base.Scale;
            }
            set
            {
                base.Scale = value;
            }
        }
        public override bool Dead
        {
            get
            {
                return base.Dead;
            }
            set
            {
                base.Dead = value;
                if (Dead)
                {
                    physics.RemoveShape();
                }
            }
        }

        /// <summary>
        /// The health bar for this player
        /// </summary>
        protected Things.Health.HealthBar health;
        public Things.Health.HealthBar Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        /// <summary>
        /// Creates a new player
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_model"></param>
        public Player(Universe.Universe _universe, Model _model, Orange.XNA.Sprite _healthBar)
            : base(_universe, _model)
        {
            health = new Things.Health.HealthBar(Universe, _healthBar);

            // Make sure we don't get garbage cleaned 
            GarbageCleaned = false;
        }

        /// <summary>
        /// Collision with enemies, and bullets
        /// </summary>
        /// <param name="_other"></param>
        public override void Collide(object _other)
        {
            base.Collide(_other);
            
            // Check for any bullets hitting us
            if (_other as Things.Bullet.BulletShooterProxy != null)
            {
                if ((_other as Things.Bullet.BulletShooterProxy).Bullet.PlayerHarmful)
                {
                    if (!((_other as Things.Bullet.BulletShooterProxy).Shooter.Wearer as Shapes.PhysicsShape).Held)
                    {
                        TakeDamage((_other as Things.Bullet.BulletShooterProxy).Bullet.Damage);
                    }
                }
            }

            else if (_other is Things.Bullet.StaticBullet)
            {
                if ((_other as Things.Bullet.StaticBullet).PlayerHarmful)
                {
                    TakeDamage((_other as Things.Bullet.StaticBullet).Damage);
                }
            }
        }

        /// <summary>
        /// When we take damage
        /// </summary>
        /// <param name="_damage"></param>
        public virtual void TakeDamage(float _damage)
        {
            Health.Health -= _damage;
        }
    }
}
