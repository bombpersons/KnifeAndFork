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

namespace LibCut.Things.Actors.Accessories.Mouths
{
    public class MainMenuFace : Accessory
    {
        /// <summary>
        /// If we die then make sure that we take speech with us.
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
                    speech.Dead = true;
                }
            }
        }

        /// <summary>
        /// The speech bubble to use
        /// </summary>
        protected TextBox.TextBox speech;

        /// <summary>
        /// A random variable to pick a tip.
        /// </summary>
        static protected Random rand = new Random();

        /// <summary>
        /// An array of tips
        /// </summary>
        static protected string[] tips = new string[]
        {
            "Hold the right trigger to grab with the fork!. ",
            "You can block bullets and other projectiles by holding the right trigger and spinning with the knife!. ",
            "Hold the A button to cut with the knife. You will cut in the opposite direction to which you are facing!. ",
        };

        /// <summary>
        /// Creates a new bread face
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_wearer"></param>
        /// <param name="_depth"></param>
        public MainMenuFace(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/MainMenuFace", 150, 150, 0, 0), _depth)
        {
            // Create the speech bubble
            speech = new TextBox.TextBox(Universe, new Vector2(500, 200), 20.0f, Position + new Vector2(100), Universe.Content.Load<SpriteFont>(@"Font"), _depth);
            speech.SpeechBubble.PointAt = this;
            speech.SpeechBubble.TheTextBox.Text =
                "Press the right trigger to grab the option you want to select with the fork!. ";
        }

        /// <summary>
        /// Update the face
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // If the text is finished chose another random tip
            if (speech.SpeechBubble.TheTextBox.Finished)
            {
                speech.SpeechBubble.TheTextBox.Text = "Tip: " + tips[rand.Next(0, tips.Length)];
            }

            speech.Position = Position + new Vector2(300, 300);

            if (!speech.SpeechBubble.TheTextBox.Paused)
            {
                sprite.frame.frame = 1;
            }
            else
            {
                sprite.frame.frame = 0;
            }
        }
    }
}
