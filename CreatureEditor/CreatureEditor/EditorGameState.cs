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


namespace CreatureEditor
{
    public class EditorGameState : Orange.XNA.Game.GameState
    {
        /// <summary>
        /// The universe
        /// </summary>
        EditorUniverse universe;

        public EditorGameState(GraphicsDevice _graphcisDevice, ContentManager _content)
            : base(_graphcisDevice, _content)
        {
            universe = new EditorUniverse(_content, _graphcisDevice);
        }

        public override void Draw()
        {
            base.Draw();

            universe.Draw();
        }

        public override void HandleInput(GameTime _gameTime, Orange.XNA.Input.Input _input)
        {
            base.HandleInput(_gameTime, _input);

            universe.HandleInput(_gameTime, _input);
        }

        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

            universe.Update(_gameTime);
        }
    }
}
