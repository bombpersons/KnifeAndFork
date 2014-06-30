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

namespace LibCut.Things
{
    public class Thing
    {
        /// <summary>
        /// The position of the shape
        /// </summary>
        protected Matrix position = Matrix.Identity;
        protected Vector2 oldPosition;
        public virtual Vector2 Position
        {
            get
            {
                return new Vector2(position.M41, position.M42);
            }
            set
            {
                oldPosition = Position;
                position = Matrix.CreateTranslation(new Vector3(value, 0));

                // Update the position of any accessories
                foreach (Actors.Accessories.Accessory acc in accessories.ToArray())
                {
                    acc.Position = Position + Vector2.Transform(acc.Clamp, rotation);
                    acc.Rotation = Rotation;

                    // Remove from the list if it is dead
                    if (acc.Dead)
                    {
                        accessories.Remove(acc);
                    }
                }
            }
        }
        public virtual Vector2 LinearVelocity
        {
            get
            {
                return new Vector2((Position.X - oldPosition.X), (Position.Y - oldPosition.Y));
            }
        }

        /// <summary>
        /// The scale of the shape
        /// </summary>
        protected Matrix scale = Matrix.Identity;
        protected Vector2 scaleVector = new Vector2(1, 1);
        public virtual Vector2 Scale
        {
            get
            {
                return scaleVector;
            }
            set
            {
                scale = Matrix.CreateScale(new Vector3(value, 1));
                scaleVector = value;
            }
        }

        /// <summary>
        /// The rotation of the shape
        /// </summary>
        protected Matrix rotation = Matrix.Identity;
        protected float rotationFloat = 0;
        protected float oldRotationFloat = 0;
        public virtual float Rotation
        {
            get
            {
                return rotationFloat;
            }
            set
            {
                oldRotationFloat = rotationFloat;
                rotation = Matrix.CreateRotationZ(value);
                rotationFloat = value;
            }
        }
        public virtual float AngularVelocity
        {
            get
            {
                return (rotationFloat % (float)Math.PI*2) - (oldRotationFloat % (float)Math.PI*2);
            }
        }

        /// Gets the tranform matrix
        /// </summary>
        public virtual Matrix Transform
        {
            get
            {
                Matrix worldMatrix = Matrix.Identity;

                // Scale
                worldMatrix *= scale;

                // Rotate
                worldMatrix *= rotation;

                // Translate
                worldMatrix *= position;

                return worldMatrix;
            }
        }

        /// <summary>
        /// The depth of the thing. Set's the preference for inserting into the universe list
        /// </summary>
        protected int depth = 0;
        public virtual int Depth
        {
            get
            {
                return depth;
            }
            set
            {
                depth = value;
            }
        }

        /// <summary>
        /// Whether or not this thing is dead. Used to remove unused things from a universe
        /// </summary>
        protected bool dead;
        public virtual bool Dead
        {
            get
            {
                return dead;
            }
            set
            {
                dead = value;
            }
        }

        /// <summary>
        /// Let the garbage cleaner know whether or not to clean up this object
        /// </summary>
        protected bool garbageCleaned = true;
        public bool GarbageCleaned
        {
            get
            {
                return garbageCleaned;
            }
            set
            {
                garbageCleaned = value;
            }
        }


        /// <summary>
        /// Whether or not the shape can escape the plate
        /// </summary>
        protected bool escapable;
        public bool Escapable
        {
            get
            {
                return escapable;
            }
            set
            {
                escapable = value;
            }
        }

        /// <summary>
        /// The universe this thing belongs to
        /// </summary>
        Universe.Universe universe;
        public Universe.Universe Universe
        {
            get
            {
                return universe;
            }
            set
            {
                universe = value;
            }
        }

        /// <summary>
        /// A list of accesories attached to this thing
        /// </summary>
        protected List<Actors.Accessories.Accessory> accessories = new List<Actors.Accessories.Accessory>();
        public List<Actors.Accessories.Accessory> Accessories
        {
            get
            {
                return accessories;
            }
            set
            {
                accessories = value;
            }
        }

        /// <summary>
        /// If you don't want the object to be added to a universe automatically call this constructor instead
        /// </summary>
        public Thing()
        {
        }

        // Creates a new thing
        public Thing(Universe.Universe _universe)
        {
            AddToUniverse(_universe, 0);
        }

        /// <summary>
        /// Creates a new thing with a specified depth
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_depth"></param>
        public Thing(Universe.Universe _universe, int _depth)
        {
            AddToUniverse(_universe, _depth);
        }

        /// <summary>
        /// Adds the thing to a universe
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_depth"></param>
        public void AddToUniverse(Universe.Universe _universe, int _depth)
        {
            universe = _universe;
            depth = _depth;

            bool inserted = false;
            for (int i = 0; i < Universe.Things.Count; i++)
            {
                if (Universe.Things[i].Depth > Depth)
                {
                    Universe.Things.Insert(i, this);
                    inserted = true;
                    break;
                }
            }
            if (!inserted)
            {
                universe.Things.Add(this);
            }
        }

        // Update the thing
        public virtual void Update(GameTime _gameTime)
        {
            // If a hit has happened call the collision event
            if (Hit)
            {
                Hit = false;
                Collide(HitObject);
            }
            if (UnHit)
            {
                UnHit = false;
                UnCollide(HitObject);
            }
        }

        // Handle any input
        public virtual void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
        }

        // Draw the thing
        public virtual void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
        }

        /// <summary>
        /// Set to make a collision happen
        /// </summary>
        protected bool hit;
        protected bool unhit;
        protected object hitObject;
        public bool Hit
        {
            get
            {
                return hit;
            }
            set
            {
                hit = value;
            }
        }
        public bool UnHit
        {
            get
            {
                return unhit;
            }
            set
            {
                unhit = value;
            }
        }
        public object HitObject
        {
            get
            {
                return hitObject;
            }
            set
            {
                hitObject = value;
            }
        }

        /// <summary>
        /// Collision with another object
        /// </summary>
        /// <param name="_other"></param>
        public virtual void Collide(object _other)
        {
        }

        /// <summary>
        /// When 
        /// </summary>
        /// <param name="_other"></param>
        public virtual void UnCollide(object _other)
        {
        }
    }
}
