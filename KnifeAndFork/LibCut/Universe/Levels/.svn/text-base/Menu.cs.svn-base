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

namespace LibCut.Universe.Levels
{
    public class Menu : Level
    {
        /// <summary>
        /// Create a new menu
        /// </summary>
        /// <param name="_content"></param>
        /// <param name="_graphicsDevice"></param>
        /// <param name="_levelState"></param>
        public Menu(ContentManager _content, GraphicsDevice _graphicsDevice, LevelState.LevelState _levelState)
            : base(_content, _graphicsDevice, _levelState)
        {
        }

        /// <summary>
        /// Reset's the game.
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            // Make a static camera
            Camera = new LibCut.Camera.Camera(this, GraphicsDevice, Vector2.Zero);
            
            EnemySpawners.Add(new Things.EnemySpawner.EnemySpawner(this, typeof(Things.Actors.MenuItems.Tutorial), new Rectangle(-200, (-1 * (GraphicsDevice.Viewport.Height / 2)) + 300, 1, 1), 1));
            EnemySpawners.Add(new Things.EnemySpawner.EnemySpawner(this, typeof(Things.Actors.MenuItems.NewGame), new Rectangle(300, -200, 1, 1), 1));
            EnemySpawners.Add(new Things.EnemySpawner.EnemySpawner(this, typeof(Things.Actors.MenuItems.Hard), new Rectangle(400, 0, 1, 1), 1));
            EnemySpawners.Add(new Things.EnemySpawner.EnemySpawner(this, typeof(Things.Actors.MenuItems.MainMenuHelperGuy), new Rectangle((-1 * (GraphicsDevice.Viewport.Width / 2)), (GraphicsDevice.Viewport.Height / 2) - 200, 1, 1), 1));

            BG = new Things.Background.Background(this, Content.Load<Texture2D>(@"Backgrounds/MenuBG"), 1);
        }
    }
}
