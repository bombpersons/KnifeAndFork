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

namespace LibCut.Things.GarbageCleaner
{
    public class GarbageCleaner : Thing
    {
        /// <summary>
        /// The range at which to remove things
        /// </summary>
        protected Rectangle range;
        public Rectangle Range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
            }
        }

        /// <summary>
        /// Creates a new garbage cleaner
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_range"></param>
        public GarbageCleaner(Universe.Universe _universe, Rectangle _range)
            : base(_universe)
        {
            Range = _range;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Kill anything that goes out of the range
            foreach (Thing thing in Universe.Things)
            {
                if (thing.GarbageCleaned)
                {
                    if (!Range.Contains((int)thing.Position.X, (int)thing.Position.Y))
                    {
                        thing.Dead = true;
                    }
                }

                if (thing is Shapes.PhysicsShape)
                {
                    if (!thing.Dead)
                    {
                        if ((thing as Shapes.PhysicsShape).ThePhysics != null)
                        {
                            if ((thing as Shapes.PhysicsShape).ThePhysics.body != null)
                            {
                                // Check how much mass the shape has. If it has a small mass then just remove it
                                if ((thing as Shapes.PhysicsShape).ThePhysics.body.GetMass() < 1f)
                                {
                                    thing.Dead = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

