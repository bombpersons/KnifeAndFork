using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCut.Things.Actors.Accessories.Eyes
{
    public class FastEyes : Eyes
    {
        public FastEyes(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/FastEyes"), _depth)
        {
        }
    }
}
