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

namespace LibCut.Things.Actors.Accessories.Menu
{
    public class NewGame : MenuItem
    {
        /// <summary>
        /// Creates a new tutorial button accessory thingy majig
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_wearer"></param>
        /// <param name="_depth"></param>
        public NewGame(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, "Easy.", null, _depth)
        {
            Func = StartTutorial;
            text.Color = Color.White;
        }

        /// <summary>
        /// Starts the tutorial
        /// </summary>
        void StartTutorial()
        {
            (Wearer.Universe as Universe.Levels.Level).LevelState.Level = new Universe.Levels.Level1(Universe.Content, Universe.GraphicsDevice, (Universe as Universe.Levels.Level).LevelState);
        }
    }
}
