using Microsoft.Xna.Framework;
using OpenWorld_RPG.PlayerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.Map
{
    public class TileLocation
    {
        public int x { get; set; }
        public int y { get; set; }
        public TileLocation(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }
}
