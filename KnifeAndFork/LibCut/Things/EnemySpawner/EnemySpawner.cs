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

namespace LibCut.Things.EnemySpawner
{
    public class EnemySpawner : Thing
    {
        /// <summary>
        /// A random number generator
        /// </summary>
        protected static Random rand = new Random();

        /// <summary>
        /// When this gets below zero, spawn new object
        /// </summary>
        protected TimeSpan spawnTime = new TimeSpan();
        public TimeSpan SpawnTime
        {
            get
            {
                return spawnTime;
            }
            set
            {
                spawnTime = value;
            }
        }

        /// <summary>
        /// The type to spawn
        /// </summary>
        protected Type spawnType;
        public Type SpawnType
        {
            get
            {
                return spawnType;
            }
            set
            {
                spawnType = value;
            }
        }

        /// <summary>
        /// The area to spawn the enemy
        /// </summary>
        protected Rectangle spawnArea;
        public Rectangle SpawnArea
        {
            get
            {
                return spawnArea;
            }
            set
            {
                spawnArea = value;
            }
        }

        /// <summary>
        /// The number of enemies to spawn in that area
        /// </summary>
        protected int number;
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        /// <summary>
        /// A list of enemie to keep track of
        /// </summary>
        List<Things.Actors.Actor> enemies = new List<Things.Actors.Actor>();

        /// <summary>
        /// Creates a new Enemy spawner
        /// </summary>
        /// <param name="_universe"></param>
        public EnemySpawner(Universe.Universe _universe, Type _type, Rectangle _spawnArea, int _num)
            : base(_universe)
        {
            SpawnType = _type;
            SpawnArea = _spawnArea;
            Number = _num;
        }

        /// <summary>
        /// Create new enemies if neccassery
        /// </summary>
        /// <param name="_gameTime"></param>
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            // Update the timer
            SpawnTime -= _gameTime.ElapsedGameTime;
            if (SpawnTime.Ticks <= 0)
            {
                // Make new ones
                if (enemies.Count < Number)
                {
                    // Get a random spot in the area
                    Vector2 position = new Vector2(SpawnArea.Left + SpawnArea.Width * (float)rand.NextDouble(), SpawnArea.Top + SpawnArea.Height * (float)rand.NextDouble());

                    // Spawn one there
                    enemies.Add((Things.Actors.Actor)Activator.CreateInstance(SpawnType, new Object[] { Universe, position }));
                }

                // Reset the counter
                SpawnTime = new TimeSpan((long)(20 * (10000000) * (float)rand.NextDouble()));
            }

            // Remove any dead enemies
            foreach (Things.Actors.Actor actor in enemies.ToArray())
            {
                if (actor.Dead)
                {
                    enemies.Remove(actor);
                }
            }
        }
    }
}
