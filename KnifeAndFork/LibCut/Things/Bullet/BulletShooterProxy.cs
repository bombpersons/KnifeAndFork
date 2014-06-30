using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCut.Things.Bullet
{
    public class BulletShooterProxy : Things.Thing
    {
        /// <summary>
        /// The bullet
        /// </summary>
        protected Things.Bullet.Bullet bullet;
        public Things.Bullet.Bullet Bullet
        {
            get
            {
                return bullet;
            }
            set
            {
                bullet = value;
            }
        }

        /// <summary>
        /// The shooter
        /// </summary>
        protected Actors.Accessories.Shooters.Shooter shooter;
        public Actors.Accessories.Shooters.Shooter Shooter
        {
            get
            {
                return shooter;
            }
            set
            {
                shooter = value;
            }
        }

        /// <summary>
        /// Creates a new BulletShooterProxy
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_shooter"></param>
        /// <param name="_bullet"></param>
        public BulletShooterProxy(Universe.Universe _universe, Actors.Accessories.Shooters.Shooter _shooter, Things.Bullet.Bullet _bullet)
            : base(_universe)
        {
            Bullet = _bullet;
            Shooter = _shooter;
        }

        /// <summary>
        ///  Make sure we die if the bullet does
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(Microsoft.Xna.Framework.GameTime _gameTime)
        {
            base.Update(_gameTime);
            if (bullet.Dead)
            {
                Dead = true;
            }
        }
    }
}
