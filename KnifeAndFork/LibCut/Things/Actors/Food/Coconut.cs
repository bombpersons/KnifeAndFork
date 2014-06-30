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
    public class Coconut : Actor
    {
        public Coconut(Universe.Universe _universe, Vector2 _pos)
            : base(_universe, _pos)
        {
            LoadFromString("Shape;Food/Coconut;210;29;127;99.99999;-18;112;-163;47;-173;-80;-6;-155;174;-121;Accessories;LibCut.Things.Actors.Accessories.Utility.SelfRighter;9.000001;88.99999;LibCut.Things.Actors.Accessories.Thrusters.SimpleThruster;-93.00001;74;LibCut.Things.Actors.Accessories.Mouths.GrumpyFace;12;-18;LibCut.Things.Actors.Accessories.Utility.Shield;-93.00001;17;LibCut.Things.Actors.Accessories.Wings.SmallWings;7;-130;");
            SetWorld(Universe.TheWorld);

            Shape.Health = 1.5f;

            Shape.Effect = typeof(Things.Effects.CoconutMilkCut);
            Shape.Sound = Universe.Content.Load<SoundEffect>(@"Sound/CutCoconut");

            Shape.FailEffect = typeof(Things.Effects.CoconutCut);
            Shape.FailSound = Universe.Content.Load<SoundEffect>(@"Sound/FailCutCoconut");
        }
    }
}
