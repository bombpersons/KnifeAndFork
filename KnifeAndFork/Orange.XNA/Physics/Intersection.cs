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

namespace Orange.XNA.Physics
{
    public class Intersection
    {
        /// <summary>
        /// Set up the intersection
        /// </summary>
        /// <param name="_normal"></param>
        /// <param name="_pos"></param>
        public Intersection(Vector2 _normal, Vector2 _pos, Fixture _fixture)
        {
            normal = _normal;
            pos = _pos;
            fixture = _fixture;
        }

        /// <summary>
        /// info
        /// </summary>
        public Vector2 normal, pos;

        /// <summary>
        /// The fixture that is intersected
        /// </summary>
        public Fixture fixture;
    }
}
