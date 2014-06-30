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
    public class Level2 : LibCut.Universe.Levels.Level
    {
        /// <summary>
        /// Creates a new game universe
        /// </summary>
        /// <param name="_content"></param>
        /// <param name="_graphicsDevice"></param>
        public Level2(ContentManager _content, GraphicsDevice _graphicsDevice, LibCut.Universe.Levels.LevelState.LevelState _levelState)
            : base(_content, _graphicsDevice, _levelState)
        {
        }

        /// <summary>
        /// Resets the level
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            EnemySpawners.Add(new LibCut.Things.EnemySpawner.EnemySpawner(this, typeof(LibCut.Things.Actors.Food.CupCake), new Rectangle(-1000, -1000, 2000, 1000), 15));
            EnemySpawners.Add(new LibCut.Things.EnemySpawner.EnemySpawner(this, typeof(LibCut.Things.Actors.Food.ChickenNugget), new Rectangle(-1000, -1000, 2000, 2000), 5));
            EnemySpawners.Add(new LibCut.Things.EnemySpawner.EnemySpawner(this, typeof(LibCut.Things.Actors.Food.Ham), new Rectangle(-3000, -3000, 3000, 3000), 3));
            EnemySpawners.Add(new LibCut.Things.EnemySpawner.EnemySpawner(this, typeof(LibCut.Things.Actors.Food.Coconut), new Rectangle(-1000, -1000, 2000, 2000), 1));
            EnemySpawners.Add(new LibCut.Things.EnemySpawner.EnemySpawner(this, typeof(LibCut.Things.Actors.Food.Carrot), new Rectangle(-2000, -2000, 4000, 4000), 1));

            BG = new Things.Background.Background(this, Content.Load<Texture2D>(@"Backgrounds/Level2BG"), 100);
            Plate = plate = new LibCut.Things.Plate.Plate(this,
                                                               new Orange.XNA.Sprite(content, @"Backgrounds/Plate"),
                                                               -100);
            plate.Position = new Vector2(0, 400);

            plate.ProgressBars.Add(new LibCut.Things.Health.CollectionProgress(this, plate, typeof(LibCut.Things.Actors.Food.ChickenNugget), new Orange.XNA.Sprite(Content, @"HealthBars/ChickenCollected"), new Orange.XNA.Sprite(Content, @"Food/ChickenNugget"), 30, plate.Depth + 1));
            plate.ProgressBars.Add(new LibCut.Things.Health.CollectionProgress(this, plate, typeof(LibCut.Things.Actors.Food.Ham), new Orange.XNA.Sprite(Content, @"HealthBars/HamCollected"), new Orange.XNA.Sprite(Content, @"Food/Ham"), 30, plate.Depth + 1));
            plate.ProgressBars.Add(new LibCut.Things.Health.CollectionProgress(this, plate, typeof(LibCut.Things.Actors.Food.Coconut), new Orange.XNA.Sprite(Content, @"HealthBars/CoconutCollected"), new Orange.XNA.Sprite(Content, @"Food/Coconut"), 30, plate.Depth + 1));

            splash = new Orange.XNA.Sprite(Content, @"Splash/WinLevel");

            var levelcontrol = new LibCut.Things.LevelController.LevelController(this);

            var mission = new LibCut.Things.PauseScreen.MissionBrief(this, new Orange.XNA.Sprite(Content, @"PausedScreens/Level1Mission"));
        }

        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);
        }

        LibCut.Things.TextBox.TextBox text;

        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Check if only the coconut is left, then leave a hint
            if (text == null)
            {
                if (plate.ProgressBars[0].Health <= 0 && plate.ProgressBars[1].Health <= 0 && plate.ProgressBars[2].Health >= 1)
                {
                    text = new LibCut.Things.TextBox.TextBox(this,
                                                             new Vector2(600, 300),
                                                             30,
                                                             new Vector2(30000, 30000),
                                                             Content.Load<SpriteFont>(@"Font"),
                                                             1000000000);
                    text.SpeechBubble.TheTextBox.Text = "Hint: Try using other food to your advantage to get the coconut! ...";
                    text.GarbageCleaned = false;
                }
            }
            else
            {
                if (text.SpeechBubble.TheTextBox.Finished)
                {
                    text.Dead = true;
                }

                text.Position = new Vector2(30000, 30000);
            }
        }
    }
}
