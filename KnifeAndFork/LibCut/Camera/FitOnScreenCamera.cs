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

namespace LibCut.Camera
{
    public class FitOnScreenCamera : Camera
    {
        /// <summary>
        /// The things to keep on the screen
        /// </summary>
        Things.Thing[] keepOnScreen;

        /// <summary>
        /// Creates a new camera that will zoom out appropriately to fit an array of things
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_pos"></param>
        /// <param name="_things"></param>
        public FitOnScreenCamera(Universe.Universe _universe, Vector2 _pos, Things.Thing[] _things)
            : base(_universe, _universe.GraphicsDevice, _pos)
        {
            keepOnScreen = _things;
        }

        /// <summary>
        /// Updatet the camera matrixes
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Loop through all the items
            Vector2 max = keepOnScreen[0].Position;
            Vector2 min = keepOnScreen[0].Position;
            foreach (Things.Thing thing in keepOnScreen)
            {
                if (!thing.Dead)
                {
                    if (max.X < thing.Position.X)
                    {
                        max.X = thing.Position.X;
                    }
                    if (max.Y < thing.Position.Y)
                    {
                        max.Y = thing.Position.Y;
                    }
                    if (min.X > thing.Position.X)
                    {
                        min.X = thing.Position.X;
                    }
                    if (min.Y > thing.Position.Y)
                    {
                        min.Y = thing.Position.Y;
                    }
                }
            }

            // Find out which dimension needs scaling more, then just use that to make the scale nice
            float widthRatio = Math.Abs(min.X - max.X) / Universe.GraphicsDevice.Viewport.Width + 0.5f;
            float heightRatio = Math.Abs(min.Y - max.Y) / Universe.GraphicsDevice.Viewport.Height + 0.5f;
            
            // If any of these are 0, then make sure that we avoid a divide by zero
            if (widthRatio < 1)
            {
                widthRatio = 1.0f;
            }
            if (heightRatio < 1)
            {
                heightRatio = 1.0f;
            }

            // Set the scale
            if (widthRatio > heightRatio)
            {
                // Use the width
                Scale = new Vector2(1 / widthRatio);
            }
            else
            {
                // Use the height
                Scale = new Vector2(1 / heightRatio);
            }

            // Move the camera to point at the middle of the objects
            Position = ((max + min) / 2);
        }
    }
}
