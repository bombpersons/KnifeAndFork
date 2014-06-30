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

namespace LibCut.Things.Health
{
    public class CollectionProgress : HealthBar
    {
        /// <summary>
        /// Make sure the icon gets put at the top
        /// </summary>
        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                Icon.Position = value - new Vector2(0, (sprite.size.X / 2) + ((Icon.TheSprite.size.Y*Icon.TheSprite.scale.Y) / 2) + 5);
            }
        }

        /// <summary>
        /// The required mass
        /// </summary>
        protected float requiredMass;
        public float RequiredMass
        {
            get
            {
                return requiredMass;
            }
            set
            {
                requiredMass = value;
            }
        }

        /// <summary>
        /// The plate to use
        /// </summary>
        protected Plate.Plate plate;
        public Plate.Plate Plate
        {
            get
            {
                return plate;
            }
            set
            {
                plate = value;
            }
        }

        /// <summary>
        /// Make sure the icon sprite dies with us
        /// </summary>
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
                    Icon.Dead = true;
                }
            }
        }

        /// <summary>
        /// The type to check for
        /// </summary>
        protected Type type;
        public Type Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        /// <summary>
        /// The icon to display above the meter.
        /// </summary>
        protected Sprite.Sprite icon;
        public Sprite.Sprite Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
            }
        }

        /// <summary>
        /// Creates a new collection progress bar
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_plate"></param>
        /// <param name="_sprite"></param>
        /// <param name="_depth"></param>
        public CollectionProgress(Universe.Universe _universe, Plate.Plate _plate, Type _type, Orange.XNA.Sprite _sprite, Orange.XNA.Sprite _icon, float _requiredMass,int _depth)
            : base(_universe, _sprite)
        {
            type = _type;
            requiredMass = _requiredMass;
            plate = _plate;

            // Rotate the sprite 90 degrees
            sprite.rotation = -(float)Math.PI / 2;

            // Create the icon
            Icon = new Sprite.StaticSprite(Universe, _icon, Depth);
            Icon.Scale = new Vector2(0.2f);
        }

        /// <summary>
        /// Update the progress
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Check the mass in the plate
            Health = 1 - (Plate.GetMass(Type) / RequiredMass);

            // Reset the color
            sprite.tint = Color.White;
        }
    }
}
