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

namespace LibCut.Things.Model3D
{
    public class Model3D : Things.Thing
    {
        // The model to draw
        Model model;

        /// <summary>
        /// Override the rotation to add 3 dimensions
        /// </summary>
        protected Vector3 rotationVector;
        public Vector3 Rotation3D
        {
            get
            {
                return rotationVector;
            }
            set
            {
                rotationVector = value;
                rotation = Matrix.CreateRotationX(rotationVector.X);
                rotation *= Matrix.CreateRotationY(rotationVector.Y);
                rotation *= Matrix.CreateRotationZ(rotationVector.Z);

                physics.Rotation = rotationVector.Z;
            }
        }
        public Vector3 UpdateRotation3D
        {
            get
            {
                return rotationVector;
            }
            set
            {
                rotationVector = value;
                rotation = Matrix.CreateRotationX(rotationVector.X);
                rotation *= Matrix.CreateRotationY(rotationVector.Y);
                rotation *= Matrix.CreateRotationZ(rotationVector.Z);

                oldRotationFloat = rotationFloat;
                rotationFloat = rotationVector.Z;

                physics.Rotation = rotationVector.Z;
            }
        }

        /// <summary>
        /// Sets the physics to be where we are
        /// </summary>
        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                physics.Position = value / LibCut.Physics.PhysicsObject.PixelsToMetres;
            }
        }
        
        /// <summary>
        /// The physics for this object (collision)
        /// </summary>
        protected Physics.PhysicsObject physics;
        public Physics.PhysicsObject Physics
        {
            get
            {
                return physics;
            }
            set
            {
                physics = value;
            }
        }

        /// <summary>
        /// The bounding box
        /// </summary>
        protected BoundingBox bounds;
        public BoundingBox Bounds
        {
            get
            {
                return bounds;
            }
            set
            {
                bounds = value;
            }
        }

        /// <summary>
        /// Creates a new model
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_model"></param>
        public Model3D(Universe.Universe _universe, Model _model)
            : base(_universe, 9001)
        {
            model = _model;
            physics = new Physics.PhysicsObject(Universe.TheWorld);
            
            // Get all the vertices and calculate the with height and depth of the objects
            Vector3[] points = new Vector3[model.Meshes[0].MeshParts[0].NumVertices];
            model.Meshes[0].MeshParts[0].VertexBuffer.GetData<Vector3>(points);

            // Loop through all the items and find the highest and lowest
            Vector3 lowest = points[0];
            Vector3 highest = points[0];
            foreach (Vector3 point in points)
            {
                if (point.X > highest.X)
                {
                    highest.X = point.X;
                }
                if (point.X < lowest.X)
                {
                    lowest.X = point.X;
                }

                if (point.Y > highest.Y)
                {
                    highest.Y = point.Y;
                }
                if (point.Y < lowest.Y)
                {
                    lowest.Y = point.Y;
                }

                if (point.Z > highest.Z)
                {
                    highest.Z = point.Z;
                }
                if (point.Z < lowest.Z)
                {
                    lowest.Z = point.Z;
                }
            }

            // We should be able to create a bounding box with these
            bounds = new BoundingBox(lowest, highest);

            // Use the bounding box to create a sensor collision box with box2d
            Vector3 diff = Bounds.Max - Bounds.Min;
            physics.Size = new Vector2(diff.X, diff.Y);
            physics.Position = Position;
            physics.Density = 0.0f;
            physics.Sensor = true;
            physics.userData = this;
        }

        /// <summary>
        /// Creates an approximate box2d body
        /// </summary>
        public void CreateBody()
        {
            physics.ChangeShape(LibCut.Physics.PhysicsObject.Shapes.BOX, null);
        }

        /// <summary>
        /// Draw the model
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_graphicsDevice"></param>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            base.Draw(_camera, _graphicsDevice);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = Transform;
                    effect.View = _camera.CameraTransform;
                    effect.Projection = _camera.Projection;
                    effect.TextureEnabled = true;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }

        /// <summary>
        /// Updates the model
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
            if (physics != null)
            {
                if (physics.body != null)
                {
                    Transform t = physics.Step();
                    Rotation3D = new Vector3(Rotation3D.X, Rotation3D.Y, t.R.GetAngle());
                    Position = t.Position * LibCut.Physics.PhysicsObject.PixelsToMetres;
                }
            }
        }
    }
}
