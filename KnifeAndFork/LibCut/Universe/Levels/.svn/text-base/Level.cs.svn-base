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
    public class Level : Universe
    {
        /// <summary>
        /// The list of enemy spawners
        /// </summary>
        protected List<Things.EnemySpawner.EnemySpawner> EnemySpawners = new List<Things.EnemySpawner.EnemySpawner>();

        /// <summary>
        /// The plate on this level
        /// </summary>
        protected Things.Plate.Plate plate;
        public Things.Plate.Plate Plate
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
        /// The background to the level
        /// </summary>
        protected Things.Background.Background bg;
        public Things.Background.Background BG
        {
            get
            {
                return bg;
            }
            set
            {
                bg = value;
            }
        }

        /// <summary>
        /// The splash image
        /// </summary>
        protected Orange.XNA.Sprite splash;
        public Orange.XNA.Sprite Splash
        {
            get
            {
                return splash;
            }
        }
        
        /// <summary>
        /// The fork player
        /// </summary>
        protected Things.Players.Fork fork;
        public Things.Players.Fork Fork
        {
            get
            {
                return fork;
            }
            set
            {
                fork = value;
            }
        }

        /// <summary>
        /// The knife player
        /// </summary>
        protected Things.Players.Knife knife;
        public Things.Players.Knife Knife
        {
            get
            {
                return knife;
            }
            set
            {
                knife = value;
            }
        }

        /// <summary>
        /// The Level controller
        /// </summary>
        protected Things.LevelController.LevelController levelController;
        public Things.LevelController.LevelController LevelController
        {
            get
            {
                return levelController;
            }
            set
            {
                levelController = value;
            }
        }

        /// <summary>
        /// The gamestate controlling the level
        /// </summary>
        protected LevelState.LevelState levelState;
        public LevelState.LevelState LevelState
        {
            get
            {
                return levelState;
            }
            set
            {
                levelState = value;
            }
        }

        /// <summary>
        /// Creates a level.
        /// </summary>
        /// <param name="_content"></param>
        /// <param name="_graphicsDevice"></param>
        public Level(ContentManager _content, GraphicsDevice _graphicsDevice, LevelState.LevelState _levelState)
            : base(_content, _graphicsDevice)
        {
            LevelState = _levelState;
        }

        /// <summary>
        /// Resets the level
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            // Remove all the enemy spawners
            EnemySpawners.Clear();

            // Add a garbage cleaner
            var garbageCollector = new Things.GarbageCleaner.GarbageCleaner(this, new Rectangle(-5000, -5000, 10000, 10000));

            // Add the players
            Knife = new LibCut.Things.Players.Knife(this);
            Fork = new LibCut.Things.Players.Fork(this);

            // Add a camera to follow the players
            Camera = new LibCut.Camera.FitOnScreenCamera(this, Vector2.Zero, new LibCut.Things.Thing[] { knife, fork });

            // Add a pause screen
            var pause = new LibCut.Things.PauseScreen.Paused(this);
        }
    }
}
