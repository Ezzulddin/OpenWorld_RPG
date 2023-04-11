using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWorld_RPG.GameStates;
using System.Diagnostics;

namespace OpenWorld_RPG.Customise_Screen
{
    public class CustomiseButton : State
    {
        private Texture2D blackHair, greenHair, redHair, blueTshirt, redTshirt, greenHairBlueTshirt,
            greenHairRedShirt, redHairBlueShirt, redHairRedShirt,backButton,nextButton,playerTexture;
        private Texture2D leftArrow, rightArrow;
        private List<Components> _customise;
        private SpriteFont font;
        private Customise player, customise,textFile;
        private int hairCount, shirtCount, trouserCount = 0;
        private Vector2 pos, size;
        private TextInputManager tManager;
        private string name ="";
        private SpriteFont nameFont;
        private Vector2 namePos = new Vector2(1000,540);
        
        public CustomiseButton(Game1 game, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, ContentManager Content) : base(game, graphicsDevice, graphics, Content) { }
        public override void LoadContent()
        {
            blackHair = Content.Load<Texture2D>("Player");
            greenHair = Content.Load<Texture2D>("PlayerGreenHair");
            redHair = Content.Load<Texture2D>("PlayerRedHair");
            leftArrow = Content.Load<Texture2D>("LeftArrow");
            rightArrow = Content.Load<Texture2D>("RightArrow");
            blueTshirt = Content.Load<Texture2D>("PlayerBlueShirt");
            redTshirt = Content.Load<Texture2D>("PlayerRedShirt");
            greenHairBlueTshirt = Content.Load<Texture2D>("GreenHairBlueShirt");
            greenHairRedShirt = Content.Load<Texture2D>("GreenHairRedShirt");
            redHairBlueShirt = Content.Load<Texture2D>("RedHairBlueShirt");
            redHairRedShirt = Content.Load<Texture2D>("RedHairRedShirt");
            backButton = Content.Load<Texture2D>("BackButton");
            nextButton = Content.Load<Texture2D>("NextButton");

            tManager = new TextInputManager(name,nameFont,namePos);

            pos = new Vector2(200, 300);
            size = new Vector2(400, 500);
            player = new Customise(blackHair, pos, size, Color.White);
            Button HairRightButton = new Button(rightArrow, font)
            {
                Position = new Vector2(player.spritePosition.X + 450, player.spritePosition.Y),
                Size = new Vector2(150, 150),
                Text = "",
            };
            HairRightButton.Click += HairRightButton_Click;
            Button HairLeftButton = new Button(leftArrow, font)
            {
                Position = new Vector2(player.spritePosition.X - 200, player.spritePosition.Y),
                Size = new Vector2(150, 150),
                Text = "",
            };
            HairLeftButton.Click += HairLeftButton_Click;
            Button ShirtRightButton = new Button(rightArrow, font)
            {
                Position = new Vector2(HairRightButton.Position.X, HairRightButton.Position.Y + 200),
                Size = new Vector2(150, 150),
                Text = "",
            };
            ShirtRightButton.Click += ShirtRightButton_Click;
            Button ShirtLeftButton = new Button(leftArrow, font)
            {
                Position = new Vector2(HairLeftButton.Position.X, HairLeftButton.Position.Y + 200),
                Size = new Vector2(150, 150),
                Text = "",
            };
            ShirtLeftButton.Click += ShirtLeftButton_Click;
            Button NextButton = new Button(nextButton, font)
            {
                Position = new Vector2(1000,800),
                Size = new Vector2(255,100),
                Text = "",
            };
            NextButton.Click += NextButton_Click;
            Button BackButton = new Button(backButton, font)
            {
                Position = new Vector2(NextButton.Position.X + 270,NextButton.Position.Y),
                Size = new Vector2(255,100),
                Text = "",
            };
            BackButton.Click += BackButton_Click;
            
            customise = new Customise();
            textFile = new Customise();

            _customise = new List<Components>()
            {
                HairRightButton,
                HairLeftButton,
                ShirtRightButton,
                ShirtLeftButton,
                NextButton,
                BackButton,
            };
            tManager.LoadContent(Content);
            
        }
        private void NextButton_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _graphics, Content));
            
        }
        private void BackButton_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _graphics, Content));
            
        }
        private void HairLeftButton_Click(object sender, System.EventArgs e)
        {
            if (hairCount >= 0 && hairCount <= 2)
            {
                hairCount--;
                player = customise.Customisation(hairCount, shirtCount, player, pos, size, greenHair, redHair,
                    blackHair, blueTshirt, redTshirt, greenHairBlueTshirt, greenHairRedShirt, redHairBlueShirt, redHairRedShirt);
                
            }
            
        }
        private void ShirtRightButton_Click(object sender, System.EventArgs e)
        {
            if (shirtCount >= 0 && hairCount <= 2)
            {
                shirtCount++;
                player = customise.Customisation(hairCount, shirtCount, player, pos, size, greenHair, redHair,
                    blackHair, blueTshirt, redTshirt, greenHairBlueTshirt, greenHairRedShirt, redHairBlueShirt, redHairRedShirt);
                
            }
            
        }
        private void ShirtLeftButton_Click(object sender, System.EventArgs e)
        {
            if (shirtCount >= 0 && hairCount <= 2)
            {
                shirtCount--;
                player = customise.Customisation(hairCount, shirtCount, player, pos, size, greenHair, redHair, 
                    blackHair, blueTshirt, redTshirt, greenHairBlueTshirt, greenHairRedShirt, redHairBlueShirt, redHairRedShirt);
                
            }
            
        }
        private void HairRightButton_Click(object sender, System.EventArgs e)
        {
            if (hairCount >= 0 && hairCount <= 2)
            {
                hairCount++;
                player = customise.Customisation(hairCount, shirtCount, player, pos, size, greenHair, redHair,
                    blackHair, blueTshirt, redTshirt, greenHairBlueTshirt, greenHairRedShirt, redHairBlueShirt, redHairRedShirt);
                
            }
            
        }
        public override void Update(GameTime gameTime)
        {
            foreach (Components e in _customise)
            {
                e.Update(gameTime);
            }
            tManager.GetKeys();
            
            
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            player.DrawCharacter(spriteBatch, player.currentTex);
            // Here is where the current player stores its texture but for some reason wont store its new texture.
            textFile.WriteToFile(player.currentTex);
            //Debug.WriteLine(textFile.ReadFromFile());

            tManager.Draw(spriteBatch);

            foreach (Components e in _customise)
            {
                e.Draw(gameTime, spriteBatch);
            }
        }
        public override void PostUpdate(GameTime gameTime)
        {
            tManager.WriteToFile();
            Debug.WriteLine(tManager.ReadFromFile());
        }
    }
}
