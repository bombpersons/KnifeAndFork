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

namespace Orange.XNA.Graphics.Primitives
{
    public class Grid : PhysicsObject
    {
        /// <summary>
        /// The shader that will do pretty much all the work
        /// </summary>
        Effect shader;

        /// <summary>
        /// The color an on cell would be
        /// </summary>
        Color fill = Color.Black;
        public Color Fill
        {
            get
            {
                return fill;
            }
            set
            {
                fill = value;
            }
        }

        /// <summary>
        /// The color and empty cell would be
        /// </summary>
        Color fillBlank = Color.White;
        public Color FillBlank
        {
            get
            {
                return fillBlank;
            }
            set
            {
                fillBlank = value;
            }
        }

        /// <summary>
        /// The color of the borders
        /// </summary>
        Color border = Color.Gray;
        public Color Border
        {
            get
            {
                return border;
            }
            set
            {
                border = value;
            }
        }

        /// <summary>
        /// The size of one cell
        /// </summary>
        Vector2 cellSize = new Vector2(20, 20);
        public Vector2 CellSize
        {
            get
            {
                return cellSize;
            }
            set
            {
                cellSize = value;
                size = new Vector2((int)size.X - ((int)size.X % (int)value.X), (int)size.Y - ((int)size.Y % (int)value.Y));
            }
        }

        /// <summary>
        /// The width of the border
        /// </summary>
        Vector2 width = new Vector2(1);
        public Vector2 Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        /// <summary>
        /// Texture data to store alive data
        /// </summary>
        public Texture2D data;

        /// <summary>
        /// We create a texture to indicate the array of aliveness
        /// </summary>
        public void SetData(Color[] _data, int _width, int _height)
        {
            // Create a blank texture for data
            data = new Texture2D(graphicsDevice, _width, _height);
            data.SetData<Color>(_data);
        }

        /// <summary>
        /// The content manager to load stuff with.
        /// </summary>
        protected ContentManager content;

        /// <summary>
        /// The graphics device so that we can create textures
        /// </summary>
        protected GraphicsDevice graphicsDevice;

        /// <summary>
        /// Constructs the grid
        /// </summary>
        /// <param name="_rect"></param>
        /// <param name="_content"></param>
        /// <param name="_graphicsDevice"></param>
        public Grid(Vector2 _size, ContentManager _content, GraphicsDevice _graphicsDevice)
        {
            size = _size;
            graphicsDevice = _graphicsDevice;
            content = _content;
            shader = _content.Load<Effect>(@"Grid");
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            // Update some things
            //_scale = drawScale / new Vector2(texture.Width, texture.Height);

            // Set shader parameters
            shader.Parameters["Scale"].SetValue(size);
            shader.Parameters["Fill"].SetValue(Fill.ToVector4());
            shader.Parameters["FillBlank"].SetValue(FillBlank.ToVector4());
            shader.Parameters["Border"].SetValue(Border.ToVector4());
            shader.Parameters["CellSize"].SetValue(CellSize);
            shader.Parameters["Width"].SetValue(Width);
            //shader.Parameters["Alive"].SetValue(alive);

            _spriteBatch.Begin(0, null, null, null, null, shader);
            // Just use spritebatch to draw the texture
            _spriteBatch.Draw(data,
                              position,
                              new Rectangle(0, 0, data.Width, data.Height),
                              Color.White,
                              rotation,
                              center,
                              size / new Vector2(data.Width, data.Height),
                              0,
                              0);
            _spriteBatch.End();
        }
    }
}
