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
    public class BigTalkingBreadFace : Accessory
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
        /// Creates a new bread face
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_wearer"></param>
        /// <param name="_depth"></param>
        public BigTalkingBreadFace(Universe.Universe _universe, Thing _wearer, int _depth)
            : base(_universe, _wearer, new Orange.XNA.Sprite(_universe.Content, @"Accessories/BigTalkingBreadFace", 250, 250, 0, 0), _depth)
        {
            // Create the speech bubble
            speech = new TextBox.TextBox(Universe, new Vector2(500, 100), 20.0f, Position + new Vector2(100), Universe.Content.Load<SpriteFont>(@"Font"), _depth);
            speech.SpeechBubble.PointAt = this;
            speech.SpeechBubble.TheTextBox.Text =
                "Oh, hi there!. Would you like some bread?. Press and hold the A button to cut with the Knife. Make sure you don't let the bread fall. Use the fork to pick it up as it falls and bring it back to your plate. Hold the right trigger to pick things up with the fork.";
        }

        /// <summary>
        /// Update the face
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            speech.Position = Position + new Vector2(300, -300);

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
