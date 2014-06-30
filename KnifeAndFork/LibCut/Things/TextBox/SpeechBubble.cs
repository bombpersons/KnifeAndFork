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

namespace LibCut.Things.TextBox
{
    public class SpeechBubble : Shapes.Shape
    {
        /// <summary>
        /// Whether or not to have the speech bubble follow on screen
        /// </summary>
        protected bool dynamic = true;
        public bool Dynamic
        {
            get
            {
                return dynamic;
            }
            set
            {
                dynamic = value;
            }
        }

        /// <summary>
        /// The position of the bubble
        /// </summary>
        public override Vector2 Position
        {
            get
            {
                if (Dynamic)
                {
                    Vector2 pos = Universe.Camera.GetScreenCoord(Universe.GraphicsDevice, new Vector2(position.Translation.X, position.Translation.Y));
                    if (!Universe.GraphicsDevice.Viewport.Bounds.Contains((int)pos.X, (int)pos.Y))
                    {
                        if (position.Translation.Y < Universe.Camera.Position.Y)
                        {
                            return Universe.Camera.GetWorldCoord(Universe.GraphicsDevice, new Vector2(Universe.GraphicsDevice.Viewport.Width / 2, 0 + size.Y * Scale.Y * Universe.Camera.Scale.Y));
                        }
                        if (position.Translation.Y > Universe.Camera.Position.Y)
                        {
                            return Universe.Camera.GetWorldCoord(Universe.GraphicsDevice, new Vector2(Universe.GraphicsDevice.Viewport.Width / 2, Universe.GraphicsDevice.Viewport.Height - size.Y * Scale.Y * Universe.Camera.Scale.Y));
                        }
                    }
                }
                return base.Position;
            }
            set
            {
                base.Position = value;
            }
        }

        /// <summary>
        /// Override transform to use the new position
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
                worldMatrix *= Matrix.CreateTranslation(new Vector3(Position, 0));

                return worldMatrix;
            }
        }

        /// <summary>
        /// The static position of the speechbubble (when in dynamic mode)
        /// </summary>
        protected Vector2 staticPosition;

        /// <summary>
        /// The text box to use to draw the text
        /// </summary>
        protected RPGText.TextBoxDrawing textBox;
        public RPGText.TextBoxDrawing TheTextBox
        {
            get
            {
                return textBox;
            }
            set
            {
                textBox = value;
            }
        }

        /// <summary>
        /// A spritebatch to draw stuff with
        /// </summary>
        protected SpriteBatch spriteBatch;

        /// <summary>
        /// The size of the speech buble
        /// </summary>
        protected Vector2 size;
        public Vector2 Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }

        }

        /// <summary>
        /// The roundedness of the edges
        /// </summary>
        protected float pinch = 4;
        public float Pinch
        {
            get
            {
                return pinch;
            }
            set
            {
                pinch = value;
            }
        }

        /// <summary>
        /// Where the speech bubble is pointing at
        /// </summary>
        protected Thing pointAt;
        public Thing PointAt
        {
            get
            {
                return pointAt;
            }
            set
            {
                pointAt = value;
            }
        }

        /// <summary>
        /// A triangle pointing to the pointAt
        /// </summary>
        public List<Vector2> Triangle
        {
            get
            {
                if (PointAt != null)
                {
                    List<Vector2> triangle = new List<Vector2>();

                    Vector2 diff = PointAt.Position - Position;
                    diff.Normalize();
                    triangle.Add((PointAt.Position - diff * 20) - Position);

                    // Put two points at either sides of the normal to diff
                    triangle.Add(0.1f * size.Length() * new Vector2(diff.Y, -diff.X));
                    triangle.Add(-0.1f * size.Length() * new Vector2(diff.Y, -diff.X));

                    triangle.Sort(new Shapes.ClockwiseComparer());

                    return triangle;
                }

                return new List<Vector2>();
            }
        }

        /// <summary>
        /// Create a new speech bubble
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_position"></param>
        /// <param name="_size"></param>
        /// <param name="_pinch"></param>
        public SpeechBubble(Universe.Universe _universe, Vector2 _position, Vector2 _size, float _speed, SpriteFont _font, float _pinch)
            : base(_universe, 500)
        {
            Position = _position;
            Size = _size;
            Pinch = _pinch;

            // Generate the points
            GeneratePoints();

            // Create the textbox
            textBox = new RPGText.TextBoxDrawing(_size * 0.8f * 2, _speed, _font);

            // Create thes spritebatch
            spriteBatch = new SpriteBatch(Universe.GraphicsDevice);

            // Add the b button accessory
            var button = new Actors.Accessories.Buttons.BButton(Universe, this, Depth);
            button.Position = new Vector2((_size.X * 4) / 5, _size.Y / 2);
            button.AddToWearer();
        }

        /// <summary>
        /// Update the text box
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            textBox.UpdateText(_gameTime);

            Console.WriteLine(Accessories[0].Position);

            if (textBox.Paused)
            {
                Accessories[0].Visible = true;
            }
            else
            {
                Accessories[0].Visible = false;
            }
        }

        /// <summary>
        /// Draws the shape
        /// </summary>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            DrawShape(_camera, Triangle);
            DrawShape(_camera, Points);

            // Draw the text
            textBox.Position = Position;
            textBox.Position = Vector2.Transform(textBox.Position, _camera.CameraTransform);
            textBox.Position = new Vector2(textBox.Position.X, -textBox.Position.Y) + new Vector2(_graphicsDevice.Viewport.Width / 2, _graphicsDevice.Viewport.Height / 2);
            Vector2 oldScale = textBox.Scale;
            textBox.Scale = textBox.Scale * _camera.Scale;
            textBox.Draw(spriteBatch);
            textBox.Scale = oldScale;
        }

        /// <summary>
        /// Draws a list of points
        /// </summary>
        /// <param name="_points"></param>
        public void DrawShape(Camera.Camera _camera, List<Vector2> _points)
        {
            if (_points.Count > 2)
            {
                Matrix viewMatrix = _camera.CameraTransform;

                Matrix projectionMatrix = _camera.Projection;

                Matrix worldMatrix = Transform;

                EffectPassCollection passes;

                if (shader == null)
                {
                    shader = Universe.Content.Load<Effect>(@"Shaders/SpeechBubble");
                }

                shader.Parameters["World"].SetValue(worldMatrix);
                shader.Parameters["View"].SetValue(viewMatrix);
                shader.Parameters["Projection"].SetValue(projectionMatrix);

                passes = shader.CurrentTechnique.Passes;

                VertexPositionColor[] vertexes = new VertexPositionColor[_points.Count];
                for (int i = 0; i < _points.Count; i++)
                {
                    vertexes[i] = new VertexPositionColor(new Vector3(_points[i], 0), DebugColor);
                }

                short[] indexes = new short[3 * (_points.Count - 2)];
                for (int i = 0, j = 0; i < 3 * (_points.Count - 2); i += 3, j++)
                {
                    if (i == 0)
                    {
                        indexes[i] = (short)0;
                        indexes[i + 1] = (short)1;
                        indexes[i + 2] = (short)2;
                        j += 2;
                    }
                    else
                    {
                        indexes[i] = (short)0;
                        indexes[i + 1] = (short)(j - 1);
                        indexes[i + 2] = (short)j;
                    }
                }

                // Enable transparency
                Universe.GraphicsDevice.BlendState = BlendState.AlphaBlend;

                foreach (EffectPass pass in passes)
                {
                    pass.Apply();

                    Universe.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleList,
                                                                                            vertexes,
                                                                                            0,
                                                                                            _points.Count,
                                                                                            indexes,
                                                                                            0,
                                                                                            indexes.Length / 3);
                }
            }
        }

        /// <summary>
        /// Generate the speech bubble
        /// </summary>
        public virtual void GeneratePoints()
        {
            // Generate the points
            for (float i = 0; i <= 2 * (float)Math.PI; i += 0.1f)
            {
                Points.Add(new Vector2(size.X * Math.Sign(Math.Cos(i)) * (float)Math.Pow(Math.Abs(Math.Cos(i)), Pinch / 2), 
                                       size.Y * Math.Sign(Math.Sin(i)) * (float)Math.Pow(Math.Abs(Math.Sin(i)), Pinch / 2)));
            }
        }
    }
}
