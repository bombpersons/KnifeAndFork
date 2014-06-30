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
    public class Bread : Actor
    {
        public Bread(Universe.Universe _universe, Vector2 _pos)
            : base(_universe, _pos)
        {
            LoadFromString("Shape;Food/Bread;385;205;-362;210;-378;-220;387;-232;Accessories;LibCut.Things.Actors.Accessories.Mouths.BigTalkingBreadFace;252;37;LibCut.Things.Actors.Accessories.Wings.SmallWings;215.3158;-136.7367;");
            SetWorld(Universe.TheWorld);

            Shape.Health = 1000.0f;
        }
    }
}
