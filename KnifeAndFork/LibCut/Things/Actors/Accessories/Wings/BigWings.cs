using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCut.Things.Actors.Accessories.Wings
{
    public class BigWings : Wings
    {
        public BigWings(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/BigWings", 500, 500, 0, 0), _depth)
        {
            sprite.AddAnimation("Flapping", 0, 3, 2, true);
            sprite.SetCurrentAnimation("Flapping");

            flapForce = 50.0f;
            flapFrame = 1;
        }
    }
}
