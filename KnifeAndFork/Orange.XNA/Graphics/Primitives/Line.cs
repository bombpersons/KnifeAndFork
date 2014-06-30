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

namespace Orange.XNA.Graphics.Primitives
{
    public class Line : Rect
    {
        /// <summary>
        /// Sets up the line
        /// </summary>
        public Line(Vector2 _start, Vector2 _end, float _width)
            : base(new Rectangle((int)_start.X, (int)_start.Y, (int)_width, (int)(_start - _end).Length()))
        {
            // Get the angle between the two points
            Vector2 diff = _end - _start;

            start = _start;
            end = _end;
            unit = diff;
            unit.Normalize();

            rotation = (float)System.Math.Atan2(diff.Y, diff.X) - (float)(System.Math.PI * 0.5f);
            center = new Vector2(0.5f, 0.0f);
        }

        /// <summary>
        /// Start, end and unit vectors for the line
        /// </summary>
        public Vector2 start, end, unit;
    }
}
