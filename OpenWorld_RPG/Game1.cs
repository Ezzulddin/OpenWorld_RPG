using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenWorld_RPG.AStar_Aglorithm;
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
        // Title
        private Title title;
        private Texture2D titleTex;
        private Vector2 titleSize = new Vector2(1250, 400);
        private Vector2 titlePos;

        // Map generation Instances
        #region Map Instances
        private FileManager fileManager = new FileManager();
        private TileMapClass tileMap = new TileMapClass();
        private Tile[,] tileArray;
        private float[][] map;
        const int NUMBER_OF_TILES = 65;
        private int SIZE_OF_CELL = 65;
        private Texture2D emptyTex, grassTex, waterTex, sandTex;
        private int seedNumber = 12345799;
        const int radius = 17;
        #endregion

        // Camera Properties
        #region Camera Instances
        public static int screenWidth;
        public static int screenHeight;
        private CameraManager cManager;
        private CameraManager nullCamera;
        #endregion

        // Player & Mob Instances
        #region Player & Mob Instances
        private Player player;
        private Mob mob;
        private Texture2D playerTex, mobTex;
        private KeyboardManager _movementManager = new KeyboardManager();
        private InputManager _inputManager = new InputManager();
        private TileLocation posOfPlayer = new TileLocation(0,0);
        private TileLocation posOfMob = new TileLocation(0, 0);
        private static Vector2 playerPos = new Vector2(0,0);
        private Vector2 playerSize = new Vector2(100,100);
        private static Vector2 mobPos = new Vector2(0, 0);
        private Vector2 mobSize = new Vector2(100, 100);
        private bool pathfind = false;
        #endregion

        // Button Instances
        #region Button Instances
        private List<Components> _gameComponents, _settingComponents;
        private Texture2D playButton, exitButton, settingButton, smallSettingButton, customiseButton,fullScreenButton,windowScreenButton;
        private SpriteFont font;
        private bool playGame,exitGame,settingGame,customiseGame = false;
        private bool menuButton = true;
        #endregion

        // Json File
        #region Json File Instances
        private JsonFile items, currency;
        private Texture2D swordTex, potionTex;
        private int swordInt, potionInt,currencyInt;
        private JsonSprite sword, potion;
        private SpriteFont currencyFont;
        #endregion

        //Inventory
        private JsonFile inventory;
        private CameraManager iManager;
        private bool tab = false;
        // A* Instances
        #region A* Instances
        private AStarSearch aStar;
        private Location startingLocation = new();
        private Location endingLocation = new();
        private Ai ai;
        private SquareGrid grid;
        private bool inArea;
        #endregion

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
            tileArray = new Tile[NUMBER_OF_TILES, NUMBER_OF_TILES];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Loading Textures
            #region Textures
            grassTex = Content.Load<Texture2D>("grass");
            waterTex = Content.Load<Texture2D>("water");
            sandTex = Content.Load<Texture2D>("Sand");
            emptyTex = Content.Load<Texture2D>("Blankimage");
            playerTex = Content.Load<Texture2D>("TempP");
            mobTex = Content.Load<Texture2D>("Mob");
            playButton = Content.Load<Texture2D>("PlayButton");
            exitButton = Content.Load<Texture2D>("ExitButton");
            smallSettingButton = Content.Load<Texture2D>("SettingButton");
            settingButton = Content.Load<Texture2D>("SettingsButton");
            customiseButton = Content.Load<Texture2D>("CustomiseButton");
            font = Content.Load<SpriteFont>("fonts");
            titleTex = Content.Load<Texture2D>("Logo1");
            swordTex = Content.Load<Texture2D>("sword");
            potionTex = Content.Load<Texture2D>("Heal Potion");
            currencyFont = Content.Load<SpriteFont>("fonts");
            #endregion

            // Loading the procedurally generated map
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

            #region Humanoids
            playerPos = new Vector2(tileArray[NUMBER_OF_TILES/2,NUMBER_OF_TILES/2].spritePosition.X, tileArray[NUMBER_OF_TILES / 2, NUMBER_OF_TILES / 2].spritePosition.Y);
            player = new Player(playerTex, playerPos, playerSize, Color.White);
            mobPos = new Vector2(playerPos.X + 120, playerPos.Y);
            mob = new Mob(mobTex, mobPos, mobSize, Color.White);
            sword = new JsonSprite(swordTex, new Vector2(0,0), new Vector2(150,150), Color.White);
            potion = new JsonSprite(potionTex, new Vector2(0, 0), new Vector2(150, 150), Color.White);
            #endregion

            #region Buttons
            Button PlayButton = new Button(playButton, font)
            {
                Position = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 150, _graphics.PreferredBackBufferHeight / 2 - 50),
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

            Button SettingButton = new Button(settingButton, font)
            {
                Position = new Vector2(CustomiseButton.Rectangle.X, CustomiseButton.Rectangle.Y + 105),
                Size = new Vector2(255, 100),
                Text = "",
            };
            SettingButton.Click += SettingButton_Click;

            Button FullScreenButton = new Button(fullScreenButton, font)
            {
                Position = new Vector2(650,450),
                Size = new Vector2(255,100),
                Text = "",
            };
            FullScreenButton.Click += FullScreenButton_Click;
            
            Button WindowScreenButton = new Button(windowScreenButton, font)
            {
                Position = new Vector2(FullScreenButton.Rectangle.X + 150, 450),
                Size = new Vector2(255, 100),
                Text = "",
            };
            WindowScreenButton.Click += WindowScreenButton_Click;

            Button SmallSettingButton = new Button(smallSettingButton, font)
            {
                Position = new Vector2(0, 0),
                Size = new Vector2(55, 55),
                Text = "",
            };
            SmallSettingButton.Click += SmallSettingButton_Click;

            Button ExitButton = new Button(exitButton, font)
            {
                Position = new Vector2(SettingButton.Rectangle.X, SettingButton.Rectangle.Y + 100),
                Size = new Vector2(255, 100),
                Text = "",
            };
            ExitButton.Click += ExitButton_Click;

            _gameComponents = new List<Components>()
            {
                PlayButton,
                ExitButton,
                SettingButton,
                CustomiseButton,
            };
            _settingComponents = new List<Components>()
            {
                SmallSettingButton,
            };
            #endregion

            #region Setting Instances
            cManager = new CameraManager();
            iManager = new CameraManager();
            nullCamera = new CameraManager();
            ai = new Ai();
            items = new JsonFile();
            inventory = new JsonFile();
            #endregion
            items.Inventory(items, swordInt, potionInt);
        }
        #region Button Clicks
        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            playGame = true;
            menuButton = false;
        }
        private void CustomiseButton_Click(object sender, System.EventArgs e)
        {
            customiseGame = true;
        }
        private void SettingButton_Click(object sender, System.EventArgs e)
        {
            settingGame = true;
        }
        private void SmallSettingButton_Click(object sender, System.EventArgs e)
        {
            playGame = false;
        }
        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            exitGame = true;
        }
        private void FullScreenButton_Click(object sender, System.EventArgs e)
        {

        }
        private void WindowScreenButton_Click(object sender, System.EventArgs e)
        {

        }
        #endregion
        public void GenerateFloorMap()
        {
            tileArray = tileMap.CreateMap(tileArray,NUMBER_OF_TILES ,SIZE_OF_CELL, grassTex, waterTex, sandTex);
        }
        
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                playGame = false;
            
            if (exitGame)
                Exit();

            
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                pathfind = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                pathfind = false;

            if (playGame == false)
            {
                foreach (Components c in _gameComponents)
                    c.Update(gameTime);
            }
            foreach (Components c in _settingComponents)
                c.Update(gameTime);

            if(Keyboard.GetState().IsKeyDown(Keys.I))
            {
                swordInt = 1;
                items.Inventory(items, swordInt, potionInt);
            }
            titlePos = new Vector2(player.spritePosition.X - titleSize.X / 2 + 50, player.spritePosition.Y - titleSize.Y);
            title = new Title(titleTex, titlePos, titleSize, Color.White);

            grid = new SquareGrid(NUMBER_OF_TILES,NUMBER_OF_TILES);

            aStar = new AStarSearch(grid);
            _movementManager.movement(player);
            _inputManager.CheckKeys(player, _graphics);

            cManager.Follow(player);
            
            // This is where the location and calculation of A* updates every 60 frame per second
            posOfPlayer = player.PlayerTile(tileArray, player, NUMBER_OF_TILES, posOfPlayer, _graphics);
            posOfMob = mob.MobTile(tileArray, mob, NUMBER_OF_TILES, posOfMob, _graphics);
            inArea = ai.Area(inArea, posOfMob, posOfPlayer, grid, _graphics, radius);
            if (playGame == true && pathfind == true && inArea == true)
            {
                startingLocation = new Location(posOfMob.x, posOfMob.y);
                endingLocation = new Location(posOfPlayer.x, posOfPlayer.y);
                aStar.CalculatedPath(startingLocation, endingLocation);
                mob = ai.FollowPath(grid, aStar, posOfMob, mob);
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            if(playGame == false && menuButton==true)
            {
                
                //Draw all buttons within the list of _gameComponents; Play, Customise, Settings, Quit.
                foreach (Components e in _gameComponents)
                    e.Draw(gameTime, _spriteBatch);

                title.DrawSprite(_spriteBatch, title.spriteTexture, cManager);
            }

            if (playGame == true && menuButton == false)
            {
                foreach(Tile t in tileArray)
                    t.DrawSprite(_spriteBatch, t.spriteTexture, cManager);

                foreach (Components e in _settingComponents)
                    e.Draw(gameTime, _spriteBatch);

                player.DrawSprite(_spriteBatch, player.spriteTexture, cManager);
                mob.DrawSprite(_spriteBatch, mob.spriteTexture, cManager);
                if (Keyboard.GetState().IsKeyDown(Keys.Tab))
                {
                    inventory.DisplayOnScreen(items, currency, player, _spriteBatch, cManager, sword, potion, emptyTex);   
                }
            }
            if(settingGame == true)
            {
                playGame = false;
                menuButton = false;

            }
            base.Draw(gameTime);
        }
    }
}