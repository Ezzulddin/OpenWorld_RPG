﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace OpenWorld_RPG.Map
{
    public class TileMapClass
    {
        private float[][] tilesArrayF;

        public TileMapClass()
        {
        }

        public TileMapClass(float[][] map)
        {
            this.tilesArrayF = map;
        }

        public Tile[,] CreateMap(Tile[,] tileArray,int numoftiles, int tileSize, Texture2D grassTex,
            Texture2D waterTex, Texture2D sandTex,Texture2D treeTex)
        {
            for (int i = 0; i < numoftiles; i++)
            {
                for (int j = 0; j < numoftiles; j++)
                {
                    if (tilesArrayF[i][j] >= 0 && tilesArrayF[i][j] < 0.45)
                    {
                        tileArray[i, j] = new Tile(waterTex, new Vector2(tileSize * i, tileSize * j), new Vector2(tileSize, tileSize), Color.White);
                    }
                    else if (tilesArrayF[i][j] >= 0.45 && tilesArrayF[i][j] < 0.55)
                    {
                        tileArray[i, j] = new Tile(waterTex, new Vector2(tileSize * i, tileSize * j), new Vector2(tileSize, tileSize), Color.SkyBlue);
                    }
                    else if (tilesArrayF[i][j] >= 0.55 && tilesArrayF[i][j] < 0.65)
                    {
                        tileArray[i, j] = new Tile(sandTex, new Vector2(tileSize * i, tileSize * j), new Vector2(tileSize, tileSize), Color.White);
                    }
                    else if(tilesArrayF[i][j] >= 0.65)
                    {
                        tileArray[i, j] = new Tile(grassTex, new Vector2(tileSize * i, tileSize * j), new Vector2(tileSize, tileSize), Color.White);
                    }
                    
                }
            }
            
            return tileArray;
        }
        
    }
}
