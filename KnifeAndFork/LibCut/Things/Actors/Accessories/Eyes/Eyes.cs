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

namespace LibCut.Things.Actors.Accessories.Eyes
{
    public class Eyes : Accessory
    {
        public Eyes(Universe.Universe _universe, Thing _wearer, Orange.XNA.Sprite _sprite, int _depth)
            : base(_universe, _wearer, _sprite, _depth)
        {
        }
    }
}
