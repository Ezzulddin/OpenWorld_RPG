using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenWorld_RPG.AStar_Aglorithm;
using OpenWorld_RPG.Customise_Screen;
using OpenWorld_RPG.GameStates;
using OpenWorld_RPG.Json_File;
using OpenWorld_RPG.Map;
using OpenWorld_RPG.Mob_Folder;
using OpenWorld_RPG.PlayerFolder;
using OpenWorld_RPG.Shop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace OpenWorld_RPG
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        public static int screenWidth;
        public static int screenHeight;
        
        private State _currentState;
        private State _nextState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            screenHeight = _graphics.PreferredBackBufferHeight = 1080;
            screenWidth = _graphics.PreferredBackBufferWidth = 1920;
        }

        protected override void Initialize()
        {
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, GraphicsDevice,_graphics, Content);
            _currentState.LoadContent();
            _nextState = null;
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();
                _nextState = null;
            }
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }
        public void ChangeState(State state)
        {
            _nextState = state;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);
            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}