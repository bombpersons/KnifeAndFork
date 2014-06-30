using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCut.Things.Actors.Accessories.Mouths
{
    public class GrumpyFace : Accessory
    {
        public GrumpyFace(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/GrumpyFace"), _depth)
        {
        }
    }
}
