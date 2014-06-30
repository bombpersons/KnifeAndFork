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

namespace LibCut.Shapes
{
    public class ClockwiseComparer : IComparer<Vector2>
    {
        /// <summary>
        /// The center of the shape, so that we can take angles from it
        /// </summary>
        Vector2 center;
        public Vector2 Center
        {
            get
            {
                return center;
            }
            set
            {
                center = value;
            }
        }

        public int Compare(Vector2 _vec1, Vector2 _vec2)
        {
            double angle1 = Math.Atan2(center.Y - _vec1.Y, center.X - _vec1.X);
            double angle2 = Math.Atan2(center.Y - _vec2.Y, center.X - _vec2.X);

            // Calculate the angle from the center
            if (angle1 > angle2)
            {
                return 1;
            }
            else if (angle1 < angle2)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
