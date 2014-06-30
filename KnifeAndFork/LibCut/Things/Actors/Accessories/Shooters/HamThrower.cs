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

namespace LibCut.Things.Actors.Accessories.Shooters
{
    public class HamThrower : Shooter
    {
        public HamThrower(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/HamThrower", 200, 200, 0, 0), _depth)
        {
            sprite.AddAnimation("Throwing", 0, 5, 4, true);
            sprite.SetCurrentAnimation("Throwing");

            // This shooter only shoots once
            OnlyShootOnce = true;
        }

        /// <summary>
        /// Called when we shoot!
        /// </summary>
        protected override void Shoot()
        {
            base.Shoot();
            
            // Get the direction to shoot
            Vector2 diff = (Target - Position);
            diff.Normalize();

            var ham = new Things.Bullet.Ham(Universe, Position, diff, this, Depth);
        }
    }
}
