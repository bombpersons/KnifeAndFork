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

namespace LibCut.Things.Actors.Accessories.Melee
{
    public class Spike : Accessory
    {
        /// <summary>
        /// Make sure the bullet position is in sync with us
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
                bullet.Position = Position;
            }
        }

        /// <summary>
        /// Remove the static bullet afterwards
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
                    bullet.Dead = true;
                    bullet.Physics.RemoveShape();
                }
            }
        }

        /// <summary>
        /// A bullet to make the spike dangerous
        /// </summary>
        Bullet.StaticBullet bullet;

        public Spike(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/Spike", 50, 50, 0, 0), _depth)
        {
            sprite.AddAnimation("Shine", 0, 3, 10, true);
            sprite.SetCurrentAnimation("Shine");

            bullet = new Bullet.StaticBullet(_universe, 0.1f, Position, Wearer, 10, _depth);
            //bullet.EnemyHarmful = false;
        }

        /// <summary>
        /// Do Stuff
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
        }
    }
}