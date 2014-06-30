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

namespace LibCut.Things.Actors.Accessories.Eyes
{
    public class PeacefulEyes : Eyes
    {
        public PeacefulEyes(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/PeacefulEyes", 100, 100, 0, 0), _depth)
        {
            sprite.AddAnimation("Blinking", 0, 4, 2, true);
            sprite.SetCurrentAnimation("Blinking");

            sprite.AddAnimation("Struggling", 4, 6, 2, true);
        }

        /// <summary>
        /// Update the animation
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            if (Wearer is Shapes.PhysicsShape)
            {
                if ((Wearer as Shapes.PhysicsShape).Held)
                {
                    sprite.SetCurrentAnimation("Struggling");
                }
                else
                {
                    sprite.SetCurrentAnimation("Blinking");
                }
            }
        }
    }
}
