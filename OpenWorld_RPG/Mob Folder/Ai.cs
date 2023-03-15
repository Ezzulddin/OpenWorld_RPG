using Accessibility;
using Microsoft.Xna.Framework;
using OpenWorld_RPG.AStar_Aglorithm;
using OpenWorld_RPG.Map;
using OpenWorld_RPG.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.Mob_Folder
{
    public class Ai
    {
        private KeyboardManager mManager;
        public Mob FollowPath(SquareGrid grid, AStarSearch aStar, TileLocation posOfM, Mob mob)
        {
            mManager = new();
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int column = 0; column < grid.Columns; column++)
                {
                    Location cell = new Location(column, row);

                    if (aStar.Path.Contains(cell))
                    {
                        if (posOfM.x <= cell.X && posOfM.y == cell.Y)
                        {
                            mManager.GoRight(mob);
                        }
                        if (posOfM.x >= cell.X && posOfM.y == cell.Y)
                        {
                            mManager.GoLeft(mob);

                        }
                        if (posOfM.x == cell.X && posOfM.y <= cell.Y)
                        {
                            mManager.GoDown(mob);

                        }
                        if (posOfM.x == cell.X && posOfM.y >= cell.Y)
                        {
                            mManager.GoUp(mob);
                        }
                    }
                }
            }
            return mob;
        }
        public bool Area(bool inArea, TileLocation mob, TileLocation p, SquareGrid grid,GraphicsDeviceManager graphics, int rad)
        {
            for(int i = 0; i < grid.Rows; i++)
            {
                for(int j = 0; j < grid.Columns; j++)
                {
                    if(p.x >= grid.Rows && p.y >= grid.Columns || p.x <= grid.Rows && p.y <= grid.Columns)
                    {
                        inArea = true;
                    }
                    else
                    {
                        inArea = false;
                    }
                    
                }
            }
            return inArea;
        }
    }
}
