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
    public class CupCake : Actor
    {
        /// <summary>
        /// Creates a new suicide cupcake
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_pos"></param>
        public CupCake(Universe.Universe _universe, Vector2 _pos)
            : base(_universe, _pos)
        {
            LoadFromString("Shape;Food/CupCake;160;110;-76.00001;125;-163;37;-180;-92.99999;22;-173;152;-133;215;-62.99999;Accessories;LibCut.Things.Actors.Accessories.Eyes.BlueEyes;22;55;LibCut.Things.Actors.Accessories.Melee.Fuse;29;-145;LibCut.Things.Actors.Accessories.Thrusters.SimpleThruster;-138;60;LibCut.Things.Actors.Accessories.Utility.Targetter;-95.00001;-120;LibCut.Things.Actors.Accessories.Thrusters.SimpleThruster;-123;81.99999;LibCut.Things.Actors.Accessories.Thrusters.SimpleThruster;-153;35;");
            SetWorld(Universe.TheWorld);

            Range = 600f;
        }
    }
}
