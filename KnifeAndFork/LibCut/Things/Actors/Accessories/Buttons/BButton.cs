using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCut.Things.Actors.Accessories.Buttons
{
    public class BButton : Accessory
    {
        public BButton(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/BButton", 64, 64, 0, 0), _depth)
        {
            sprite.AddAnimation("Press", 0, 2, 2, true);
            sprite.SetCurrentAnimation("Press");
        }
    }
}
