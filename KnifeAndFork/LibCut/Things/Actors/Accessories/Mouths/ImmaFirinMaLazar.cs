using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCut.Things.Actors.Accessories.Mouths
{
    public class ImmaFirinMaLazar : Accessory
    {
        public ImmaFirinMaLazar(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/FirinMaLazar", 200, 200, 0, 0), _depth)
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime _gameTime)
        {
            base.Update(_gameTime);

            if (Wearer is Shapes.PhysicsShape)
            {
                if ((Wearer as Shapes.PhysicsShape).Held)
                {
                    sprite.frame.frame = 1;
                }
                else
                {
                    sprite.frame.frame = 0;
                }
            }
        }
    }
}
