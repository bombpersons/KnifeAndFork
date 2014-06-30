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
    public class RayLine
    {
        /// <summary>
        /// Start and end of the ray
        /// </summary>
        public Vector2 start, end, unit;

        /// <summary>
        /// The world to use
        /// </summary>
        World world;
        
        /// <summary>
        /// Set up the ray
        /// </summary>
        /// <param name="_start"></param>
        /// <param name="_end"></param>
        public RayLine(Vector2 _start, Vector2 _end, World _world)
        {
            start = _start;
            end = _end;
            world = _world;
            unit = (end - start);
            unit.Normalize();
        }

        /// <summary>
        /// Gets the intersection
        /// </summary>
        /// <param name="_shapes"></param>
        /// <returns></returns>
        public void CalcIntersection()
        {
            world.RayCast(CallBack, start, end);
        }

        /// <summary>
        /// Pointer to the callback function
        /// </summary>
        public RayCastCallback callBack;

        /// <summary>
        /// The intersection to store the results
        /// </summary>
        public Intersection inter;

        /// <summary>
        ///  Check this before checking inter!
        /// </summary>
        public bool calledBack = false;
        
        /// <summary>
        /// Default callback
        /// </summary>
        /// <param name="_fixture"></param>
        /// <param name="_point"></param>
        /// <param name="_normal"></param>
        /// <param name="_fraction"></param>
        /// <returns></returns>
        public float CallBack(Fixture _fixture, Vector2 _point, Vector2 _normal, float _fraction)
        {

            if (inter == null)
            {
                inter = new Intersection(_normal, _point, _fixture);
                calledBack = true;
                return 1.0f;
            }
            else if ((inter.pos - start).Length() > (_point - start).Length())
            {
                inter = new Intersection(_normal, _point, _fixture);
                calledBack = true;
                return 1.0f;
            }

            return _fraction;
        }
    }
}
