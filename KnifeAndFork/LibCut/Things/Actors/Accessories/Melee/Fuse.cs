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

namespace LibCut.Things.Actors.Accessories.Melee
{
    public class Fuse : Accessory
    {
        public Fuse(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/Fuse", 200, 200, 0, 0), _depth)
        {
            // Create the animation
            sprite.AddAnimation("Fuse", 0, 6, 6, false);
            sprite.frame.frame = 0;
        }

        /// <summary>
        /// Blow up the wearer when we get to a certain frame
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(Microsoft.Xna.Framework.GameTime _gameTime)
        {
            base.Update(_gameTime);

            if (Wearer is Shapes.PhysicsShape)
            {
                if (sprite.CurrentAnimation != "Fuse")
                {
                    sprite.frame.frame = 0;
                }

                if (Vector2.Distance(Position, (Wearer as Shapes.PhysicsShape).Target) < (Wearer as Shapes.PhysicsShape).Parent.Range)
                {
                    sprite.SetCurrentAnimation("Fuse");
                }

                if (sprite.frame.frame >= 5)
                {
                    (Wearer as Shapes.PhysicsShape).Explode();
                    Dead = true;
                }
            }
        }
    }
}
