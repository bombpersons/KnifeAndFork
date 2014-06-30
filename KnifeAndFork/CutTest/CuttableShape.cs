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

namespace CutTest
{
    public class CuttableShape
    {
        List<Vector2> Points;

        public CuttableShape()
        {
            Points = new List<Vector2>();
        }

        static public int IntersectLine(Vector2 _line1a, Vector2 _line1b, Vector2 _line2a, Vector2 _line2b, ref Vector2 _out)
        {
            float cross = Cross((_line2b - _line2a), (_line1b - _line1a));
            if (cross == 0)
            {
                // Pararell or colinear
                return 0;
            }

            float t, s;
            t = Cross(_line1a - _line2a, _line2b - _line2a) / cross;
            s = Cross(_line1a - _line2a, _line1b - _line1a) / cross;

            // Check if the lines intersected
            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                _out = _line1a + (_line1b - _line1a) * t;
                return 1;
            }
            else
            {
                // No intersect
                return -1;
            }
        }

        static public float Cross(Vector2 _u, Vector2 _v)
        {
            return (_u.X * _v.Y) - (_v.X * _u.Y);
        }
    }
}
