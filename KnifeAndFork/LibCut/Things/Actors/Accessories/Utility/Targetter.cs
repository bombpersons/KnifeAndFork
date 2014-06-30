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

namespace LibCut.Things.Actors.Accessories.Utility
{
    public class Targetter : Accessory
    {
        /// <summary>
        /// The time rechoose targets
        /// </summary>
        protected TimeSpan switchTime = new TimeSpan();
        public TimeSpan SwitchTime
        {
            get
            {
                return switchTime;
            }
            set
            {
                switchTime = value;
            }
        }

        /// <summary>
        /// The type to target at the moment
        /// </summary>
        protected Type target;
        public Type Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        /// <summary>
        /// A random number generator to use
        /// </summary>
        protected Random rand = new Random();

        /// <summary>
        /// Creates a new targetter
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_wearer"></param>
        /// <param name="_depth"></param>
        public Targetter(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/Targetter", 50, 50, 0, 0), _depth)
        {
        }

        /// <summary>
        /// Rechoosese a target
        /// </summary>
        public void Retarget(GameTime _gameTime)
        {
            switchTime -= _gameTime.ElapsedGameTime;

            if (switchTime.Ticks <= 0)
            {
                switchTime = new TimeSpan((long)(30 * (10000000) * (float)rand.NextDouble()));

                // Pick a new target
                switch(rand.Next(2))
                {
                    case 0:
                        Target = typeof(Things.Players.Knife);
                        break;
                    case 1:
                        Target = typeof(Things.Players.Fork);
                        break;
                }
            }
        }

        /// <summary>
        /// Update parents target
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(Microsoft.Xna.Framework.GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Retarget
            Retarget(_gameTime);

            Shapes.PhysicsShape shape = (Wearer as Shapes.PhysicsShape);
            if (shape != null)
            {
                // Change the frame depending on whether or not we are being held
                if (shape.Held)
                {
                    sprite.frame.frame = 1;
                }
                else
                {
                    sprite.frame.frame = 0;
                }

                foreach (Things.Thing thing in Universe.Things)
                {
                    if (!shape.Held)
                    {
                        if (thing.GetType() == Target)
                        {
                            if (Vector2.Distance(thing.Position, shape.Position) < shape.Parent.Range)
                            {
                                shape.Parent.Target = thing.Position;
                            }
                            shape.Target = thing.Position;
                        }
                    }
                    else
                    {
                        if (thing is Actor && thing != shape.Parent)
                        {
                            if (Vector2.Distance(thing.Position, shape.Position) < shape.Parent.Range)
                            {
                                shape.Target = thing.Position;
                            }
                            shape.Target = thing.Position;
                        }
                    }
                }
            }
        }
    }
}
