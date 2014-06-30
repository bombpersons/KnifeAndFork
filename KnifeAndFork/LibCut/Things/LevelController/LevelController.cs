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

namespace LibCut.Things.LevelController
{
    public class LevelController : Things.Thing
    {
        /// <summary>
        /// The level this controller is controlling
        /// </summary>
        public Universe.Levels.Level Level
        {
            get
            {
                return (Universe as Universe.Levels.Level);
            }
        }

        /// <summary>
        /// The timer
        /// </summary>
        protected TimeSpan timer;
        public TimeSpan Timer
        {
            get
            {
                return timer;
            }
            set
            {
                timer = value;
            }

        }

        /// <summary>
        /// How long we need to have the plate full to win.
        /// </summary>
        protected TimeSpan holdTime = new TimeSpan(0, 0, 5);

        /// <summary>
        /// Health bar to show how much longer we have to wait until we win.
        /// </summary>
        protected Things.Health.HealthBar done;
        public Things.Health.HealthBar Done
        {
            get
            {
                return done;
            }
            set
            {
                done = value;
            }
        }

        /// <summary>
        /// The sprite to show with the meter when you have almost won.
        /// </summary>
        protected Sprite.StaticSprite protect;
        public Sprite.StaticSprite Protect
        {
            get
            {
                return protect;
            }
            set
            {
                protect = value;
            }
        }

        /// <summary>
        /// The splash sprite
        /// </summary>
        protected Sprite.StaticSprite splash;
        public Sprite.StaticSprite Splash
        {
            get
            {
                return splash;
            }
            set
            {
                splash = value;
            }
        }

        /// <summary>
        /// Whether or not the game is over
        /// </summary>
        protected bool gameOver;
        public bool GameOver
        {
            get
            {
                return gameOver;
            }
            set
            {
                gameOver = value;
            }
        }

        /// <summary>
        /// Controls the level. Displays win screen if the players win
        /// </summary>
        /// <param name="_level"></param>
        public LevelController(Universe.Levels.Level _level)
            : base(_level)
        {
            // Load the helalth bar
            done = new Health.HealthBar(Universe, new Orange.XNA.Sprite(Universe.Content, @"HealthBars/PlateCompleted"));
            done.Position = new Vector2(Universe.GraphicsDevice.Viewport.Width , Universe.GraphicsDevice.Viewport.Height) / 2;

            // Load the protect sprite
            protect = new Sprite.StaticSprite(Universe, new Orange.XNA.Sprite(Universe.Content, @"Splash/ProtectPlate"), done.Depth);
            protect.Position = done.Position - new Vector2(0, protect.TheSprite.size.Y / 2);
            protect.Visible = false;
        }

        /// <summary>
        /// Check for triggers
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(Microsoft.Xna.Framework.GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Check if all the food has been collected
            bool allDone = true;
            foreach (Things.Health.CollectionProgress bar in Level.Plate.ProgressBars)
            {
                if (bar.Health > 0)
                {
                    allDone = false;
                }
            }

            // If the plate is full at the moment, then increase the timer count
            if (allDone)
            {
                protect.Visible = true;

                if (timer < holdTime)
                {
                    timer += _gameTime.ElapsedGameTime;
                }
                else
                {
                    // We wins =)
                    if (splash == null)
                    {
                        GameOver = true;
                        splash = new Things.Sprite.StaticSprite(Universe, Level.Splash, 100000);
                        splash.Position = new Vector2(Universe.GraphicsDevice.Viewport.Width / 2, Universe.GraphicsDevice.Viewport.Height / 2);
                    }
                }
            }
            else
            {
                protect.Visible = false;

                timer = new TimeSpan();
            }

            // Check if any players have died
            if (Level.Fork.Health.Health <= 0 || Level.Knife.Health.Health <= 0)
            {
                // Display the game over screen
                if (!GameOver)
                {
                    splash = new Things.Sprite.StaticSprite(Universe, new Orange.XNA.Sprite(Universe.Content, @"Splash/Die"), 100000);
                    splash.Position = new Vector2(Universe.GraphicsDevice.Viewport.Width / 2, Universe.GraphicsDevice.Viewport.Height / 2);

                    Level.Knife.Dead = true;
                    Level.Fork.Dead = true;
                }

                GameOver = true;
            }
        }

        /// <summary>
        /// Will go to the main menu if a button is pressed
        /// </summary>
        /// <param name="_gameTime"></param>
        /// <param name="_input"></param>
        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);

            // If the game is over and a button is pressed, then reset the level
            if (GameOver && (_input.ClickedPadButton(Buttons.A, 0) || _input.ClickedPadButton(Buttons.A, 1)))
            {
                Level.LevelState.Level = new Universe.Levels.Menu(Universe.Content, Universe.GraphicsDevice, Level.LevelState);
            }
        }

        /// <summary>
        /// Draw the bar that shows the players how long until the win when they have the plate full of food.
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_graphicsDevice"></param>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            base.Draw(_camera, _graphicsDevice);

            Done.Health = (float)timer.Ticks / (float)holdTime.Ticks;
        }
    }
}
