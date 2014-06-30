using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCut.Things.Actors.Accessories.Eyes
{
    public class BlueEyes : Eyes
    {
        public BlueEyes(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/BlueEyes"), _depth)
        {
        }
    }
}
