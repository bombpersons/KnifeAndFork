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

namespace LibCut.Things.DebugItems.ShapeMaker
{
    /// <summary>
    /// Takes input and creates shapes and stuff =0
    /// </summary>
    public class ShapeMaker : Things.Thing
    {
        /// <summary>
        /// The current shape being edited
        /// </summary>
        Actors.Actor currentActor;

        /// <summary>
        /// A list of textures to switch between
        /// </summary>
        int currentTexture = 0;
        string[] Textures = { @"Food/ChickenNugget",
                              @"Food/Ham",
                              @"Food/Kimchi",
                              @"Food/Cheese",
                              @"Food/Pie",
                              @"Food/Carrot",
                              @"Food/Coconut",
                              @"Food/Bread",
                              @"Food/CupCake",
                             };

        /// <summary>
        /// A list of accessories to switch between
        /// </summary>
        int currentAcessory = 0;
        bool accessoryPlaced = false;
        Actors.Accessories.Accessory Accessory = null;
        new Type[] Accessories = { typeof(Actors.Accessories.Wings.SmallWings),
                                   typeof(Actors.Accessories.Wings.BigWings),
                                   typeof(Actors.Accessories.Eyes.PeacefulEyes), 
                                   typeof(Actors.Accessories.Eyes.EvenMorePeacefulEyes),
                                   typeof(Actors.Accessories.Mouths.ImmaFirinMaLazar),
                                   typeof(Actors.Accessories.Shooters.HamThrower),
                                   typeof(Actors.Accessories.Utility.SelfRighter),
                                   typeof(Actors.Accessories.Utility.Targetter),
                                   typeof(Actors.Accessories.Thrusters.SimpleThruster),
                                   typeof(Actors.Accessories.Thrusters.RocketThruster),
                                   typeof(Actors.Accessories.Eyes.FastEyes),
                                   typeof(Actors.Accessories.Utility.Shield),
                                   typeof(Actors.Accessories.Mouths.GrumpyFace),
                                   typeof(Actors.Accessories.Melee.Spike),
                                   typeof(Actors.Accessories.Mouths.BigTalkingBreadFace),
                                   typeof(Actors.Accessories.Menu.Tutorial),
                                   typeof(Actors.Accessories.Menu.NewGame),
                                   typeof(Actors.Accessories.Mouths.MainMenuFace),
                                   typeof(Actors.Accessories.Eyes.BlueEyes),
                                   typeof(Actors.Accessories.Melee.Fuse),
                                 };

        /// <summary>
        /// Holds values for cuts
        /// </summary>
        Vector2[] cut = new Vector2[2];

        /// <summary>
        /// Creates a new ShapeMaker.
        /// </summary>
        /// <param name="_universe"></param>
        public ShapeMaker(Universe.Universe _universe, int _depth)
            : base(_universe, _depth)
        {
            // Create a shape to start of with
            currentActor = new Actors.Actor(Universe, Vector2.Zero);
            currentActor.Shape.Tex = Universe.Content.Load<Texture2D>(Textures[currentTexture]);
            currentActor.Shape.Tex.Name = Textures[currentTexture];
        }

        /// <summary>
        /// Updatet the position
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
        }

        /// <summary>
        /// Takes input
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);

            // Enable cutting
            if (_input.Keyboard.IsKeyDown(Keys.LeftControl))
            {
                if (_input.ClickedLeftButton)
                {
                    cut[0] = Universe.Camera.GetWorldCoord(Universe.GraphicsDevice, new Vector2(_input.Mouse.X, _input.Mouse.Y));
                }
                if (_input.ReleasedLeftButton)
                {
                    cut[1] = Universe.Camera.GetWorldCoord(Universe.GraphicsDevice, new Vector2(_input.Mouse.X, _input.Mouse.Y));

                    // Do the cut
                    foreach (Thing thing in Universe.Things.ToArray())
                    {
                        if (thing is Shapes.PhysicsShape)
                        {
                            Shapes.PhysicsShape shape = (Shapes.PhysicsShape)thing;
                            shape.Cut(cut[0], cut[1]);
                        }
                    }
                }
            }
            else if (_input.ClickedLeftButton)
            {
                // Add a point to the current shape
                if (currentActor.Shape != null)
                {
                    currentActor.Shape.Points.Add(Universe.Camera.GetWorldCoord(Universe.GraphicsDevice, new Vector2(_input.Mouse.X, _input.Mouse.Y)));
                    currentActor.Shape.Sort();
                    currentActor.Shape.InitTextureStuff();
                }
            }

            // Start a new shape
            if (_input.ClickedButton(Keys.Space))
            {
                currentActor = new Actors.Actor(Universe, Vector2.Zero);
            }

            // Next texture
            if (_input.ClickedButton(Keys.T))
            {
                currentTexture++;
                currentActor.Shape.Tex = Universe.Content.Load<Texture2D>(Textures[(currentTexture) % Textures.Length]);
                currentActor.Shape.Tex.Name = Textures[(currentTexture) % Textures.Length];
            }

            // Next accessory
            if (_input.ClickedButton(Keys.A))
            {
                // Delete the current accessory if it wasn't placed
                if (!accessoryPlaced)
                {
                    // Remove the current accessory
                    if (Accessory != null)
                    {
                        Accessory.Dead = true;
                    }
                }

                // Create a new accessory for the next one in the array
                Accessory = (Actors.Accessories.Accessory)Activator.CreateInstance(Accessories[currentAcessory % Accessories.Length], new Object[] { Universe, currentActor.Shape, currentActor.Shape.Depth + 1 });
                accessoryPlaced = false;
                currentAcessory++;
            }

            // If we haven't placed the accessory
            if (!accessoryPlaced)
            {
                if (Accessory != null)
                {
                    Accessory.Position = Universe.Camera.GetWorldCoord(Universe.GraphicsDevice, new Vector2(_input.Mouse.X, _input.Mouse.Y));
                }
            }

            // Place an accessory
            if (_input.ClickedRightButton)
            {
                if (Accessory != null && !accessoryPlaced)
                {
                    Accessory.AddToWearer();
                    accessoryPlaced = true;
                }
            }

            // Put physics on the current shape
            if (_input.ClickedButton(Keys.P))
            {
                Console.WriteLine(currentActor.SetWorld(Universe.TheWorld));
            }

            if (_input.ClickedButton(Keys.S))
            {
                currentActor.SaveToFile("savedActor.txt");
            }

            if (_input.ClickedButton(Keys.L))
            {
                currentActor.LoadFromString("Shape;Food/CupCake;160;110;-76.00001;125;-163;37;-180;-92.99999;22;-173;152;-133;215;-62.99999;Accessories;LibCut.Things.Actors.Accessories.Eyes.BlueEyes;22;55;LibCut.Things.Actors.Accessories.Melee.Fuse;29;-145;LibCut.Things.Actors.Accessories.Thrusters.SimpleThruster;-138;60;LibCut.Things.Actors.Accessories.Utility.Targetter;-95.00001;-120;LibCut.Things.Actors.Accessories.Thrusters.SimpleThruster;-123;81.99999;LibCut.Things.Actors.Accessories.Thrusters.SimpleThruster;-153;35;");
            }
        }


    }
}
