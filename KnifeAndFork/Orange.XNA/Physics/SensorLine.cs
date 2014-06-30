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

namespace Orange.XNA.Physics
{
    public class SensorLine : Orange.XNA.Graphics.Primitives.Line
    {
        /// <summary>
        /// Constructs the line
        /// </summary>
        /// <param name="_start"></param>
        /// <param name="_end"></param>
        /// <param name="_width"></param>
        /// <param name="_graphicsDevice"></param>
        public SensorLine(Vector2 _start, Vector2 _end, float _width, Box2D.XNA.World _world)
            : base(_start, _end, _width) 
        {
            // Set up the physics as a sensor
            sensor = true;
            world = _world;
            ChangeShape(Shapes.BOX);
        }
    }
}
