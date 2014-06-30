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
    public class Tutorial : Actor
    {
        public Tutorial(Universe.Universe _universe, Vector2 _pos)
            : base(_universe, _pos)
        {
            LoadFromString("Shape;Food/ChickenNugget;219;22;165;91.99999;-140;98.99999;-193;-80;-66.00001;-128;192;-101;Accessories;LibCut.Things.Actors.Accessories.Menu.Tutorial;-158;-67.99999;LibCut.Things.Actors.Accessories.Wings.SmallWings;17;-92.99999;LibCut.Things.Actors.Accessories.Thrusters.SimpleThruster;-151;72;LibCut.Things.Actors.Accessories.Utility.SelfRighter;13;69.99999;");
            SetWorld(Universe.TheWorld);
        }
    }
}
