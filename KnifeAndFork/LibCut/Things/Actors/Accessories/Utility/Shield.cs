using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCut.Things.Actors.Accessories.Utility
{
    public class Shield : Accessory
    {
        public override bool Dead
        {
            get
            {
                return base.Dead;
            }
            set
            {
                base.Dead = value;
                if (value)
                {
                    if (Wearer is Shapes.PhysicsShape)
                    {
                        (Wearer as Shapes.PhysicsShape).Cuttable = true;
                    }
                }
            }
        }

        public Shield(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/Shield"), _depth)
        {
            // Make the wearer invincable!
            if (Wearer is Shapes.PhysicsShape)
            {
                (Wearer as Shapes.PhysicsShape).Cuttable = false;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime _gameTime)
        {
            base.Update(_gameTime);
        }
    }
}
