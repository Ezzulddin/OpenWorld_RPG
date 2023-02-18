using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenWorld_RPG.Map;
using OpenWorld_RPG.PlayerFolder;
using System;
using System.Net;

namespace OpenWorld_RPG
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        // Map generation Instances
        #region Map Instances
        private FileManager fileManager = new FileManager();
        private TileMapClass tileMap = new TileMapClass();
        private Tile[,] tileArray;
        private float[][] map;
        const int NUMBER_OF_TILES = 200;
        private int SIZE_OF_CELL = 100;
        private Texture2D emptyTex, grassTex, waterTex, sandTex;
        private int seedNumber = 12345699;
        private int radius = 11;
        #endregion

        // Camera Properties
        #region Camera Instances
        public static int screenWidth;
        public static int screenHeight;
        private CameraManager cManager;
        private CameraManager nullCamera;
        #endregion

        // Player Instances
        #region Player Instances
        private Player player;
        private Texture2D playerTex;
        private KeyboardManager _movementManager = new KeyboardManager();
        private InputManager _inputManager = new InputManager();
        private TileLocation posOfPlayer = new TileLocation(0,0);
        private static Vector2 playerPos = new Vector2(0,0);
        private Vector2 playerSize = new Vector2(50,50);
        #endregion

        // Camera Instances
        #region Camera Instances
        
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
            #endregion
            #region Setting Instances
            cManager = new CameraManager();
            nullCamera = new CameraManager();
            #endregion
            
        }
        public void GenerateFloorMap()
        {
            tileArray = tileMap.CreateMap(tileArray,NUMBER_OF_TILES ,SIZE_OF_CELL, grassTex, waterTex, sandTex);
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //posOfPlayer = playerVicinity.PlayerPos(tileArray, player, posOfPlayer, _graphics);
            posOfPlayer = player.PlayerTile(tileArray, player, NUMBER_OF_TILES, posOfPlayer, _graphics);
            _movementManager.movement(player);
            _inputManager.CheckKeys(player, _graphics);
            cManager.Follow(player);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // The foreach function will draw the tileArray values (textures)


            for (int i = posOfPlayer.x - radius; i < posOfPlayer.x + radius; i++)
            {
                for (int j = posOfPlayer.y - radius; j < posOfPlayer.y + radius; j++)
                {
                    tileArray[i, j].DrawSprite(_spriteBatch, tileArray[i, j].spriteTexture, cManager);
                }
            }
            //foreach (Tile tile in tileArray)
            //{
            //    tile.DrawSprite(_spriteBatch, tile.spriteTexture, cManager);
            //}

            player.DrawSprite(_spriteBatch,player.spriteTexture,cManager);

            base.Draw(gameTime);
        }
    }
}