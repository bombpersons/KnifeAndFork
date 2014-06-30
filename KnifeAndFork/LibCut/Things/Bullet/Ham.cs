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
    public class Ham : Bullet
    {
        /// <summary>
        /// Creates a new ham bullet
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_position"></param>
        /// <param name="_direction"></param>
        /// <param name="_speed"></param>
        /// <param name="_depth"></param>
        public Ham(Universe.Universe _universe, Vector2 _position, Vector2 _direction, Actors.Accessories.Shooters.Shooter _firer, int _depth)
            : base(_universe, new Orange.XNA.Sprite(_universe.Content, @"Bullets/Ham"), _position, _direction, 20, new TimeSpan(0, 0, 5), _firer, 0.05f, _depth)
        {
            PlayerHarmful = true;
            EnemyHarmful = true;
        }

        /// <summary>
        /// Make the ham rotate
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
            Rotation += 0.1f;
        }
    }
}
