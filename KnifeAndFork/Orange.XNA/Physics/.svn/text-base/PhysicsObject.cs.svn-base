using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Box2D.XNA;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Orange.XNA
{
    public class PhysicsObject
    {
        /// <summary>
        /// The position of this object
        /// </summary>
        public Vector2 _position;
        public virtual Vector2 position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                if (body != null)
                {
                    body.Position = position;
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
        public Vector2 _scale = new Vector2(1.0f, 1.0f);
        public virtual Vector2 scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
                if (body != null)
                    ChangeShape(shape);
            }
        }

        /// <summary>
        /// The angle of the object
        /// </summary>
        public float _rotation;
        public float rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
                if (body != null)
                {
                    body.Rotation = _rotation;
                }
            }
        }


        /// <summary>
        /// The width and height of the object
        /// </summary>
        public Vector2 size;

        /// <summary>
        /// The density of the object. Used to calculate mass.
        /// </summary>
        public float _density;
        public float density
        {
            get
            {
                return _density;
            }
            set
            {
                _density = value;
                if (body != null)
                    ChangeShape(shape);
            }
        }

        /// <summary>
        /// The amount of friction the object has.
        /// </summary>
        public float _friction;
        public float friction
        {
            get
            {
                return _friction;
            }
            set
            {
                _friction = value;
                if (body != null)
                    ChangeShape(shape);
            }
        }

        /// <summary>
        /// Restitution
        /// </summary>
        public float _restitution;
        public float restitution
        {
            get
            {
                return _restitution;
            }
            set
            {
                _restitution = value;
                if (body != null)
                    ChangeShape(shape);
            }
        }

        /// <summary>
        /// Not exactly like air friction but sort of.
        /// </summary>
        public float _linearDampening;
        public float linearDampening
        {
            get
            {
                return _linearDampening;
            }
            set
            {
                _linearDampening = value;
                if (body != null)
                {
                    body.SetLinearDamping(linearDampening);
                }
            }
        }

        public float _angularDampening;
        public float angularDampening
        {
            get
            {
                return _angularDampening;
            }
            set
            {
                _angularDampening = value;
                if (body != null)
                {
                    body.SetAngularDamping(angularDampening);
                }
            }
        }

        /// <summary>
        /// Proper air friction
        /// </summary>
        public float airFriction;

        /// <summary>
        /// Determines whether or not the object is a sensor
        /// </summary>
        public bool _sensor = false;
        public bool sensor
        {
            get
            {
                return _sensor;
            }

            set
            {
                if (value != _sensor)
                {
                    _sensor = value;
                    if (body != null)
                        ChangeShape(shape);
                }
            }
        }

        /// <summary>
        /// A pointer to the world this object will be in.
        /// </summary>
        public World world;

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
        public int PixelsToMetres = 50;

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
        /// <param name="_world"></param>
        public PhysicsObject(World _world)
        {
            world = _world;
            userData = this;
        }

        /// <summary>
        /// Removes a physical body from the world
        /// </summary>
        public void RemoveShape()
        {
            if (body != null)
            {
                world.DestroyBody(body);
                body = null;
            }
        }

        /// <summary>
        /// Changes the shape of the object
        /// </summary>
        /// <param name="_shape"></param>
        public void ChangeShape(Shapes _shape)
        {
            // Save shape for later
            shape = _shape;

            // Check if the world exists
            if (world == null)
                return;
            
            if (body == null)
            {
                // Create a new body definition
                BodyDef bodydef = new BodyDef();

                // Set the position
                bodydef.position = center*scale;

                // Set the user data
                bodydef.userData = userData;

                if (density > 0)
                    bodydef.type = BodyType.Dynamic;
                else
                    bodydef.type = BodyType.Static;

                // Add it to the world
                body = world.CreateBody(bodydef);
            }
            else
            {
                // Remove the fixture if body already exists.
                body.DestroyFixture(body.GetFixtureList());
            }

            // The shape
            if (_shape == Shapes.BOX)
            {
                // Define the shape
                PolygonShape polydef = new PolygonShape();
                FixtureDef fixtureDef = new FixtureDef();

                // Set as a box
                polydef.SetAsBox((size.X * scale.X) / 2, (size.Y * scale.Y) / 2);

                // Set up the fixture definition
                fixtureDef.shape = polydef;
                fixtureDef.friction = friction;
                fixtureDef.restitution = restitution;
                fixtureDef.density = density;
                fixtureDef.isSensor = sensor;

                // Add the shape to the body
                body.CreateFixture(fixtureDef);
            }

            if (_shape == Shapes.CIRCLE)
            {
                CircleShape circledef = new CircleShape();
                FixtureDef fixtureDef = new FixtureDef();

                circledef._radius = (size.X*scale.X)/2;

                fixtureDef.shape = circledef;
                fixtureDef.friction = friction;
                fixtureDef.restitution = restitution;
                fixtureDef.density = density;
                fixtureDef.isSensor = sensor;

                body.CreateFixture(fixtureDef);
            }

            // Update position and rotation
            body.Position = position;
            body.Rotation = rotation;
            body.SetLinearDamping(linearDampening);
            body.SetAngularDamping(angularDampening);
            body.SetUserData(userData);
        }

        /// <summary>
        /// Upates the object, call this every frame, AFTER stepping the world.
        /// </summary>
        /// <param name="_fps"></param>
        /// <param name="_velIterations"></param>
        /// <param name="_posIterations"></param>
        public void Step()
        {
            // Get the transform
            Transform transform;
            body.GetTransform(out transform);

            //center = new Vector2(body.GetLocalCenter().X, body.GetLocalCenter().Y);
            _position = transform.Position * PixelsToMetres;
            _rotation = transform.R.GetAngle();

            // Simulate some air friction
            // X
            float xAirForce;
            if (body.GetLinearVelocity().X > 0)
            {
                xAirForce = -1 * airFriction * (float)System.Math.Pow(body.GetLinearVelocity().X, 2);
            }
            else
            {
                xAirForce = 1 * airFriction * (float)System.Math.Pow(body.GetLinearVelocity().X, 2);
            }

            // Y
            float yAirForce;
            if (body.GetLinearVelocity().Y > 0)
            {
                yAirForce = -1 * airFriction * (float)System.Math.Pow(body.GetLinearVelocity().Y, 2);
            }
            else
            {
                yAirForce = 1 * airFriction * (float)System.Math.Pow(body.GetLinearVelocity().Y, 2);
            }

            // Apply this force
            body.ApplyForce(new Vector2(xAirForce, yAirForce), new Vector2(0.0f, 0.0f));
        }
    }
}
