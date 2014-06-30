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

namespace KnifeAndFork.Game
{
    public class GameUniverse : LibCut.Universe.Levels.Level
    {
        /// <summary>
        /// Creates a new game universe
        /// </summary>
        /// <param name="_content"></param>
        /// <param name="_graphicsDevice"></param>
        public GameUniverse(ContentManager _content, GraphicsDevice _graphicsDevice)
            : base(_content, _graphicsDevice)
        {
        }

        /// <summary>
        /// Resets the level
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            EnemySpawners.Add(new LibCut.Things.EnemySpawner.EnemySpawner(this, typeof(LibCut.Things.Actors.Food.ChickenNugget), new Rectangle(-1000, -1000, 2000, 2000), 5));
            EnemySpawners.Add(new LibCut.Things.EnemySpawner.EnemySpawner(this, typeof(LibCut.Things.Actors.Food.Ham), new Rectangle(-3000, -3000, 3000, 3000), 3));
            EnemySpawners.Add(new LibCut.Things.EnemySpawner.EnemySpawner(this, typeof(LibCut.Things.Actors.Food.Coconut), new Rectangle(1000, 1000, 2000, 2000), 1));
            EnemySpawners.Add(new LibCut.Things.EnemySpawner.EnemySpawner(this, typeof(LibCut.Things.Actors.Food.Carrot), new Rectangle(-2000, -2000, 4000, 4000), 1));

            BG = bg = new LibCut.Things.Background.Background(this, new Orange.XNA.Sprite(Content, @"Backgrounds/Level1BG"));
            Plate = plate = new LibCut.Things.Plate.Plate(this,
                                                               new Orange.XNA.Sprite(content, @"Backgrounds/Plate"),
                                                               bg.Depth + 1);
            plate.Position = new Vector2(0, 400);

            plate.ProgressBars.Add(new LibCut.Things.Health.CollectionProgress(this, plate, typeof(LibCut.Things.Actors.Food.ChickenNugget), new Orange.XNA.Sprite(Content, @"HealthBars/ChickenCollected"), new Orange.XNA.Sprite(Content, @"Food/ChickenNugget"), 30, plate.Depth + 1));
            plate.ProgressBars.Add(new LibCut.Things.Health.CollectionProgress(this, plate, typeof(LibCut.Things.Actors.Food.Ham), new Orange.XNA.Sprite(Content, @"HealthBars/HamCollected"), new Orange.XNA.Sprite(Content, @"Food/Ham"), 30, plate.Depth + 1));
            plate.ProgressBars.Add(new LibCut.Things.Health.CollectionProgress(this, plate, typeof(LibCut.Things.Actors.Food.Coconut), new Orange.XNA.Sprite(Content, @"HealthBars/CoconutCollected"), new Orange.XNA.Sprite(Content, @"Food/Coconut"), 30, plate.Depth + 1));

            splash = new Orange.XNA.Sprite(Content, @"Splash/WinLevel");

            var levelcontrol = new LibCut.Things.LevelController.LevelController(this);
        }

        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);
        }

        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
        }
    }
}
