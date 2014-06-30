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
    public class Tutorial : Level
    {
        public Tutorial(ContentManager _content, GraphicsDevice _graphicsDevice, LevelState.LevelState _levelState)
            : base(_content, _graphicsDevice, _levelState)
        {
        }

        /// <summary>
        /// Initialize the universe
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            Plate = plate = new LibCut.Things.Plate.Plate(this,
                                                          new Orange.XNA.Sprite(content, @"Backgrounds/Plate"),
                                                          -100);
            plate.ProgressBars.Add(new LibCut.Things.Health.CollectionProgress(this, plate, typeof(LibCut.Things.Actors.Food.Bread), new Orange.XNA.Sprite(Content, @"HealthBars/BreadCollected"), new Orange.XNA.Sprite(Content, @"Food/Bread"), 40, plate.Depth + 1));

            splash = new Orange.XNA.Sprite(Content, @"Splash/WinLevel");

            levelController = new LibCut.Things.LevelController.LevelController(this);

            BG = new Things.Background.Background(this, Content.Load<Texture2D>(@"Backgrounds/TutorialBG"), 100);
        }

        /// <summary>
        /// Make sure there is only one instance of the bread at once
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            bool spawn = true;
            foreach (LibCut.Things.Thing thing in Things)
            {
                if (thing is LibCut.Shapes.PhysicsShape)
                {
                    if ((thing as LibCut.Shapes.PhysicsShape).Parent is LibCut.Things.Actors.Food.Bread)
                    {
                        spawn = false;
                    }
                }
            }

            // If spawn is still true, then spawn a new piece of bread
            if (spawn )
            {
                var bread = new LibCut.Things.Actors.Food.Bread(this, new Vector2(-700, -800));
            }
        }
    }
}
