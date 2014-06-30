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
    public class ChickenNugget : Actor
    {
        /// <summary>
        /// Creates a new chickennuget
        /// </summary>
        /// <param name="_pos"></param>
        /// <param name="_content"></param>
        public ChickenNugget(Universe.Universe _universe, Vector2 _pos)
            : base(_universe, _pos)
        {
            // Load a chicken nugget
            LoadFromString("Shape;Food/ChickenNugget;144;0;11;53;-123;40;-106;-99.99999;41;-140;136;-97.99999;Accessories;LibCut.Things.Actors.Accessories.Eyes.PeacefulEyes;74;-30;LibCut.Things.Actors.Accessories.Wings.SmallWings;-43;-76.99999;LibCut.Things.Actors.Accessories.Utility.SelfRighter;-61.00001;12;");
            SetWorld(Universe.TheWorld);

            // Set the health
            Shape.Health = 0.4f;
            
            // Set the sound
            Shape.Sound = Universe.Content.Load<SoundEffect>(@"Sound/Cut");
        }
    }
}