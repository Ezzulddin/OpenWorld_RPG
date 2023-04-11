using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.GameStates
{
    public abstract class State
    {
        protected Game1 _game;

        protected GraphicsDevice _graphicsDevice;
        protected GraphicsDeviceManager _graphics;
        protected ContentManager Content;

        public State(Game1 game, GraphicsDevice graphicsDevice,GraphicsDeviceManager graphics, ContentManager content)
        {
            _game = game;
            Content = content;
            _graphicsDevice = graphicsDevice;
            _graphics = graphics;
        }

        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void PostUpdate(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
