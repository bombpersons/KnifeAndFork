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

namespace LibCut.Things.Actors.Food
{
    public class Pie : Actor
    {
        public Pie(Universe.Universe _universe, Vector2 _pos)
            : base(_universe, _pos)
        {
            LoadFromString("Shape;Food/Pie;122;62.99999;10;108;-102;53;-120;-65;-3;-152;132;-77.99999;Accessories;LibCut.Things.Actors.Accessories.Wings.BigWings;-3;-122;LibCut.Things.Actors.Accessories.Utility.Targetter;-5;-147;LibCut.Things.Actors.Accessories.Thrusters.SimpleThruster;-70.00001;67.99999;LibCut.Things.Actors.Accessories.Eyes.EvenMorePeacefulEyes;10;22;LibCut.Things.Actors.Accessories.Utility.SelfRighter;21.03226;68.63911;");
            SetWorld(Universe.TheWorld);

            Shape.Health = 1.0f;
        }
    }
}
