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

namespace Orange.XNA.Graphics.UI
{
    /// <summary>
    /// A button
    /// </summary>
    public class Button : Widget
    {
        /// <summary>
        /// Constructs the button. The button will be displayed as rect
        /// </summary>
        /// <param name="_sprite"></param>
        public Button(Rectangle _rect)
            : base (_rect)
        {
            // Set up the click call back
            clickCallBack = DefaultClickCallBack;
        }

        /// <summary>
        /// Add the click call back
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            if (Clicked)
            {
                clickCallBack();
            }
        }

        /// <summary>
        /// The delegate for the click callback
        /// </summary>
        /// <param name="_pos"></param>
        public delegate void ClickCallBack();
        public ClickCallBack clickCallBack;

        /// <summary>
        /// An empty callback to stop everything from crashing if the delegate isn't set
        /// </summary>
        protected void DefaultClickCallBack()
        {
            Console.WriteLine("CLICKY");
        }
    }
}
