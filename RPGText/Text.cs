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

namespace RPGText
{
    public class TextBox
    {
        /// <summary>
        /// The actual text
        /// </summary>
        protected string text;
        public virtual string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                FormatString();
            }
        }

        /// <summary>
        /// Sets the size of the text box in char height / width
        /// </summary>
        protected int charWidth;
        public int CharWidth
        {
            get
            {
                return charWidth;
            }
            set
            {
                charWidth = value;
            }
        }

        /// <summary>
        /// The delimeters to use when word wrapping
        /// </summary>
        protected char[] delimeters = {' '};
        public char[] Delimeters
        {
            get
            {
                return delimeters;
            }
            set
            {
                delimeters = value;
            }
        }

        /// <summary>
        /// The lines
        /// </summary>
        protected List<string> lines = new List<string>();
        public List<string> Lines
        {
            get
            {
                return lines;
            }
            set
            {
                lines = value;
            }
        }

        /// <summary>
        /// Creates a new textbox
        /// </summary>
        public TextBox(int _lineWidth)
        {
            charWidth = _lineWidth;
        }

        /// <summary>
        /// The word wraps the string to the charSize
        /// </summary>
        public void FormatString()
        {
            // Don't do anything if the string is null
            if (text != null)
            {
                // Clear the line list
                lines.Clear();

                // Loop through the whole string
                int charCount = 0;
                int start = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    // inc char count
                    charCount++;

                    // If the char count goes over the charsize
                    if (charCount > charWidth)
                    {
                        // Count back till a space can be found
                        for (int j = i; j >= start; j--)
                        {
                            // Check if this chara`cter is any of the delimeters
                            bool found = false;
                            foreach (char del in Delimeters)
                            {
                                if (text[j] == del)
                                    found = true;
                            }

                            // We found one of the delimeters
                            if (found)
                            {
                                // Use this value to get a substring
                                lines.Add(text.Substring(start, (j - start)));

                                // Set the start of the next line to j + 1 (to skip the delimiter).
                                start = j + 1;

                                // Set i back to the start of the next line
                                i = start;

                                // Reset the char count
                                charCount = 0;

                                // Break out of this loop
                                break;
                            }
                        }
                    }
                }

                // If there is still one line left then add this on now
                if (charCount < charWidth && start != text.Length)
                {
                    lines.Add(text.Substring(start, text.Length - start));
                }
            }
        }
    }
}
