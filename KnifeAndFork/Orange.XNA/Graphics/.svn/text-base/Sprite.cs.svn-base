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

namespace Orange.XNA
{   /// <summary>
    /// Contains information about a sprite. It can handle animated sprites.
    /// </summary>
    public class Sprite : PhysicsObject
    {
        /// <summary>
        /// The texture for this sprite
        /// </summary>
        public Texture2D texture;

        /// <summary>
        /// Whether or not to draw the texture
        /// </summary>
        public bool visible = true;

        /// <summary>
        /// You can flip the sprite by setting this.
        /// </summary>
        public SpriteEffects flip;

        /// <summary>
        /// A color to tint the sprite with. Using Color.White will make no tint. Color.White is the default.
        /// </summary>
        public Color tint = Color.White;

        /// <summary>
        /// The section of the texture to draw
        /// </summary>
        public Rectangle clippingRect;

        /// <summary>
        /// A class to contain all the information about the animation frames.
        /// </summary>
        public class Frame
        {
            /// <summary>
            /// A boolean value determining whether or not this sprite is animated or not.
            /// If the sprite is not animated, the entire sprite will be drawn.
            /// </summary>
            public bool animated = false;

            /// <summary>
            /// The current frame to draw.
            /// </summary>
            double FrameNum;
            public double frame
            {
                set
                {
                    modified = true;
                    FrameNum = value;
                }
                get
                {
                    return FrameNum;
                }
            }

            /// <summary>
            /// Width of a single frame.
            /// </summary>
            public uint width;

            /// <summary>
            /// Height of a single frame.
            /// </summary>
            public uint height;

            /// <summary>
            /// The horizontal spacing between frames on the spritesheet
            /// </summary>
            public uint widthSpacing;

            /// <summary>
            /// The vertical spacing between frames on the spritesheet
            /// </summary>
            public uint heightSpacing;

            /// <summary>
            /// This gets set if the frame is modified
            /// </summary>
            public bool modified;
        }

        /// <summary>
        /// An instance of the frame class, which contains all the information about animation. 
        /// Adjusting any of these values will cause every to be updated.
        /// </summary>
        Frame _frame;
        public Frame frame
        {
            set
            {   
                // Update frame
                _frame = value;

                // Call update when this is changed at all
                UpdateFrameRect();
            }
            get
            {
                return _frame;
            }
        }

        /// <summary>
        /// Contains information about an animation. Start frame, end frame, fps, etc
        /// </summary>
        public class Animation
        {
            /// <summary>
            /// The index of the starting frame
            /// </summary>
            public int start;

            /// <summary>
            /// The index of the ending frame
            /// </summary>
            public int end;

            /// <summary>
            /// The speed of the animation
            /// </summary>
            public double fps = 60;

            /// <summary>
            /// Whether or not to loop the animation
            /// </summary>
            public bool loop;
        }

        /// <summary>
        /// A dictionary of all the animations in this sprite
        /// </summary>
        Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        /// <summary>
        /// The animation that will be played if there is no current animation.
        /// </summary>
        public Animation defaultAnimation = new Animation();

        /// <summary>
        /// The currently playing animation. If this animation cannot be found then the default will be chosen.
        /// </summary>
        string _currentAnimation = "";
        public string CurrentAnimation
        {
            get
            {
                return _currentAnimation;
            }
        }
        public Animation GetCurrentAnimation()
        {
            if (!animations.ContainsKey(_currentAnimation))
            {
                return defaultAnimation;
            }
            else
            {
                return animations[_currentAnimation];
            }
        }
        public void SetCurrentAnimation(string _key)
        {
            _currentAnimation = _key;
        }


        /// <summary>
        /// Adds an animation to the sprite.
        /// Won't allow duplicates.
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_start"></param>
        /// <param name="_end"></param>
        /// <param name="_fps"></param>
        /// <param name="_loop"></param>
        public void AddAnimation(string _name, int _start, int _end, int _fps, bool _loop)
        {
            if (!animations.ContainsKey(_name))
            {   
                Animation animation = new Animation();
                animation.start = _start;
                animation.end = _end;
                animation.fps = _fps;
                animation.loop = _loop;
                animations.Add(_name, animation);
            }
        }

        /// <summary>
        /// Removes an animation from the sprite.
        /// If the current animation is removed, default the default animation will play.
        /// </summary>
        /// <param name="_name"></param>
        public void RemoveAnimation(string _name)
        {
            if (animations.ContainsKey(_name))
            {
                animations.Remove(_name);
            }
        }

        /// <summary>
        /// Class describing how the sprite should be drawn.
        /// </summary>
        public class DrawRules
        {
            /// <summary>
            /// Shader to use to draw the sprite
            /// </summary>
            Effect effect;

            /// <summary>
            /// What to do with the pixels when drawing.
            /// Use this to make the sprite transparent opaque or other things.
            /// </summary>
            BlendState blend;

            /// <summary>
            /// Controls how the texture is presented to any shaders
            /// </summary>
            SamplerState sample;

            /// <summary>
            /// Controls shadows.. I think
            /// </summary>
            DepthStencilState depthStencil;

            /// <summary>
            ///  Controls how vertex data is converted into pixel data (rasterized)
            /// </summary>
            RasterizerState rasterizer;
        }

        /// <summary>
        /// Drawing rules for this sprite
        /// </summary>
        public DrawRules drawRules;

        /// <summary>
        /// Sprite's constructor.
        /// If you are using this constructor, remeber to call Load() when you need to load a texture.
        /// </summary>
        public Sprite()
        {
            // We can't really do anything untill we get texture loaded...
        }

        /// <summary>
        /// Automatically loads a static image
        /// </summary>
        /// <param name="_content"></param>
        /// <param name="_name"></param>
        public Sprite(ContentManager _content, string _name)
        {
            // Just load a static sprite
            Load(_content, _name);
        }

        /// <summary>
        /// Automatically loads a sprite sheet
        /// </summary>
        /// <param name="_content"></param>
        /// <param name="_name"></param>
        /// <param name="_frameWidth"></param>
        /// <param name="_frameHeight"></param>
        /// <param name="_frameWidthSpacing"></param>
        /// <param name="_frameHeightSpacing"></param>
        public Sprite(ContentManager _content, string _name, uint _frameWidth, uint _frameHeight, uint _frameWidthSpacing, uint _frameHeightSpacing)
        {
            Load(_content, _name, _frameWidth, _frameHeight, _frameWidthSpacing, _frameHeightSpacing);
        }

        /// <summary>
        /// Loads the requested resource into the sprite. Using this version assumes a non animated sprite.
        /// </summary>
        /// <param name="_content">A pointer to the content manager to use to load the sprite</param>
        /// <param name="_name">The title of the resoure (without file extension)</param>
        public void Load(ContentManager _content, string _name)
        {
            // Load the texture
            texture = _content.Load<Texture2D>(_name);

            // Create a blank frame info
            frame = new Frame();

            // Put in default values for a static sprite
            frame.animated = false; // It's not animated
            frame.width = (uint)texture.Width;
            frame.height = (uint)texture.Height;

            // Update the clipping rect
            UpdateFrameRect();

            // Set defaults
            SetDefaults();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_content"></param>
        /// <param name="_name"></param>
        /// <param name="_frameWidth"></param>
        /// <param name="_frameHeight"></param>
        /// <param name="_frameWidthSpacing"></param>
        /// <param name="_frameHeightSpacing"></param>
        public void Load(ContentManager _content, string _name, uint _frameWidth, uint _frameHeight, uint _frameWidthSpacing, uint _frameHeightSpacing)
        {
            // First load the texture normaly
            Load(_content, _name);

            // Now lets make the frame data
            frame = new Frame();
            frame.animated = true;
            frame.width = _frameWidth;
            frame.height = _frameHeight;
            frame.widthSpacing = _frameWidthSpacing;
            frame.heightSpacing = _frameHeightSpacing;

            // Force the current frame data to be generated
            UpdateFrameRect();

            // Set defaults
            SetDefaults();
        }

        /// <summary>
        /// Resets all values back to defaults
        /// </summary>
        public void SetDefaults()
        {
            // Set the center of the sprite
            center = new Vector2(frame.width / 2, frame.height / 2);

            // Set the tint
            tint = Color.White;

            // Reset scale
            scale = new Vector2(1.0f);
        }

        /// <summary>
        /// Draws the sprite using a spritebatch. Make sure to encapsulate this call with begin and end calls.
        /// </summary>
        /// <param name="_spriteBatch"></param>
        public virtual void Draw(SpriteBatch _spriteBatch)
        {   
            // If the frame data has been modified then update the clipping rect
            if (frame.modified)
            {
                UpdateFrameRect();
                frame.modified = false;
            }

            if (visible)
            {
                // Just use spritebatch to draw the texture
                _spriteBatch.Draw(texture,
                                  position,
                                  clippingRect,
                                  tint,
                                  rotation,
                                  center,
                                  scale,
                                  flip,
                                  0);
            }
        }

        public void IncAnimation(GameTime _gameTime)
        {   
            // First check if we are actually animated
            if (frame.animated)
            {
                // Get how much time one frame should take
                int mili = (int)((1.0 / GetCurrentAnimation().fps) * 1000);

                // So how much of that has elapsed?
                if (_gameTime.ElapsedGameTime.Milliseconds != 0)
                {
                    frame.frame += _gameTime.ElapsedGameTime.Milliseconds / (float)mili;
                }

                // If the frame is over the amount set in the animation,
                // then check if it is set to loop.
                if (frame.frame > GetCurrentAnimation().end)
                {
                    if (GetCurrentAnimation().loop)
                    {
                        frame.frame = (frame.frame - GetCurrentAnimation().end) + GetCurrentAnimation().start;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the sprite. Only needs to be called if your sprite is animated.
        /// </summary>
        /// <param name="_gameTime"></param>
        public virtual void Update(GameTime _gameTime)
        {
            // Inc frame
            IncAnimation(_gameTime);
        }

        /// <summary>
        /// This class can calculate the rectangle for a particular item from a sprite sheet.
        /// </summary>
        static class SpriteSheetParser
        {   
            /// <summary>
            /// Gets how many frames will be across a texture
            /// </summary>
            /// <param name="_frame"></param>
            /// <param name="_texture"></param>
            /// <returns></returns>
            static public Vector2 GetFramesDimensions(Frame _frame, Texture2D _texture)
            {
                return new Vector2(GetFramesWidth(_frame, _texture), GetFramesHeight(_frame, _texture));
            }

            /// <summary>
            /// Gets how many frames will be across the width of a texture
            /// </summary>
            /// <param name="_frame"></param>
            /// <param name="_texture"></param>
            /// <returns></returns>
            static public uint GetFramesWidth(Frame _frame, Texture2D _texture)
            {
                return (uint)(_texture.Width / (_frame.width + _frame.widthSpacing))
                     + (uint)((_texture.Width % (_frame.width + _frame.widthSpacing)) / _frame.width);
            }

            /// <summary>
            /// Gets how many frames will be across the height of a texture
            /// </summary>
            /// <param name="_frame"></param>
            /// <param name="_texture"></param>
            /// <returns></returns>
            static public uint GetFramesHeight(Frame _frame, Texture2D _texture)
            {
                return (uint)(_texture.Height / (_frame.height + _frame.heightSpacing))
                     + (uint)((_texture.Height % (_frame.height + _frame.heightSpacing)) / _frame.height);
            }

            /// <summary>
            /// Gets the position of a frame in terms of frames
            /// </summary>
            /// <param name="_frame"></param>
            /// <param name="_texture"></param>
            /// <returns></returns>
            static public Vector2 GetFramePosition(Frame _frame, Texture2D _texture)
            {   
                // Get dimensions of the image in frames
                uint widthFrame = GetFramesWidth(_frame, _texture);
                uint heightFrame = GetFramesHeight(_frame, _texture);

                // Check if this is within the amount of frames available
                if (_frame.frame > widthFrame * heightFrame)
                {
                    return new Vector2();
                }

                // Find out the position of the frame in terms of frames
                uint xFrame = (uint)_frame.frame % widthFrame;
                uint yFrame = (uint)_frame.frame / widthFrame;

                return new Vector2(xFrame, yFrame);
            }

            /// <summary>
            /// Gets the actual pixel coordinates of the current frame.
            /// </summary>
            /// <param name="_frame"></param>
            /// <param name="_texture"></param>
            /// <returns></returns>
            static public Vector2 GetAbsoluteFramePosition(Frame _frame, Texture2D _texture)
            {
                return GetFramePosition(_frame, _texture) * new Vector2((_frame.width + _frame.widthSpacing), (_frame.height + _frame.heightSpacing));
            }

            /// <summary>
            /// Generates a rectangle for a certain frame on a texture
            /// </summary>
            /// <param name="_frame"></param>
            /// <param name="_texture"></param>
            /// <returns></returns>
            static public Rectangle GetRect(Frame _frame, Texture2D _texture)
            {   
                if (_frame.animated)
                { 
                    return new Rectangle((int)GetAbsoluteFramePosition(_frame, _texture).X, (int)GetAbsoluteFramePosition(_frame, _texture).Y, (int)_frame.width, (int)_frame.height);
                }
                else
                {
                    return _texture.Bounds;
                }
            }
        }

        /// <summary>
        /// Updates the rectangle used for drawing the current frame.
        /// </summary>
        protected void UpdateFrameRect()
        {
            clippingRect = SpriteSheetParser.GetRect(frame, texture);
            size.X = clippingRect.Width;
            size.Y = clippingRect.Height;
        }
    }

}
