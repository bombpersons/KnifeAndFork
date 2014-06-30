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

namespace Orange.XNA.Graphics.Particles
{
    /// <summary>
    /// A single particle. A particle can contain other particles, so you can use this as an emitter.
    /// </summary>
    public class Particle : Sprite
    {
        /// <summary>
        /// A list of particles to manage
        /// </summary>
        protected List<Particle> particles = new List<Particle>();

        /// <summary>
        /// Time this particle has left untill it dies
        /// </summary>
        protected TimeSpan timeLeft;
        protected TimeSpan time;

        /// <summary>
        /// A random number generator to generate particles
        /// </summary>
        protected Random rand = new Random();

        /// <summary>
        /// Whether or not to make this particle last forever
        /// </summary>
        bool forever;
        public bool Forever
        {
            get
            {
                return forever;
            }
            set
            {
                forever = value;
            }
        }

        /// <summary>
        /// Checks whether or not the particle has expired
        /// </summary>
        public bool Expired
        {
            get
            {
                if (timeLeft.Ticks < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Whether or not this particle should emit other particles
        /// </summary>
        bool emitter = true;
        public bool Emitter
        {
            get
            {
                return emitter;
            }
            set
            {
                emitter = value;
            }
        }

        /// <summary>
        /// The content manager to load stuff with
        /// </summary>
        protected ContentManager content;

        /// <summary>
        /// Construct the particle
        /// </summary>
        /// <param name="_content"></param>
        /// <param name="name"></param>
        public Particle(ContentManager _content, string _name, TimeSpan _timeSpan)
            : base(_content, _name)
        {
            content = _content;
            Set(_timeSpan);
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Particle()
        {
        }

        /// <summary>
        /// Set the expire time
        /// </summary>
        /// <param name="_timeSpan"></param>
        public void Set(TimeSpan _timeSpan)
        {
            time = _timeSpan;
            timeLeft = time.Duration();
        }

        /// <summary>
        /// Called on update. This will decay the particle a little bit.
        /// </summary>
        /// <param name="?"></param>
        protected virtual void Decay(GameTime _gameTime)
        {
            if (!Forever)
            {
                timeLeft -= _gameTime.ElapsedGameTime;
            }

            // Check if any of the particles have died. Remove them from the list if they have
            List<int> removeList = new List<int>();
            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].Expired)
                {
                    removeList.Add(i);
                }
            }
            foreach (int i in removeList)
            {
                particles.RemoveAt(i);
            }
        }

        /// <summary>
        /// Emits other particles
        /// </summary>
        /// <param name="_gameTime"></param>
        protected virtual void Emit(GameTime _gameTime)
        {
            // Don't do anything by default
        }

        /// <summary>
        /// Change properties on the 
        /// </summary>
        protected virtual void DoEffects()
        {
            if (!Expired)
            {
                tint = Color.White*((float)timeLeft.Ticks / (float)time.Ticks);
            }
            else
            {
                tint = Color.White*0.0f;
            }
        }

        /// <summary>
        /// Update the stats on the particle.
        /// </summary>
        /// <param name="_gameTime"></param>
        public virtual void Update(GameTime _gameTime)
        {
            if (Emitter && !Expired)
                Emit(_gameTime);
            Decay(_gameTime);
            DoEffects();

            // Update all the particles that are children to this one
            foreach (Particle particle in particles)
            {
                particle.Update(_gameTime);
            }
        }
        
        /// <summary>
        /// Override the draw method to add in some stuff
        /// </summary>
        /// <param name="_spriteBatch"></param>
        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
            
            // Draw the particles that are children to this one
            foreach (Particle particle in particles)
            {
                particle.Draw(_spriteBatch);
            }
        }
    }
}
