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
    public class Camera : Things.Thing
    {
        // The transform matrix needs to be inverted
        public Matrix CameraTransform
        {
            get
            {
                Matrix t = Matrix.CreateLookAt(
                                  new Vector3(Position, -2000.0f),
                                  new Vector3(Position, 0.0f),
                                  Vector3.Down
                                  );
                return t * scale * Matrix.Invert(rotation);
            }
        }

        /// <summary>
        /// The projection matrix to use
        /// </summary>
        protected Matrix projection = Matrix.Identity;
        public virtual Matrix Projection
        {
            get
            {
                return projection;
            }
            set
            {
                projection = value;
            }
        }


        public virtual Matrix PerspectiveProjection
        {
            get
            {
                Matrix persp = Matrix.CreatePerspective(Universe.GraphicsDevice.Viewport.Width,
                                                        Universe.GraphicsDevice.Viewport.Height,
                                                        0.01f,
                                                        3000.0f);
                return persp;
            }
        }

        /// <summary>
        /// Creates a new camera at position _pos
        /// </summary>
        /// <param name="_pos"></param>
        public Camera(Universe.Universe _universe, GraphicsDevice _graphicsDevice, Vector2 _pos)
            : base(_universe)
        {
            Position = _pos;

            // Create the projection matrix
            Projection = Matrix.CreateOrthographic(_graphicsDevice.Viewport.Width,
                                                   _graphicsDevice.Viewport.Height,
                                                   1.0f,
                                                   3000.0f);

            // Make sure we don't get destroyed by the garbage cleaner
            GarbageCleaned = false;
        }

        /// <summary>
        /// Gets the world coordinates for a locaton on the screen
        /// </summary>
        /// <param name="_graphicsDevice"></param>
        public Vector2 GetWorldCoord(GraphicsDevice _graphicsDevice, Vector2 _pos)
        {
            Vector3 temp = _graphicsDevice.Viewport.Unproject(new Vector3(_pos, 0),
                                                              Projection,
                                                              CameraTransform,
                                                              Matrix.Identity);

            return new Vector2(temp.X, temp.Y);
        }

        /// <summary>
        /// Gets the screen coordinates for a locaton on in the world
        /// </summary>
        /// <param name="_graphicsDevice"></param>
        public Vector2 GetScreenCoord(GraphicsDevice _graphicsDevice, Vector2 _pos)
        {
            Vector3 temp = _graphicsDevice.Viewport.Project(new Vector3(_pos, 0),
                                                            Projection,
                                                            CameraTransform,
                                                            Matrix.Identity);

            return new Vector2(temp.X, temp.Y);
        }
    }
}
