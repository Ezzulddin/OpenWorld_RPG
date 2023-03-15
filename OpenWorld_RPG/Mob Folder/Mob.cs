using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenWorld_RPG.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.Mob_Folder
{
    public class Mob : Sprite
    {
        public Mob(Texture2D tex, Vector2 pos, Vector2 size, Color colour) : base(tex, pos, size, colour) { }
        public TileLocation MobTile(Tile[,] tileArray, Mob m, int tiles, TileLocation mPos, GraphicsDeviceManager graphics)
        {
            for (int i = 0; i < tiles; i++)
            {
                for (int j = 0; j < tiles; j++)
                {
                    if (m.spritePosition.X >= tileArray[i, j].spritePosition.X && m.spritePosition.X <= tileArray[i, j].spritePosition.X + graphics.PreferredBackBufferWidth
                        && m.spritePosition.Y >= tileArray[i, j].spritePosition.Y && m.spritePosition.Y <= tileArray[i, j].spritePosition.Y + graphics.PreferredBackBufferHeight)
                    {
                        mPos.x = i;
                        mPos.y = j;
                    }
                }
            }
            return mPos;
        }

    }
}
