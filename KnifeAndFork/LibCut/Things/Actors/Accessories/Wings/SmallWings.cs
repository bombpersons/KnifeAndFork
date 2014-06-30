using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCut.Things.Actors.Accessories.Wings
{
    public class SmallWings : Accessories.Wings.Wings
    {
        /// <summary>
        /// Create a new instance of wings
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_wearer"></param>
        public SmallWings(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/Wings", 100, 100, 0, 0), _depth)
        {
            sprite.AddAnimation("Flapping", 0, 5, 10, true);
            sprite.SetCurrentAnimation("Flapping");

            flapForce = 75.0f;
            flapFrame = 4;
        }

    }
}
