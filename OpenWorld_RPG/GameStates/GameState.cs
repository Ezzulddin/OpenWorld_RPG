using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenWorld_RPG.AStar_Aglorithm;
using OpenWorld_RPG.Customise_Screen;
using OpenWorld_RPG.Json_File;
using OpenWorld_RPG.Map;
using OpenWorld_RPG.Mob_Folder;
using OpenWorld_RPG.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace OpenWorld_RPG.GameStates
{
    public class GameState : State
    {
        #region Player & Mob Instances
        private Player player;
        private Mob mob;
        private Texture2D playerTex, mobTex;
        private KeyboardManager _movementManager = new KeyboardManager();
        private InputManager _inputManager = new InputManager();
        private TileLocation posOfPlayer = new TileLocation(0, 0);
        private TileLocation posOfMob = new TileLocation(0, 0);
        private static Vector2 playerPos = new Vector2(0, 0);
        private Vector2 playerSize = new Vector2(75, 110);
        private static Vector2 mobPos = new Vector2(0, 0);
        private Vector2 mobSize = new Vector2(100, 100);
        private bool pathfind = false;
        private bool mobKilled = false;
        #endregion
        private Texture2D emptyTex, grassTex, waterTex, sandTex,treeTex;
        private JsonFile items, currency;
        private Texture2D swordTex, potionTex;
        private int swordInt, potionInt, currencyInt;
        private JsonSprite sword, potion;
        private SpriteFont currencyFont;
        private TextManager tManager;
        private string money = "100";
        private int moneyInt;
        private bool bought = false;
        private JsonFile inventory;
        private CameraManager iManager;
        private bool tab = false;
        private CameraManager cManager;
        private Texture2D smallSettingButton;
        private SpriteFont font;
        private List<Components> _settingComponents;
        private AStarSearch aStar;
        private Location startingLocation = new();
        private Location endingLocation = new();
        private Ai ai;
        private SquareGrid grid;
        private bool inArea;
        private FileManager fileManager = new FileManager();
        private TileMapClass tileMap = new TileMapClass();
        private Tile[,] tileArray;
        private float[][] map;
        const int NUMBER_OF_TILES = 110;
        private int SIZE_OF_CELL = 65;
        private int seedNumber = 12345899;
        const int radius = 17;
        private Customise textFile;
        private string customTex;
        private TextInputManager nameManager;
        private string name = "";
        private SpriteFont nameFont;
        private Vector2 namePos = new Vector2(500,500);
        public GameState(Game1 game, GraphicsDevice graphicsDevice,GraphicsDeviceManager graphics,ContentManager Content) : base(game, graphicsDevice,graphics, Content) 
        {
            tileArray = new Tile[NUMBER_OF_TILES, NUMBER_OF_TILES];
        }

        public override void LoadContent()
        {
            textFile = new Customise();
            grassTex = Content.Load<Texture2D>("grass");
            waterTex = Content.Load<Texture2D>("water");
            sandTex = Content.Load<Texture2D>("Sand");
            emptyTex = Content.Load<Texture2D>("Blankimage");
            treeTex = Content.Load<Texture2D>("Tree");

            customTex = textFile.ReadFromFile();
            playerTex = Content.Load<Texture2D>(customTex);

            mobTex = Content.Load<Texture2D>("Mob");
            swordTex = Content.Load<Texture2D>("sword");
            potionTex = Content.Load<Texture2D>("Heal Potion");
            currencyFont = Content.Load<SpriteFont>("fonts");
            smallSettingButton = Content.Load<Texture2D>("SettingButton");
            
            playerPos = new Vector2(4000,800);
            player = new Player(playerTex, playerPos, playerSize, Color.White);
            mobPos = new Vector2(playerPos.X + 120, playerPos.Y);
            mob = new Mob(mobTex, mobPos, mobSize, Color.White);
            sword = new JsonSprite(swordTex, new Vector2(0, 0), new Vector2(150, 150), Color.White);
            potion = new JsonSprite(potionTex, new Vector2(0, 0), new Vector2(150, 150), Color.White);
            #region Perlin Map
            // Initialise the number of tiles to the map
            map = new float[NUMBER_OF_TILES][];
            for (int i = 0; i < NUMBER_OF_TILES; i++)
            {
                map[i] = new float[NUMBER_OF_TILES];
            }

            tileMap = new TileMapClass(map);

            fileManager.WriteToFile(NUMBER_OF_TILES, seedNumber);

            float[,] temp = fileManager.ReadFile(NUMBER_OF_TILES);
            for (int i = 0; i < NUMBER_OF_TILES; i++)
            {
                for (int j = 0; j < NUMBER_OF_TILES; j++)
                {
                    map[i][j] = temp[i, j];
                }
            }
            tileMap = new TileMapClass(map);
            GenerateFloorMap();
            #endregion

            Button SmallSettingButton = new Button(smallSettingButton, font)
            {
                Position = new Vector2(0, 0),
                Size = new Vector2(55, 55),
                Text = "",
            };
            SmallSettingButton.Click += SmallSettingButton_Click;
            _settingComponents = new List<Components>()
            {
                SmallSettingButton,
            };
            cManager = new CameraManager();
            iManager = new CameraManager();
            ai = new Ai();
            items = new JsonFile();
            inventory = new JsonFile();
            currency = new JsonFile();

            nameManager = new TextInputManager(name, nameFont, namePos);
            name = nameManager.ReadFromFile();

            tManager = new TextManager();

            tManager = new TextManager(new Vector2(1840, 0), money, currencyFont);
            items.Inventory(items, swordInt, potionInt);
            currency.CurrencySystem(currency, moneyInt);
            
        }
        private void SmallSettingButton_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice,_graphics, Content));
        }
        public void GenerateFloorMap()
        {
            tileArray = tileMap.CreateMap(tileArray, NUMBER_OF_TILES, SIZE_OF_CELL, grassTex, waterTex, sandTex,treeTex);
        }
        

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _game.ChangeState(new MenuState(_game, _graphicsDevice,_graphics, Content));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                pathfind = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                pathfind = false;
            foreach (Components c in _settingComponents)
                c.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.M))
                bought = true;

            currency.CurrencyCalculator(currency, mobKilled, bought, moneyInt, tManager, money, currencyFont);
            
            grid = new SquareGrid(NUMBER_OF_TILES, NUMBER_OF_TILES);

            aStar = new AStarSearch(grid);
            _movementManager.movement(player);
            _inputManager.CheckKeys(player);

            cManager.Follow(player);
            posOfPlayer = player.PlayerTile(tileArray, player, NUMBER_OF_TILES, posOfPlayer, _graphics);
            posOfMob = mob.MobTile(tileArray, mob, NUMBER_OF_TILES, posOfMob, _graphics);
            if (pathfind)
            {
                inArea = ai.Area(inArea, posOfMob, posOfPlayer, grid, _graphics, radius);
                startingLocation = new Location(posOfMob.x, posOfMob.y);
                endingLocation = new Location(posOfPlayer.x, posOfPlayer.y);
                aStar.CalculatedPath(startingLocation, endingLocation);
                mob = ai.FollowPath(grid, aStar, posOfMob, mob);
            }
            
        }
        public override void PostUpdate(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Tile t in tileArray)
                t.DrawSprite(spriteBatch, t.spriteTexture, cManager);
            foreach (Components e in _settingComponents)
                e.Draw(gameTime, spriteBatch);
            
            player.DrawSprite(spriteBatch, playerTex, cManager);
            mob.DrawSprite(spriteBatch, mob.spriteTexture, cManager);
            tManager.DrawText(spriteBatch);
            
            
            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
            {
                inventory.DisplayOnScreen(items, currency, player, spriteBatch, cManager, sword, potion, emptyTex);
                nameManager.Draw(spriteBatch);
            }
        }
    }
}
