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

namespace LibCut.Things.Background
{
    public class Background : Sprite.Sprite
    {
        /// <summary>
        /// The parralax effect
        /// </summary>
        protected float parralax = 0.5f;
        public float Parralax
        {
            get
            {
                return parralax;
            }
            set
            {
                parralax = value;
            }
        }

        /// <summary>
        /// The repeater shader
        /// </summary>
        protected Effect repeater;

        /// <summary>
        /// The texture to repeat
        /// </summary>
        protected Texture2D texture;

        /// <summary>
        /// How many times to repeat the texture
        /// </summary>
        protected int repeats;
        public int Repeats
        {
            get
            {
                return repeats;
            }
            set
            {
                repeats = value;
            }
        }

        /// <summary>
        /// Creates a new background
        /// </summary>
        /// <param name="_universe"></param>
        /// <param name="_sprite"></param>
        public Background(Universe.Universe _universe, Texture2D _texture, int _repeats)
            : base(_universe, null, -1000)
        {
            texture = _texture;
            repeater = Universe.Content.Load<Effect>(@"Shaders/BackgroundShader");
            repeats = _repeats;
        }

        /// <summary>
        /// Make the background repeat
        /// </summary>
        /// <param name="_camera"></param>
        /// <param name="_graphicsDevice"></param>
        public override void Draw(Camera.Camera _camera, GraphicsDevice _graphicsDevice)
        {
            //base.Draw(_camera, _graphicsDevice);

            spriteBatch.Begin(0, BlendState.AlphaBlend, null, null, null, repeater);
            Vector2 DrawPos = Vector2.Transform(Position, _camera.CameraTransform);
            DrawPos = new Vector2(DrawPos.X, -DrawPos.Y) + new Vector2(_graphicsDevice.Viewport.Width / 2, _graphicsDevice.Viewport.Height / 2);
            spriteBatch.Draw(texture,
                             DrawPos,
                             new Rectangle(0, 0, texture.Bounds.Width*Repeats, texture.Bounds.Height*Repeats),
                             Color.White,
                             0,
                             new Vector2(texture.Width*Repeats, texture.Height*Repeats) / 2,
                             Scale*_camera.Scale,
                             SpriteEffects.None,
                             0);
            spriteBatch.End();
        }
    }
}
