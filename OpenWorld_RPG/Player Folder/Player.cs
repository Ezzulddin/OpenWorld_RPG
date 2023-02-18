using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenWorld_RPG.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.PlayerFolder
{
    public class Player : Sprite
    {
        public Vector2 projectedPos;
        public bool hasCollidedTop = false;
        public bool hasCollidedBottom = false;
        public bool hasCollidedLeft = false;
        public bool hasCollidedRight = false;

        public bool goingLeft;
        public bool goingRight;
        public bool goingUp;
        public bool goingDown;
        public bool sprinting;

        public Player(Texture2D tex, Vector2 pos, Vector2 size, Color colour) : base(tex,pos,size,colour)
        {
        }
        
        public TileLocation PlayerTile(Tile[,] tileArray,Player p,int tiles,TileLocation pPos ,GraphicsDeviceManager graphics)
        {
            for(int i = 0; i<tiles;i++)
            {
                for(int j = 0; j<tiles;j++)
                { 
                    if(p.spritePosition.X >= tileArray[i,j].spritePosition.X && p.spritePosition.X <= tileArray[i,j].spritePosition.X + graphics.PreferredBackBufferWidth 
                        && p.spritePosition.Y >= tileArray[i,j].spritePosition.Y && p.spritePosition.Y<= tileArray[i,j].spritePosition.Y + graphics.PreferredBackBufferHeight)
                    {
                        pPos.x = i;
                        pPos.y = j;
                    }
                }
            }
            return pPos;
        }
    }
}
