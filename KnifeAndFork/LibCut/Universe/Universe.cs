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

using Box2D.XNA;

namespace LibCut.Universe
{
    public class Universe : Things.Thing
    {
        /// <summary>
        /// Store the content manager so we can store stuff later
        /// </summary>
        protected ContentManager content;
        public ContentManager Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }

        /// <summary>
        /// The graphics device to use
        /// </summary>
        protected GraphicsDevice graphicsDevice;
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return graphicsDevice;
            }
            set
            {
                graphicsDevice = value;
            }
        }

        /// <summary>
        /// The world for box2d to use
        /// </summary>
        protected World theWorld;
        public World TheWorld
        {
            get
            {
                return theWorld;
            }
            set
            {
                theWorld = value;
            }
        }

        /// <summary>
        /// A list of all of the things in this universe
        /// </summary>
        protected List<Things.Thing> things = new List<Things.Thing>();
        public List<Things.Thing> Things
        {
            get
            {
                return things;
            }
            set
            {
                things = value;
            }
        }

        /// <summary>
        /// The camera to use
        /// </summary>
        protected Camera.Camera camera;
        public Camera.Camera Camera
        {
            get
            {
                return camera;
            }
            set
            {
                camera = value;
            }
        }

        /// <summary>
        /// Whether or not the universe is locked (paused)
        /// </summary>
        protected bool locked;
        public bool Locked
        {
            get
            {
                return locked;
            }
            set
            {
                locked = value;
            }
        }

        /// <summary>
        /// Creates the universe
        /// </summary>
        /// <param name="_graphicsDevice"></param>
        /// <param name="_content"></param>
        public Universe(ContentManager _content, GraphicsDevice _graphicsDevice)
        {
            content = _content;
            graphicsDevice = _graphicsDevice;

            // Create a world to use
            theWorld = new World(new Vector2(0, 9.81f), false);

            // Add our contact listener
            theWorld.ContactListener = new Physics.CollisionContact();

            // Reset the level
            Reset();
        }

        /// <summary>
        /// Resets the world
        /// </summary>
        public virtual void Reset()
        {
            // Kill all the objects
            foreach (LibCut.Things.Thing thing in Things)
            {
                thing.Dead = true;
            }

            // Clear the list
            Things.Clear();
        }

        /// <summary>
        /// Updates the universe
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Only update if we aren't locked
            if (!Locked)
            {

                // Step the world
                TheWorld.Step(0.016f, 10, 10);

                // Update all the stuff
                foreach (Things.Thing thing in things.ToArray())
                {
                    thing.Update(_gameTime);

                    // If the thing is dead then remove
                    if (thing.Dead)
                    {
                        things.Remove(thing);
                    }
                }

            }
        }

        /// <summary>
        /// Handles any input
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);

            // Update all the stuff
            foreach (Things.Thing thing in things.ToArray())
            {
                thing.HandleInput(_gameTime, _input);
            }
        }

        /// <summary>
        /// Draws the universe
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_graphicsDevice"></param>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            base.Draw(_camera, _graphicsDevice);

            // Update all the stuff
            foreach (Things.Thing thing in things)
            {
                thing.Draw(_camera, _graphicsDevice);
            }
        }

        /// <summary>
        /// Shortcut
        /// </summary>
        /// <param name="_graphicsDevice"></param>
        public virtual void Draw()
        {
            Draw(Camera, graphicsDevice);
        }
    }
}
