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

namespace LibCut.Things.Actors.MenuItems
{
    public class Hard : Actor
    {
        public Hard(Universe.Universe _universe, Vector2 _pos)
            : base(_universe, _pos)
        {
            LoadFromString("Shape;Food/Pie;122;96.99999;12;142;-133;127;-213;52;-180;-61;-5;-120;88.00001;-95;148;-8;Accessories;LibCut.Things.Actors.Accessories.Menu.Hard;-180;-40;LibCut.Things.Actors.Accessories.Wings.SmallWings;-37;-77.99999;LibCut.Things.Actors.Accessories.Thrusters.SimpleThruster;-128;113;LibCut.Things.Actors.Accessories.Utility.SelfRighter;13;69.99999;");
            SetWorld(Universe.TheWorld);
        }
    }
}
