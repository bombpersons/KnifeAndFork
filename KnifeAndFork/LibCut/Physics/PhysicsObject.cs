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

namespace LibCut.Physics
{
    public class PhysicsObject
    {
        /// <summary>
        /// The Position of this object
        /// </summary>
        public Vector2 position;
        public virtual Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                if (body != null)
                {
                    body.Position = Position;
                }
            }
        }


        /// <summary>
        /// The center of the sprite. All transformations will take place around this point.
        /// The default value will be the center of the object.
        /// </summary>
        public Vector2 center;

        /// <summary>
        /// Scale of the object
        /// </summary>
        public Vector2 scale = new Vector2(1.0f, 1.0f);
        public virtual Vector2 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }

        /// <summary>
        /// The angle of the object
        /// </summary>
        public float rotation;
        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                if (body != null)
                {
                    body.Rotation = rotation;
                }
            }
        }


        /// <summary>
        /// The width and height of the object
        /// </summary>
        public Vector2 size;
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
        /// The Density of the object. Used to calculate mass.
        /// </summary>
        public float density;
        public float Density
        {
            get
            {
                return density;
            }
            set
            {
                density = value;
            }
        }

        /// <summary>
        /// The amount of Friction the object has.
        /// </summary>
        public float friction;
        public float Friction
        {
            get
            {
                return friction;
            }
            set
            {
                friction = value;
            }
        }

        /// <summary>
        /// Restitution
        /// </summary>
        public float restitution;
        public float Restitution
        {
            get
            {
                return restitution;
            }
            set
            {
                restitution = value;
            }
        }

        /// <summary>
        /// Not exactly like air Friction but sort of.
        /// </summary>
        public float linearDampening;
        public float LinearDampening
        {
            get
            {
                return linearDampening;
            }
            set
            {
                linearDampening = value;
                if (body != null)
                {
                    body.SetLinearDamping(LinearDampening);
                }
            }
        }

        public float angularDampening;
        public float AngularDampening
        {
            get
            {
                return angularDampening;
            }
            set
            {
                angularDampening = value;
                if (body != null)
                {
                    body.SetAngularDamping(AngularDampening);
                }
            }
        }

        /// <summary>
        /// Determines whether or not the object is a Sensor
        /// </summary>
        public bool sensor = false;
        public bool Sensor
        {
            get
            {
                return sensor;
            }

            set
            {
                if (value != sensor)
                {
                    sensor = value;
                }
            }
        }

        /// <summary>
        /// A pointer to the TheWorld this object will be in.
        /// </summary>
        public World TheWorld;

        /// <summary>
        /// The current shape.
        /// </summary>
        public Shapes shape = Shapes.BOX;

        /// <summary>
        /// A pointer to the body structure of the object.
        /// </summary>
        public Body body;

        /// <summary>
        /// User data. By default this is a pointer to this instance of the class.
        /// </summary>
        public object userData;

        /// <summary>
        /// Defines possible shapes the objects collision can use.
        /// </summary>
        public enum Shapes
        {
            BOX,
            CIRCLE,
            POLYGON,
        }

        /// <summary>
        /// Pixels to metres conversion rate
        /// </summary>
        static public float PixelsToMetres = 50.0f;

        /// <summary>
        /// Constructor does nothing.
        /// </summary>
        public PhysicsObject()
        {
            userData = this;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~PhysicsObject()
        {
            RemoveShape();
        }

        /// <summary>
        /// Another destrcutor
        /// </summary>
        public void Dispose()
        {
            RemoveShape();
        }

        /// <summary>
        /// Initializes the physics object
        /// </summary>
        /// <param name="_TheWorld"></param>
        public PhysicsObject(World _TheWorld)
        {
            TheWorld = _TheWorld;
            userData = this;
        }

        /// <summary>
        /// Removes a physical body from the TheWorld
        /// </summary>
        public void RemoveShape()
        {
            if (body != null)
            {
                TheWorld.DestroyBody(body);
                body = null;
            }
        }

        /// <summary>
        /// Adds a fixture to the body
        /// </summary>
        public bool AddFixture(Shapes _shape, Vector2[] _points)
        {
            // Adds a fixture to the current body
            if (body != null)
            {
                // Check the mass isn't too big
                if (_points != null)
                {
                    MassData mass = TestMass(_points);
                    if (mass.I < 0.0f)
                        return false;
                    if (mass.mass < 0.002f)
                        return false;
                    if (mass.mass * Density < 1.192092896e-06f)
                        return false;
                }

                // The shape is a polygon
                if (_shape == Shapes.POLYGON)
                {
                    PolygonShape poly = new PolygonShape();
                    List<Vector2> arr = new List<Vector2>();
                    for (int i = 0; i < _points.Length; i++)
                    {
                        if (!arr.Contains(_points[i] / PixelsToMetres))
                            arr.Add(_points[i] / PixelsToMetres);
                    }

                    poly.Set(arr.ToArray(), arr.Count);

                    FixtureDef fixtureDef = new FixtureDef();

                    fixtureDef.shape = poly;
                    fixtureDef.friction = Friction;
                    fixtureDef.restitution = Restitution;
                    fixtureDef.density = Density;
                    fixtureDef.isSensor = Sensor;

                    body.CreateFixture(fixtureDef);
                }

                // The shape
                if (_shape == Shapes.BOX)
                {
                    // Define the shape
                    PolygonShape polydef = new PolygonShape();
                    FixtureDef fixtureDef = new FixtureDef();

                    // Set as a box
                    polydef.SetAsBox((size.X * Scale.X) / (2 * PixelsToMetres), (size.Y * Scale.Y) / (2 * PixelsToMetres));

                    // Set up the fixture definition
                    fixtureDef.shape = polydef;
                    fixtureDef.friction = Friction;
                    fixtureDef.restitution = Restitution;
                    fixtureDef.density = Density;
                    fixtureDef.isSensor = Sensor;

                    // Add the shape to the body
                    body.CreateFixture(fixtureDef);
                }

                if (_shape == Shapes.CIRCLE)
                {
                    CircleShape circledef = new CircleShape();
                    FixtureDef fixtureDef = new FixtureDef();

                    circledef._radius = (size.X * Scale.X) / (2 * PixelsToMetres);

                    fixtureDef.shape = circledef;
                    fixtureDef.friction = Friction;
                    fixtureDef.restitution = Restitution;
                    fixtureDef.density = Density;
                    fixtureDef.isSensor = Sensor;

                    body.CreateFixture(fixtureDef);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Tests the mass of an object
        /// </summary>
        /// <param name="_points"></param>
        /// <returns></returns>
        public MassData TestMass(Vector2[] _points)
        {
            if (_points.Length > 8)
                return new MassData();

            PolygonShape poly = new PolygonShape();
            List<Vector2> arr = new List<Vector2>();
            for (int i = 0; i < _points.Length; i++)
            {
                if (!arr.Contains(_points[i] / PixelsToMetres))
                    arr.Add(_points[i] / PixelsToMetres);
            }

            poly.Set(arr.ToArray(), arr.Count);

            MassData data = new MassData();
            poly.ComputeMass(out data, Density);

            return data;
        }

        public bool CreateBody()
        {
            // Check if the TheWorld exists
            if (TheWorld == null)
                return false;

            if (body == null)
            {
                // Create a new body definition
                BodyDef bodydef = new BodyDef();

                // Set the Position
                bodydef.position = (center * Scale) / PixelsToMetres;

                // Set the user data
                bodydef.userData = userData;

                if (Density > 0)
                    bodydef.type = BodyType.Dynamic;
                else
                    bodydef.type = BodyType.Static;

                // Add it to the TheWorld
                body = TheWorld.CreateBody(bodydef);
            }
            else
            {
                // Remove the fixture if body already exists.
                body.DestroyFixture(body.GetFixtureList());
            }

            // Update Position and Rotation
            body.Position = Position / PixelsToMetres;
            body.Rotation = Rotation;
            body.SetLinearDamping(LinearDampening);
            body.SetAngularDamping(AngularDampening);
            body.SetUserData(userData);

            return true;
        }

        /// <summary>
        /// Changes the shape of the object
        /// </summary>
        /// <param name="_shape"></param>
        public bool ChangeShape(Shapes _shape, Vector2[] _points)
        {
            // Check if the TheWorld exists
            if (TheWorld == null)
                return false;

            // Check the mass isn't too big
            if (_points != null)
            {
                MassData mass = TestMass(_points);
                if (mass.I < 0.0f)
                    return false;
                if (mass.mass < 0.002f)
                    return false;
                if (mass.mass * Density < 1.192092896e-06f)
                    return false;
            }

            if (body == null)
            {
                // Create a new body definition
                BodyDef bodydef = new BodyDef();

                // Set the Position
                bodydef.position = (center * Scale) / PixelsToMetres;

                // Set the user data
                bodydef.userData = userData;

                if (Density > 0)
                    bodydef.type = BodyType.Dynamic;
                else
                    bodydef.type = BodyType.Static;

                // Add it to the TheWorld
                body = TheWorld.CreateBody(bodydef);
            }
            else
            {
                // Remove the fixture if body already exists.
                body.DestroyFixture(body.GetFixtureList());
            }

            // The shape is a polygon
            if (_shape == Shapes.POLYGON)
            {
                PolygonShape poly = new PolygonShape();
                List<Vector2> arr = new List<Vector2>();
                for (int i = 0; i < _points.Length; i++)
                {
                    if (!arr.Contains(_points[i] / PixelsToMetres))
                        arr.Add(_points[i] / PixelsToMetres);
                }

                poly.Set(arr.ToArray(), arr.Count);

                FixtureDef fixtureDef = new FixtureDef();

                fixtureDef.shape = poly;
                fixtureDef.friction = Friction;
                fixtureDef.restitution = Restitution;
                fixtureDef.density = Density;
                fixtureDef.isSensor = Sensor;
                fixtureDef.userData = userData;

                body.CreateFixture(fixtureDef);
            }

            // The shape
            if (_shape == Shapes.BOX)
            {
                // Define the shape
                PolygonShape polydef = new PolygonShape();
                FixtureDef fixtureDef = new FixtureDef();

                // Set as a box
                polydef.SetAsBox((size.X * Scale.X) / (2 * PixelsToMetres), (size.Y * Scale.Y) / (2 * PixelsToMetres));

                // Set up the fixture definition
                fixtureDef.shape = polydef;
                fixtureDef.friction = Friction;
                fixtureDef.restitution = Restitution;
                fixtureDef.density = Density;
                fixtureDef.isSensor = Sensor;
                fixtureDef.userData = userData;

                // Add the shape to the body
                body.CreateFixture(fixtureDef);
            }

            if (_shape == Shapes.CIRCLE)
            {
                CircleShape circledef = new CircleShape();
                FixtureDef fixtureDef = new FixtureDef();

                circledef._radius = (size.X * Scale.X) / (2 * PixelsToMetres);

                fixtureDef.shape = circledef;
                fixtureDef.friction = Friction;
                fixtureDef.restitution = Restitution;
                fixtureDef.density = Density;
                fixtureDef.isSensor = Sensor;
                fixtureDef.userData = userData;

                body.CreateFixture(fixtureDef);
            }

            // Update Position and Rotation
            body.Position = Position / PixelsToMetres;
            body.Rotation = Rotation;
            body.SetLinearDamping(LinearDampening);
            body.SetAngularDamping(AngularDampening);
            body.SetUserData(userData);

            return true;
        }

        /// <summary>
        /// Upates the object, call this every frame, AFTER stepping the TheWorld.
        /// </summary>
        /// <param name="_fps"></param>
        /// <param name="_velIterations"></param>
        /// <param name="_posIterations"></param>
        public Transform Step()
        {
            if (body != null)
            {
                // Get the transform
                Transform transform;
                body.GetTransform(out transform);

                //center = new Vector2(body.GetLocalCenter().X, body.GetLocalCenter().Y);
                position = transform.Position * PixelsToMetres;
                rotation = transform.R.GetAngle();

                return transform;
            }

            return new Transform();
        }
    }
}
