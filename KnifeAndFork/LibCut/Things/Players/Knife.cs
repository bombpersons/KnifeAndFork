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

namespace LibCut.Things.Players
{
    public class Knife : Player
    {
        /// <summary>
        /// Override positin and rotati
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
                spinPhysics.Position = value / LibCut.Physics.PhysicsObject.PixelsToMetres;
                cutPhysics.Position = (value + new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) * -110) / LibCut.Physics.PhysicsObject.PixelsToMetres;
                (cutPhysics.userData as Thing).Position = (value + new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) * -110);
            }
        }
        public override float Rotation
        {
            get
            {
                return base.Rotation;
            }
            set
            {
                spinPhysics.Rotation = value;
                base.Rotation = value;
            }
        }

        /// <summary>
        /// Whether or not the knife is in cutting mode
        /// </summary>
        protected bool cutting = false;
        public bool Cutting
        {
            get
            {
                return cutting;
            }
            set
            {
                cutting = value;
                if (value)
                {
                    cutPhysics.Size = new Vector2(10);
                    cutPhysics.ChangeShape(LibCut.Physics.PhysicsObject.Shapes.CIRCLE, null);
                }
                else
                {
                    cutPhysics.RemoveShape();
                }
            }
        }

        /// <summary>
        /// When we are spining, we deflect bullets
        /// </summary>
        protected bool spinning = false;
        public bool Spinning
        {
            get
            {
                return spinning;
            }
            set
            {
                spinning = value;
                if (value)
                {
                    spinPhysics.ChangeShape(LibCut.Physics.PhysicsObject.Shapes.CIRCLE, null);
                }
                else
                {
                    spinPhysics.RemoveShape();
                }
            }
        }

        /// <summary>
        /// The physics object for the spinning
        /// </summary>
        Physics.PhysicsObject spinPhysics;

        /// <summary>
        /// The speed to cut at
        /// </summary>
        protected float cuttingSpeed = 20;
        public float CuttingSpeed
        {
            get
            {
                return cuttingSpeed;
            }
            set
            {
                cuttingSpeed = value;
            }
        }

        /// <summary>
        /// The amount to rotate the knife when cutting
        /// </summary>
        protected Vector2 cuttingRotation = new Vector2(0, -8 * (float)Math.PI / 32);
        public Vector2 CuttingRotation
        {
            get
            {
                return cuttingRotation;
            }
            set
            {
                cuttingRotation = value;
            }
        }

        /// <summary>
        /// The points that are cutting
        /// </summary>
        protected Vector2[] cutPoints = new Vector2[2];

        /// <summary>
        /// The object to detect collisions for cutting
        /// </summary>
        protected Physics.PhysicsObject cutPhysics;

        /// <summary>
        /// Creates a new knife
        /// </summary>
        /// <param name="_universe"></param>
        public Knife(Universe.Universe _universe)
            : base(_universe, _universe.Content.Load<Model>(@"Models/Knife"), new Orange.XNA.Sprite(_universe.Content, @"HealthBars/KnifeHealth"))
        {
            physics.Size = new Vector2(150, 50);
            CreateBody();

            // The spinning physics
            spinPhysics = new Physics.PhysicsObject(Universe.TheWorld);
            spinPhysics.Size = new Vector2(200);

            // The cutting physics
            cutPhysics = new Physics.PhysicsObject(Universe.TheWorld);
            cutPhysics.Size = new Vector2(10);
            cutPhysics.userData = new CutDummy(this);
            cutPhysics.sensor = true;

            // Rotate the knife
            Rotation3D = new Vector3((float)Math.PI / 2, Rotation3D.Y, Rotation3D.Z);

            // Position the health bar
            Health.Position = new Vector2(Universe.GraphicsDevice.Viewport.Width - (Health.TheSprite.texture.Width / 2),
                                          Universe.GraphicsDevice.Viewport.Height - (Health.TheSprite.texture.Height / 2));
            Health.TheSprite.flip = SpriteEffects.FlipHorizontally;
            Health.Side = true;
        }

        /// <summary>
        /// Update the knife
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Y COORD
            if (Cutting && rotationVector.Y > CuttingRotation.Y)
            {
                Rotation3D = new Vector3(rotationVector.X, rotationVector.Y - 0.2f, rotationVector.Z);
                if (rotationVector.Y < CuttingRotation.Y)
                {
                    Rotation3D = new Vector3(rotationVector.X, CuttingRotation.Y, rotationVector.Z);
                }
            }
            else if (!Cutting && rotationVector.Y < 0)
            {
                Rotation3D = new Vector3(rotationVector.X, rotationVector.Y + 0.05f, rotationVector.Z);
                if (rotationVector.Y > 0)
                {
                    Rotation3D = new Vector3(rotationVector.X, 0, rotationVector.Z);
                }
            }

            // X COORD
            if (Cutting && rotationVector.X > CuttingRotation.X)
            {
                Rotation3D = new Vector3(rotationVector.X - 0.2f, rotationVector.Y, rotationVector.Z);
                if (rotationVector.Y < CuttingRotation.X)
                {
                    Rotation3D = new Vector3(rotationVector.X, rotationVector.Y, rotationVector.Z);
                }
            }
            else if (!Cutting && rotationVector.X < (float)Math.PI / 2)
            {
                Rotation3D = new Vector3(rotationVector.X + 0.05f, rotationVector.Y, rotationVector.Z);
                if (rotationVector.X > 0)
                {
                    Rotation3D = new Vector3((float)(Math.PI / 2), rotationVector.Y, rotationVector.Z);
                }
            }
        }

        /// <summary>
        /// Take input from the player
        /// </summary>
        /// <param name="_gameTime"></param>
        /// <param name="_input"></param>
        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);

            /*
            if (_input.ClickedButton(Keys.Space))
            {
                Cutting = true;
                cutPoints[0] = Position;
            }
            else if(_input.ReleasedButton(Keys.Space))
            {
                Cutting = false;
                cutPoints[1] = Position;
                
                // Do the cut
                foreach(Things.Thing thing in Universe.Things.ToArray())
                {
                    var shape = thing as Shapes.PhysicsShape;
                    if (shape != null)
                    {
                        shape.Cut(cutPoints[0], cutPoints[1]);
                    }
                }
            }

            // Move forward if cutting
            if (Cutting)
            {
                Position += new Vector2((float)Math.Cos(Rotation3D.Z), (float)Math.Sin(Rotation3D.Z)) * CuttingSpeed;
            }

            // Rotate the fork
            if (_input.Keyboard.IsKeyDown(Keys.Left) && !Cutting)
            {
                Rotation3D = new Vector3(rotationVector.X, rotationVector.Y, rotationVector.Z + 0.1f);
            }
            if (_input.Keyboard.IsKeyDown(Keys.Right) && !Cutting)
            {
                Rotation3D = new Vector3(rotationVector.X, rotationVector.Y, rotationVector.Z - 0.1f);
            }

            // Rotate the fork
            if (_input.Keyboard.IsKeyDown(Keys.Left) && !Cutting && _input.Keyboard.IsKeyDown(Keys.LeftControl))
            {
                if (!Spinning)
                {
                    Spinning = true;
                }
                Rotation3D = new Vector3(rotationVector.X, rotationVector.Y, rotationVector.Z + 0.5f);
            }
            else if (_input.Keyboard.IsKeyDown(Keys.Right) && !Cutting && _input.Keyboard.IsKeyDown(Keys.LeftControl))
            {
                if (!Spinning)
                {
                    Spinning = true;
                }
                Rotation3D = new Vector3(rotationVector.X, rotationVector.Y, rotationVector.Z - 0.5f);
            }
            else
            {
                if (Spinning)
                    Spinning = false;
            }

            if (_input.Keyboard.IsKeyDown(Keys.W))
            {
                Position += new Vector2(0, -1) * 3;
            }
            if (_input.Keyboard.IsKeyDown(Keys.R))
            {
                Position += new Vector2(0, 1) * 3;
            }
            if (_input.Keyboard.IsKeyDown(Keys.S))
            {
                Position += new Vector2(1, 0) * 3;
            }
            if (_input.Keyboard.IsKeyDown(Keys.A))
            {
                Position += new Vector2(-1, 0) * 3;
            }*/

            //Position = Universe.Camera.GetWorldCoord(Universe.GraphicsDevice, new Vector2(_input.Mouse.X, _input.Mouse.Y));

            // If the universe isn't locked
            if (!Universe.Locked)
            {

                /* XBOX CONTROLS */
                if (_input.ClickedPadButton(Buttons.A, 0))
                {
                    Cutting = true;
                }
                else if (_input.ReleasedPadButton(Buttons.A, 0))
                {
                    Cutting = false;
                }

                // Move forward if cutting
                if (Cutting)
                {
                    Position += new Vector2((float)Math.Cos(Rotation3D.Z), (float)Math.Sin(Rotation3D.Z)) * CuttingSpeed;
                }

                // Rotate the fork
                if (!Cutting)
                {
                    UpdateRotation3D = new Vector3(rotationVector.X, rotationVector.Y, rotationVector.Z + (0.2f * _input.GetGamePad(0).ThumbSticks.Right.X * (1 + _input.GetGamePad(0).Triggers.Right)));
                }

                // Rotate the fork
                if (!Cutting && Math.Abs(AngularVelocity) > 0.6f && Math.Abs(AngularVelocity) < 3.0f)
                {
                    if (!Spinning)
                    {
                        Spinning = true;
                    }
                }
                else
                {
                    if (Spinning)
                        Spinning = false;
                }

                Position += _input.GetGamePad(0).ThumbSticks.Left * new Vector2(1, -1) * 9;
            }
        }

        /// <summary>
        /// The flash to show
        /// </summary>
        Things.Flash.KnifeFlash flash;

        /// <summary>
        /// Take damage
        /// </summary>
        /// <param name="_damage"></param>
        public override void TakeDamage(float _damage)
        {
            base.TakeDamage(_damage);
            if (flash != null)
                if (flash.Dead)
                    flash = null;

            if (flash == null)
                flash = new Things.Flash.KnifeFlash(Universe, new TimeSpan(0, 0, 1), Depth);
        }
    }

    /// <summary>
    /// A dummy class for cutting
    /// </summary>
    public class CutDummy : LibCut.Things.Thing
    {
        /// <summary>
        /// The knife this is attached to.
        /// </summary>
        protected Knife knife;
        public Knife Knife
        {
            get
            {
                return knife;
            }
            set
            {
                knife = value;
            }
        }

        /// <summary>
        /// Set the knife object
        /// </summary>
        /// <param name="_knife"></param>
        public CutDummy(Knife _knife)
        {
            Knife = _knife;
        }
    }

}