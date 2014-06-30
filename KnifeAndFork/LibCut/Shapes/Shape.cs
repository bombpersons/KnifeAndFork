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

namespace LibCut.Shapes
{
    /// <summary>
    /// A generic class for a shape
    /// </summary>
    public class Shape : Things.Thing
    {
        /// <summary>
        /// Dispose of the texture when we die
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
                //if (value)
                    //tex.Dispose();
            }
        }
        
        /// <summary>
        /// The points of the shape
        /// </summary>
        protected List<Vector2> points = new List<Vector2>();
        public List<Vector2> Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }

        /// <summary>
        /// Gets the point after matrix multiplacation
        /// </summary>
        /// <param name="_i"></param>
        /// <returns></returns>
        public Vector2 GetPoint(int _i)
        {
            Vector2 ret = points[_i];
            //5Vector2.Transform(ret, Transform);
            return ret;
        }

        /// <summary>
        /// Gets the center of the shape
        /// </summary>
        public Vector2 Center
        {
            get
            {
                if (points.Count > 0)
                {
                    // The size will be the distance between the max and min points
                    float minX = points[0].X, minY = points[0].Y, maxX = points[0].X, maxY = points[0].Y;
                    foreach (Vector2 point in points)
                    {
                        if (point.X > maxX)
                        {
                            maxX = point.X;
                        }

                        if (point.X < minX)
                        {
                            minX = point.X;
                        }

                        if (point.Y > maxY)
                        {
                            maxY = point.Y;
                        }

                        if (point.Y < minY)
                        {
                            minY = point.Y;
                        }
                    }
                    return new Vector2(minX + ((maxX - minX) / 2), minY + ((maxY - minY) / 2));
                }
                return Vector2.Zero;
            }
        }

        /// <summary>
        /// The shader to use to draw the shape
        /// </summary>
        protected Effect shader;
        public Effect Shader
        {
            get
            {
                return shader;
            }
            set
            {
                shader = value;
            }
        }

        /// <summary>
        /// The red alpha
        /// </summary>
        protected float redAlpha;
        public float RedAlpha
        {
            get
            {
                return redAlpha;   
            }
            set
            {
                redAlpha = value;
            }
        }

        /// <summary>
        /// The size of the shape
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
        /// The center for the texture
        /// </summary>
        protected Vector2 textureCenter;
        public Vector2 TextureCenter
        {
            get
            {
                return textureCenter;
            }
            set
            {
                textureCenter = value;
            }
        }

        /// <summary>
        /// The texture to draw
        /// </summary>
        protected Texture2D tex;
        public Texture2D Tex
        {
            get
            {
                return tex;
            }
            set
            {
                tex = value;
            }
        }

        /// <summary>
        /// Whether or not the shape is cuttable
        /// </summary>
        protected bool cuttable = true;
        public bool Cuttable
        {
            get
            {
                return cuttable;
            }
            set
            {
                cuttable = value;
            }
        }

        /// <summary>
        /// The sound to play when we get cut.
        /// </summary>
        protected SoundEffect sound;
        public SoundEffect Sound
        {
            get
            {
                return sound;
            }
            set
            {
                sound = value;
            }
        }


        /// <summary>
        /// The sound to play when we fail to get cut
        /// </summary>
        protected SoundEffect failSound;
        public SoundEffect FailSound
        {
            get
            {
                return failSound;
            }
            set
            {
                failSound = value;
            }
        }


        /// <summary>
        /// The effect to use
        /// </summary>
        protected Type effect = typeof(Things.Effects.Cut);
        public Type Effect
        {
            get
            {
                return effect;
            }
            set
            {
                effect = value;
            }
        }

        /// <summary>
        /// The effect to use when cutting fails
        /// </summary>
        protected Type failEffect = typeof(Things.Effects.Cut);
        public Type FailEffect
        {
            get
            {
                return failEffect;
            }
            set
            {
                failEffect = value;
            }
        }


        /// <summary>
        /// The sound to play when exploding
        /// </summary>
        protected SoundEffect explodeSound;
        public SoundEffect ExplodeSound
        {
            get
            {
                return explodeSound;
            }
            set
            {
                explodeSound = value;
            }
        }

        /// <summary>
        /// Blank constructor
        /// </summary>
        public Shape(Universe.Universe _universe)
            : base(_universe)
        {
            ExplodeSound = Universe.Content.Load<SoundEffect>(@"Sound/Explode");
        }

        /// <summary>
        /// Creates a shape with depth
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_depth"></param>
        public Shape(Universe.Universe _universe, int _depth)
            : base(_universe, _depth)
        {
        }

        /// <summary>
        /// Creates info for the textures
        /// </summary>
        public void InitTextureStuff()
        {
            // Create the size and textureCenter

            // The size will be the distance between the max and min points
            float minX = points[0].X, minY = points[0].Y, maxX = points[0].X, maxY = points[0].Y;
            foreach (Vector2 point in points)
            {
                if (point.X > maxX)
                {
                    maxX = point.X;
                }

                if (point.X < minX)
                {
                    minX = point.X;
                }

                if (point.Y > maxY)
                {
                    maxY = point.Y;
                }

                if (point.Y < minY)
                {
                    minY = point.Y;
                }
            }

            // We can calculate the size now
            Size = new Vector2(maxX - minX, maxY - minY);

            // Set the texture center to the middle of the shape
            TextureCenter = Center;
        }

        /// <summary>
        /// Update the cuttable property
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
        }

        /// <summary>
        /// Draws the shape
        /// </summary>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            if (points.Count > 2)
            {
                Matrix viewMatrix = _camera.CameraTransform;

                Matrix projectionMatrix = _camera.Projection;

                Matrix worldMatrix = Transform;

                EffectPassCollection passes;

                if (shader == null)
                {
                    shader = Universe.Content.Load<Effect>("CutShader");
                }

                shader.Parameters["View"].SetValue(viewMatrix);
                shader.Parameters["Projection"].SetValue(projectionMatrix);
                shader.Parameters["World"].SetValue(worldMatrix);

                shader.Parameters["Center"].SetValue(textureCenter);
                shader.Parameters["Size"].SetValue(size);
                shader.Parameters["Texture"].SetValue(tex);
                if (RedAlpha > 0)
                {
                    Vector4 color = (1 - RedAlpha) * Color.White.ToVector4() + RedAlpha * Color.Red.ToVector4();
                    shader.Parameters["Tint"].SetValue(color);
                }
                else
                    shader.Parameters["Tint"].SetValue(Color.White.ToVector4());

                passes = shader.CurrentTechnique.Passes;

                VertexPositionColor[] vertexes = new VertexPositionColor[points.Count];
                for (int i = 0; i < points.Count; i++)
                {
                    vertexes[i] = new VertexPositionColor(new Vector3(points[i], 0), DebugColor);
                }

                short[] indexes = new short[3 * (points.Count - 2)];
                for (int i = 0, j = 0; i < 3 * (points.Count - 2); i += 3, j++)
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
                _graphicsDevice.BlendState = BlendState.AlphaBlend;

                foreach (EffectPass pass in passes)
                {
                    pass.Apply();

                    _graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleList,
                                                                                    vertexes,
                                                                                    0,
                                                                                    points.Count,
                                                                                    indexes,
                                                                                    0,
                                                                                    indexes.Length / 3);
                }
            }
        }

        /// <summary>
        /// The color to draw in with debug
        /// </summary>
        Color debugColor = Color.Red;
        public Color DebugColor
        {
            get
            {
                return debugColor;
            }
            set
            {
                debugColor = value;
            }
        }

        // Cuts the shape into two shapes
        public virtual Shape[] Cut(Vector2 _pos1, Vector2 _pos2)
        {
            // If the shape isn't cuttable then return null
            if (!Cuttable)
            {
                return null;
            }

            // Tranform the input vectors
            Matrix inverse = Matrix.Invert(Transform);
            _pos1 = Vector2.Transform(_pos1, inverse);
            _pos2 = Vector2.Transform(_pos2, inverse);

            // First get the intersect points. There is no point
            // in continuing if there aren't atleast 2 intersects
            IntersectResult[] intersects;
            if ((intersects = GetIntersectPoints(_pos1, _pos2)).Length >= 2)
            {
                // Create 2 shapes
                Shape[] shapes = new Shape[2];
                for (int i = 0; i < shapes.Length; i++)
                {
                    shapes[i] = new Shape(Universe);

                    // Add the intersects to the points first
                    shapes[i].Points.Add(intersects[0].IntersectPoint);
                    shapes[i].Points.Add(intersects[1].IntersectPoint);
                }

                // Find which points are on which side and add them to the appropriate shape
                for (int i = 0; i < points.Count; i++)
                {
                    bool side = SideOfCut(_pos1, _pos2, GetPoint(i));
                    if (side)
                    {
                        shapes[0].Points.Add(GetPoint(i));
                    }
                    else
                    {
                        shapes[1].Points.Add(GetPoint(i));
                    }
                }

                // Find out which accessories are on wich side as well
                for (int i = 0; i < Accessories.Count; i++)
                {
                    bool side = SideOfCut(_pos1, _pos2, accessories[i].Clamp);
                    if (side)
                    {
                        shapes[0].Accessories.Add(accessories[i]);
                        accessories[i].Wearer = shapes[0];
                    }
                    else
                    {
                        shapes[1].Accessories.Add(accessories[i]);
                        accessories[i].Wearer = shapes[1];
                    }
                }

                // Sort the shapes
                shapes[0].Sort();
                shapes[1].Sort();

                // Now pass along some values
                for (int i = 0; i < shapes.Length; i++)
                {
                    shapes[i].Shader = Shader;
                    shapes[i].TextureCenter = TextureCenter;
                    shapes[i].Tex = Tex;
                    shapes[i].Size = size;
                    shapes[i].Rotation = Rotation;
                    shapes[i].Position = Position;
                    shapes[i].Scale = Scale;
                    shapes[i].Sound = Sound;
                    shapes[i].Effect = Effect;
                    shapes[i].FailEffect = FailEffect;
                    shapes[i].FailSound = FailSound;
                }

                // Make ourselves dead
                //Dead = true;

                return shapes;
            }

            return null;
        }

        /// <summary>
        /// Sorts all the points in clockwise order. (Box2d likes polygons like this)
        /// </summary>
        public void Sort()
        {
            ClockwiseComparer comp = new ClockwiseComparer();
            comp.Center = Center;

            points.Sort(comp);
        }

        /// <summary>
        /// Finds out which side of the cut the point is at
        /// </summary>
        /// <param name="_pos1"></param>
        /// <param name="_pos2"></param>
        /// <param name="_pos"></param>
        /// <returns></returns>
        static protected bool SideOfCut(Vector2 _pos1, Vector2 _pos2, Vector2 _pos)
        {
            // The line
            Vector2 line = _pos2 - _pos1;

            // First get the normal of this line.
            // Then we can use it to create an equation
            // for any point on one side of the line

            // The normal of a 2d vector is as follows
            Vector2 normal = new Vector2(-1 * line.Y, line.X);

            // The sign of t will tell us what side the point is on
            float t = Cross((_pos1 - _pos), line) / Cross(normal, line);

            return (t > 0);
        }

        /// <summary>
        /// Gets the points where a line intersects with the shape
        /// </summary>
        /// <param name="_pos1"></param>
        /// <param name="_pos2"></param>
        /// <returns></returns>
        public IntersectResult[] GetIntersectPoints(Vector2 _pos1, Vector2 _pos2)
        {
            List<IntersectResult> result = new List<IntersectResult>();

            // We need to account for rotation, scale and translation
            // So times all these points by our matrixes before doing anything with them
            for (int i = 0; i < points.Count; i++)
            {
                Vector2 temp = new Vector2();
                if (IntersectLine(GetPoint(i % points.Count), GetPoint((i + 1) % points.Count), _pos1, _pos2, ref temp) == 1)
                {
                    result.Add(new IntersectResult(temp, new int[] { i % points.Count, (i + 1) % points.Count }));
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Used to provide more information about the intersect
        /// </summary>
        public struct IntersectResult
        {
            public IntersectResult(Vector2 _vec, int[] _indexes)
            {
                intersectPoint = _vec;
                vectorIndexes = _indexes;
            }

            Vector2 intersectPoint; // The intersection point
            public Vector2 IntersectPoint
            {
                get
                {
                    return intersectPoint;
                }
                set
                {
                    intersectPoint = value;
                }
            }
            int[] vectorIndexes; // The index of the line that this intersection occurred at
            public int[] VectorIndexes
            {
                get
                {
                    return vectorIndexes;
                }
                set
                {
                    vectorIndexes = value;
                }
            }
        }

        /// <summary>
        /// Gets the union of two shapes
        /// </summary>
        /// <param name="_shape1"></param>
        /// <param name="_shape2"></param>
        static public List<Vector2> Union(Vector2[] _shape1, Vector2[] _shape2)
        {
            return null;
        }

        /// <summary>
        /// Calculates the intersect of a line
        /// </summary>
        /// <param name="_line1a"></param>
        /// <param name="_line1b"></param>
        /// <param name="_line2a"></param>
        /// <param name="_line2b"></param>
        /// <param name="_out"></param>
        /// <returns></returns>
        static public int IntersectLine(Vector2 _line1a, Vector2 _line1b, Vector2 _line2a, Vector2 _line2b, ref Vector2 _out)
        {
            float cross = Cross((_line2b - _line2a), (_line1b - _line1a));
            if (cross == 0)
            {
                // Pararell or colinear
                return 0;
            }

            float t, s;
            t = Cross(_line1a - _line2a, _line2b - _line2a) / cross;
            s = Cross(_line1a - _line2a, _line1b - _line1a) / cross;

            // Check if the lines intersected
            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                _out = _line1a + (_line1b - _line1a) * t;
                return 1;
            }
            else
            {
                // No intersect
                return -1;
            }
        }

        /// <summary>
        /// Calculates the cross product of two 2dvectors
        /// </summary>
        /// <param name="_u"></param>
        /// <param name="_v"></param>
        /// <returns></returns>
        static public float Cross(Vector2 _u, Vector2 _v)
        {
            return (_u.X * _v.Y) - (_v.X * _u.Y);
        }

        /// <summary>
        /// Display some useful information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "";
            foreach (Vector2 point in Points)
            {
                result += point.ToString() + "\n";
            }
            return result;
        }
    }
}
