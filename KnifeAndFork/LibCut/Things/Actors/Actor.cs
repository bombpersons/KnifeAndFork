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

namespace LibCut.Things.Actors
{
    public class Actor : Thing
    {
        /// <summary>
        /// The target of the actor
        /// </summary>
        protected Vector2 target;
        public Vector2 Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        /// <summary>
        /// The range of the actor
        /// </summary>
        protected float range = 600;
        public float Range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
            }
        }

        /// <summary>
        /// The shape of the actor
        /// </summary>
        protected Shapes.PhysicsShape shape;
        public Shapes.PhysicsShape Shape
        {
            get
            {
                return shape;
            }
            set
            {
                shape = value;
            }
        }

        /// <summary>
        /// Creates the actor
        /// </summary>
        /// <param name="_position"></param>
        public Actor(Universe.Universe _universe, Vector2 _position)
            : base(_universe)
        {
            // Create a shape
            shape = new Shapes.PhysicsShape(_universe);
            shape.Parent = this;

            Position = _position;
            Target = _position;
            shape.Position = _position;
        }

        /// <summary>
        /// Saves the actor to a file
        /// </summary>
        /// <param name="_filename"></param>
        /// <returns></returns>
        public bool SaveToFile(string _filename)
        {
            // Open a file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(_filename))
            {
                // Check if the file opened
                if (file == null)
                    return false;

                // Save the points of the shape first
                file.Write("Shape;");

                // The texture of the shape now
                file.Write(Shape.Tex.Name + ";");

                // Write All the points
                foreach (Vector2 point in Shape.Points)
                {
                    file.Write(point.X.ToString() + ";");
                    file.Write(point.Y.ToString() + ";");
                }

                // Save the accessories
                file.Write("Accessories;");

                // Write all the accessories
                foreach (Things.Actors.Accessories.Accessory acc in Shape.Accessories)
                {
                    file.Write(acc.GetType().ToString() + ";");
                    file.Write(acc.Clamp.X.ToString() + ";");
                    file.Write(acc.Clamp.Y.ToString() + ";");
                }

                // Okay close the file
                file.Close();
            }

            // Return true since everything thing went ok
            return true;
        }

        /// <summary>
        /// Loads self from a string
        /// </summary>
        /// <param name="_load"></param>
        /// <returns></returns>
        public bool LoadFromString(string _load)
        {
            // Split the string up into bits
            string[] bits = _load.Split(';');

            // Loop through and do stuff with it
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i] == "Shape")
                {
                    // This is the shape definition
                    
                    // The next bit is the texture name, so lets load that
                    Shape.Tex = Universe.Content.Load<Texture2D>(bits[i + 1]);

                    // Clear the points before loading
                    Shape.Points.Clear();

                    // The next bit is the points the shape is made of. It should be in order.
                    for (int j = 0;; j += 2)
                    {
                        // Break out of the loop if we can't get any valid floats
                        if (bits[i + 2 + j] == "Accessories")
                        {
                            i += 2 + j;
                            break;
                        }

                        // The first should be the x coord, and the second should be the y
                        Vector2 point = new Vector2(float.Parse(bits[i + 2 + j]), float.Parse(bits[i + 3 + j]));

                        // Add to the points in the shape
                        Shape.Points.Add(point);
                    }

                    // Sort the points after adding them
                    Shape.Sort();

                    // Init the texture stuff
                    Shape.InitTextureStuff();
                }

                if (bits[i] == "Accessories")
                {
                    // The accessory block

                    // The following data should be information about all the accessories this actor has.
                    
                    // Loop through all of this and create the objects as we go along
                    for (int j = 1; i < bits.Length; j += 3)
                    {
                        // Check the first bit isn't blank
                        if (bits[i + j] == "")
                            break;

                        // The first part is the name of the object
                        Actors.Accessories.Accessory acc = (Actors.Accessories.Accessory)Activator.CreateInstance(Type.GetType(bits[i+j]), new object[] {Universe, Shape, Shape.Depth + 1});

                        // The position of the accessory
                        acc.Position = new Vector2(float.Parse(bits[i + j + 1]), float.Parse(bits[i + j + 2]));
                        acc.AddToWearer();
                    }
                }

                Shape.Position = Position;

                // Set the default sound
                Shape.Sound = Universe.Content.Load<SoundEffect>(@"Sound/Cut");
            }

            // Okay done loading =)
            return true;
        }

        /// <summary>
        /// Sets the world
        /// </summary>
        /// <param name="_world"></param>
        public bool SetWorld(World _world)
        {
            return shape.SetWorld(_world);
        }

        /// <summary>
        /// Update stuff
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Set position to the position of the shape
            if (Shape != null)
            {
                Position = Shape.Position;
                Rotation = Shape.Rotation;
                Scale = Shape.Scale;

                if (Shape.Dead)
                {
                    Dead = true;
                    Shape = null;
                }
            }
        }

        /// <summary>
        /// Draw Everything
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_graphicsDevice"></param>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            base.Draw(_camera, _graphicsDevice);
        }
    }
}
