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

namespace LibCut.Things.PauseScreen
{
    public class Paused : Pause
    {
        public Paused(Universe.Universe _universe)
            : base(_universe, new Orange.XNA.Sprite(_universe.Content, @"PausedScreens/Paused"))
        {
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);

            if (sprite.visible)
            {
                if (_input.ClickedPadButton(Buttons.Back, 0) || _input.ClickedPadButton(Buttons.Back, 1))
                {
                    if (Universe is Universe.Levels.Level)
                    {
                        (Universe as Universe.Levels.Level).LevelState.Level = new Universe.Levels.Menu(Universe.Content, Universe.GraphicsDevice, (Universe as Universe.Levels.Level).LevelState);
                    }
                }
            }
        }
    }
}