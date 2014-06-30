using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCut.Things.Actors.Accessories.Eyes
{
    public class EvenMorePeacefulEyes : Eyes
    {
        public EvenMorePeacefulEyes(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/EvenMorePeacefulEyes", 200, 200, 0, 0), _depth)
        {
            sprite.AddAnimation("Blinking", 0, 4, 2, true);
            sprite.SetCurrentAnimation("Blinking");

            //sprite.AddAnimation("!", 4, 4, 60, false);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime _gameTime)
        {
            base.Update(_gameTime);

            if (Wearer is Shapes.PhysicsShape)
            {
                if ((Wearer as Shapes.PhysicsShape).Held)
                {
                    //sprite.SetCurrentAnimation("!");
                    sprite.frame.frame = 4;
                }
                else
                {
                    sprite.SetCurrentAnimation("Blinking");
                }
            }
        }
    }
}
