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
    public class Carrot : Actor
    {
        public Carrot(Universe.Universe _universe, Vector2 _pos)
            : base(_universe, _pos)
        {
            LoadFromString("Shape;Food/Carrot;364;0;304;18;-298;40;-356;-5;-316;-45;306;-32;Accessories;LibCut.Things.Actors.Accessories.Thrusters.RocketThruster;-299;-2;LibCut.Things.Actors.Accessories.Eyes.FastEyes;259;7;LibCut.Things.Actors.Accessories.Utility.Targetter;-223;-32;LibCut.Things.Actors.Accessories.Melee.Spike;349;5;");
            SetWorld(Universe.TheWorld);
            Shape.ThePhysics.angularDampening = .9f;

            Shape.Health = 0.3f;
            Shape.Sound = Universe.Content.Load<SoundEffect>(@"Sound/CutCarrot");

            Range = 2000;
        }
    }
}
