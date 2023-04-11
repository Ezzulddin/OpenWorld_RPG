using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OpenWorld_RPG.Customise_Screen;
using OpenWorld_RPG.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.GameStates
{
    public class MenuState : State
    {
        private List<Components> _gameComponents;
        private Texture2D playButton, exitButton, settingButton, customiseButton;
        private SpriteFont font;
        private Title title;
        private Texture2D titleTex;
        private Vector2 titleSize = new Vector2(1250, 400);
        private Vector2 titlePos;
        public MenuState(Game1 game, GraphicsDevice graphicsDevice,GraphicsDeviceManager graphics,ContentManager Content) : base(game, graphicsDevice,graphics, Content) { }

        public override void LoadContent()
        {
            playButton = Content.Load<Texture2D>("PlayButton");
            exitButton = Content.Load<Texture2D>("ExitButton");
            customiseButton = Content.Load<Texture2D>("CustomiseButton");
            font = Content.Load<SpriteFont>("fonts");
            titleTex = Content.Load<Texture2D>("Logo1");

            #region Buttons
            Button PlayButton = new Button(playButton, font)
            {
                Position = new Vector2((1920 / 2) - 150, (1080 / 2) - 50),
                Size = new Vector2(250, 100),
                Text = "",
            };
            PlayButton.Click += PlayButton_Click;

            Button CustomiseButton = new Button(customiseButton, font)
            {
                Position = new Vector2(PlayButton.Rectangle.X, PlayButton.Rectangle.Y + 105),
                Size = new Vector2(255, 100),
                Text = "",
            };
            CustomiseButton.Click += CustomiseButton_Click;


            Button ExitButton = new Button(exitButton, font)
            {
                Position = new Vector2(CustomiseButton.Rectangle.X, CustomiseButton.Rectangle.Y + 100),
                Size = new Vector2(255, 100),
                Text = "",
            };
            ExitButton.Click += ExitButton_Click;

            _gameComponents = new List<Components>()
            {
                PlayButton,
                ExitButton,
                CustomiseButton,
            };
            titlePos = new Vector2(960 - 600, 440 - 350);
            title = new Title(titleTex, titlePos, titleSize, Color.White);
            #endregion
        }
        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice,_graphics, Content));
        }
        private void CustomiseButton_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new CustomiseButton(_game, _graphicsDevice, _graphics, Content));
        }
        private void SettingButton_Click(object sender, System.EventArgs e)
        {
            
        }
        
        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            _game.Exit();
        }
        public override void Update(GameTime gameTime)
        {
            
            foreach (Components e in _gameComponents)
            {
                e.Update(gameTime);
            }
        }
        public override void PostUpdate(GameTime gameTime)
        {
            
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Components e in _gameComponents)
                e.Draw(gameTime, spriteBatch);
            title.DrawSprite(spriteBatch, title.tex);
        }

    }
}
