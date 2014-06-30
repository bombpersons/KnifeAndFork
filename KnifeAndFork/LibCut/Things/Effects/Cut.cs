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

namespace LibCut.Things.Effects
{
    public class Cut : Effect
    {
        /// <summary>
        /// Create the cut effect
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_pos"></param>
        /// <param name="_rot"></param>
        /// <param name="_depth"></param>
        public Cut(Universe.Universe _universe, Vector2 _pos, float _rot, int _depth)
            : base(_universe, new Orange.XNA.Sprite(_universe.Content, @"Effects/Cut", 500, 70, 0, 0), _pos, 7, _depth)
        {
            Rotation = _rot;

            // Set up the animation
            sprite.AddAnimation("Cut", 0, 7, 10, false);
            sprite.SetCurrentAnimation("Cut");
        }
    }
}
