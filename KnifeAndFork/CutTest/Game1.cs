using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Box2D.XNA;

namespace CutTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseState oldMouse = Mouse.GetState();
        MouseState mouse = Mouse.GetState();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 720;

            //graphics.IsFullScreen = true;

            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        List<LibCut.Shapes.PhysicsShape> shapes = new List<LibCut.Shapes.PhysicsShape>();
        LibCut.Physics.PhysicsObject bounds;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            camera = new LibCut.Camera.DebugCamera(null, GraphicsDevice, Vector2.Zero);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < 2; i++)
            {
                points[i] = new LibCut.Things.Sprite.Sprite(null, new Orange.XNA.Sprite(Content, "TestPoint"), 0);
            }

            World world = new World(new Vector2(0, 9.81f), true);

            bounds = new LibCut.Physics.PhysicsObject();
            bounds.Size = new Vector2(GraphicsDevice.Viewport.Width * 100, 10);
            bounds.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height);
            bounds.Friction = 0.4f;
            bounds.TheWorld = world;
            bounds.ChangeShape(LibCut.Physics.PhysicsObject.Shapes.BOX, null);

            shapes.Add(new LibCut.Shapes.PhysicsShape(null));
            shapes[0].SetWorld(world);
            //shapes[0].Points.Add(new Vector2(-100, -100));
            //shapes[0].Points.Add(new Vector2(-100, 100));
            //shapes[0].Points.Add(new Vector2(100, 100));
            //shapes[0].Points.Add(new Vector2(100, -100));
            shapes[0].Sort();

            //shapes[0].Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            shapes[0].Shader = Content.Load<Effect>("CutShader");
            shapes[0].Tex = Content.Load<Texture2D>("CutTestTexture");
            
            //camera.Position = new Vector2((float)GraphicsDevice.Viewport.Width / 2,
            //                             (float)GraphicsDevice.Viewport.Height / 2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        static LibCut.Things.Sprite.Sprite[] points = new LibCut.Things.Sprite.Sprite[2];

        Vector2[] cut = new Vector2[2];

        LibCut.Camera.Camera camera;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            camera.HandleInput(gameTime, null);

            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                //camera.Position += new Vector2(0.05f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
               // camera.Rotation += 0.005f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                //camera.Scale *= 1.005f;
            }

            oldMouse = mouse;
            mouse = Mouse.GetState();

            if (mouse.MiddleButton == ButtonState.Pressed)
            {
                points[0].Position = camera.GetWorldCoord(GraphicsDevice, new Vector2(mouse.X, mouse.Y));
                
            }

            Console.WriteLine(points[0].Position);

            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                Vector2 m = camera.GetWorldCoord(GraphicsDevice, new Vector2(mouse.X, mouse.Y));
                //m = Vector2.Transform(m, camera.Transform);
                shapes[0].Points.Add(m);
                shapes[0].Sort();
                shapes[0].InitTextureStuff();
            }

            if (mouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Released)
            {
                cut[0] = new Vector2(mouse.X, mouse.Y);
                points[0].Position = camera.GetWorldCoord(GraphicsDevice, cut[0]);
            }

            if (mouse.RightButton == ButtonState.Released && oldMouse.RightButton == ButtonState.Pressed)
            {
                cut[1] = new Vector2(mouse.X, mouse.Y);
                points[1].Position = camera.GetWorldCoord(GraphicsDevice, cut[1]);
                cut[0] = camera.GetWorldCoord(GraphicsDevice, cut[0]);
                cut[1] = camera.GetWorldCoord(GraphicsDevice, cut[1]);
                foreach (LibCut.Shapes.PhysicsShape s in shapes.ToArray())
                {
                    //points[0].Position = Vector2.Transform(cut[0], s.Transform);
                    //points[1].Position = Vector2.Transform(cut[1], s.Transform);

                    LibCut.Shapes.PhysicsShape[] t = s.Cut(cut[0], cut[1]);
                    if (t != null)
                    {
                        if (t[0] != null)
                        {
                            shapes.Add(t[0]);
                            shapes.Add(t[1]);
                            s.DestroyBody();
                            shapes.Remove(s);
                        }
                    }
                }
            }

            // Update the world
            shapes[0].TheWorld.Step((float)gameTime.ElapsedGameTime.TotalSeconds, 3, 8);

            // Update the shapes
            foreach (LibCut.Shapes.PhysicsShape p in shapes)
            {
                p.Update(gameTime);
            }

            bounds.Step();

            //camera.Position = shapes[0].Position;
            //camera.Rotation = shapes[0].Rotation;

            for (Body body = shapes[0].TheWorld.GetBodyList(); body.GetNext() != null; body = body.GetNext())
            {
                //Console.WriteLine(body.GetAngle());
                //Console.WriteLine(body.GetPosition() * LibCut.Physics.PhysicsObject.PixelsToMetres);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Orange);

            // TODO: Add your drawing code here
            foreach (LibCut.Shapes.PhysicsShape shape in shapes)
            {
                shape.Draw(camera, GraphicsDevice);
            }

            // Draw the points
            foreach (LibCut.Things.Sprite.Sprite sprite in points)
            {
                sprite.Draw(camera, GraphicsDevice);
            }

            /*if (shapes != null && shapes[0] != null)
            {
                shapes[0].DebugColor = Color.White;
                shapes[1].DebugColor = Color.Black;

                foreach (LibCut.Shapes.Shape s in shapes)
                {
                    s.DrawDebug(GraphicsDevice);
                }
            }*/

            base.Draw(gameTime);
        }
    }
}
