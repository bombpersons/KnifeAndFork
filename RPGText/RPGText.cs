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

namespace RPGText
{
    public class RPGText : TextBox
    {
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (value != Text)
                {
                    Finished = false;
                    charIndex = 0;
                    lineIndex = 0;
                }
                base.Text = value;
            }
        }

        /// <summary>
        /// Wether or not the text is paused
        /// </summary>
        protected bool paused;
        public bool Paused
        {
            get
            {
                return paused;
            }
            set
            {
                paused = value;
            }
        }

        /// <summary>
        /// How many times we have paused
        /// </summary>
        protected int pausedCount = 0;

        /// <summary>
        /// Whether or not the text has finished
        /// </summary>
        protected bool finished;
        public bool Finished
        {
            get
            {
                return finished;
            }
            set
            {
                finished = value;
            }
        }

        /// <summary>
        /// The user data
        /// </summary>
        protected object userData;
        public object UserData
        {
            get
            {
                return userData;
            }
            set
            {
                userData = value;
            }
        }

        /// <summary>
        /// The speed the text scrolls
        /// </summary>
        protected float speed;
        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

        /// <summary>
        /// The current line index
        /// </summary>
        protected int lineIndex;
        protected float charIndex;

        /// <summary>
        /// The character height to show
        /// </summary>
        protected int charHeight = 3;
        public int CharHeight
        {
            get
            {
                return charHeight;
            }
            set
            {
                charHeight = value;
            }
        }

        /// <summary>
        /// The lines to use to print
        /// </summary>
        protected List<string> printLines;
        public List<string> PrintLines
        {
            get
            {
                return printLines;
            }
            set
            {
                printLines = value;
            }
        }

        /// <summary>
        /// Whether or not we are in the process of reading an event
        /// </summary>
        protected bool readingEvent = false;

        /// <summary>
        /// The currently read event string
        /// </summary>
        protected string eventString;

        /// <summary>
        /// The delegate definition
        /// </summary>
        /// <param name="_caller"></param>
        /// <returns></returns>
        public delegate int Event(object _caller);

        /// <summary>
        /// A list of events to use
        /// </summary>
        protected Dictionary<int, Event> events = new Dictionary<int, Event>();
        public Dictionary<int, Event> Events
        {
            get
            {
                return events;
            }
            set
            {
                events = value;
            }
        }

        /// <summary>
        /// Create a rpgtext thingy majig
        /// </summary
        /// <param name="_charWidth"></param>
        /// <param name="_speed"></param>
        public RPGText(int _charWidth, float _speed)
            : base(_charWidth)
        {
            Speed = _speed;
        }
        
        /// <summary>
        /// Returns the current text
        /// </summary>
        /// <param name="_gameTime"></param>
        /// <returns></returns>
        public virtual void UpdateText(GameTime _gameTime)
        {
            if (!Paused && lines.Count > 0)
            {
                // Increase the charIndex
                charIndex += (float)_gameTime.ElapsedGameTime.TotalSeconds * Speed;

                // If the charIndex is higher than the current line's length
                if (charIndex > lines[lineIndex].Length)
                {
                    if (lineIndex >= lines.Count - 1)
                    {
                        charIndex = lines[lines.Count - 1].Length - 1;
                        Finished = true; // The text is finished.
                    }
                    else
                    {
                        charIndex %= lines[lineIndex].Length;
                        lineIndex++;
                    }
                }
                else
                {
                    // Pause if we see any full stops
                    if ((int)charIndex - 1 >= 0)
                    {
                        if (lines[lineIndex][(int)charIndex - 1] == '.')
                        {
                            Paused = true;
                            pausedCount++;
                            if (Events.ContainsKey(pausedCount))
                            {
                                Events[pausedCount](userData);
                            }
                        }
                    }
                }

                // Now create the string
                List<string> text = new List<string>();
                int start = (lineIndex - (charHeight - 1));
                if (start < 0)
                {
                    start = 0;
                }
                for (int i = start; i <= lineIndex; i++)
                {
                    if (i == lineIndex)
                    {
                        text.Add(lines[i].Substring(0, (int)charIndex));
                    }
                    else
                    {
                        text.Add(lines[i]);
                    }
                }

                // Return the text
                printLines = text;
            }
        }
    }
}
