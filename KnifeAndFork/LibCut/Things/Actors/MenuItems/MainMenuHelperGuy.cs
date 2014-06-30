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
    public class MainMenuHelperGuy : Actor
    {
        public MainMenuHelperGuy(Universe.Universe _universe, Vector2 _pos)
            : base(_universe, _pos)
        {
            LoadFromString("Shape;Food/Carrot;148;52;-237;76.99999;-278;-61.99999;212;-43;255;2;Accessories;LibCut.Things.Actors.Accessories.Mouths.MainMenuFace;140;10;LibCut.Things.Actors.Accessories.Thrusters.SimpleThruster;-190;47;LibCut.Things.Actors.Accessories.Wings.SmallWings;-137;-45;LibCut.Things.Actors.Accessories.Utility.SelfRighter;13;10.99999;");
            SetWorld(Universe.TheWorld);

            Shape.Sound = Universe.Content.Load<SoundEffect>(@"Sound/CutCarrot");
        }
    }
}
