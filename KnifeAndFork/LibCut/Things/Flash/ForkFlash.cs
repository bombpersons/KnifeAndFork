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

namespace LibCut.Things.Flash
{
    public class ForkFlash : Flash
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
                    GamePad.SetVibration(PlayerIndex.Two, 0.0f, 0.0f);
            }
        }

        public ForkFlash(Universe.Universe _universe, TimeSpan _time, int _depth)
            : base(_universe, new Orange.XNA.Sprite(_universe.Content, @"HealthBars/ForkFlash"), Vector2.Zero, _time, _depth)
        {
            Position = new Vector2(sprite.size.X / 2, sprite.size.Y / 2);
            //GamePad.SetVibration(PlayerIndex.Two, 1.0f, 1.0f);
        }

        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            if (!Dead)
                GamePad.SetVibration(PlayerIndex.Two, Alpha*0.8f, Alpha*0.8f);
        }
    }
}
