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
    public class Ham : Actor
    {
        public Ham(Universe.Universe _universe, Vector2 _pos)
            : base(_universe, _pos)
        {
            LoadFromString("Shape;Food/Ham;157;30;-14;99.99999;-163;33;-116;-122;94.00001;-155;159;-67.99999;Accessories;LibCut.Things.Actors.Accessories.Mouths.ImmaFirinMaLazar;46;-3;LibCut.Things.Actors.Accessories.Shooters.HamThrower;-108;-8;LibCut.Things.Actors.Accessories.Shooters.HamThrower;156;-35;LibCut.Things.Actors.Accessories.Wings.SmallWings;-31;-115;LibCut.Things.Actors.Accessories.Utility.SelfRighter;-61.00001;12;LibCut.Things.Actors.Accessories.Utility.Targetter;-110;-117;LibCut.Things.Actors.Accessories.Thrusters.SimpleThruster;-130;43;");
            SetWorld(Universe.TheWorld);

            Shape.Health = 1.0f;
            Shape.Sound = Universe.Content.Load<SoundEffect>(@"Sound/CutHam");
        }
    }
}
