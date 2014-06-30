using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCut.Things.Sprite
{
    public class StaticSprite : Sprite
    {
        public StaticSprite(Universe.Universe _universe, Orange.XNA.Sprite _sprite, int _depth)
            : base(_universe, _sprite, _depth)
        {
        }

        public override void Draw(Camera.Camera _camera, Microsoft.Xna.Framework.Graphics.GraphicsDevice _graphicsDevice)
        {
            //base.Draw(_camera, _graphicsDevice);
            sprite.position = Position;
            sprite.scale = Scale;
            sprite.rotation = Rotation;
            spriteBatch.Begin();
            sprite.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
