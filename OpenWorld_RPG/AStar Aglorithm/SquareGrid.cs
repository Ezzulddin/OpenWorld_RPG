using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.AStar_Aglorithm
{
    public class SquareGrid : IWeightedGraph<Location>
    {
        public static readonly Location[] Directions = new[]
        {
            new Location(0,1),
            new Location(0,-1),
            new Location(-1,0),
            new Location(1,0),
        };
        public int Rows { get; set; }
        public int Columns { get; set; }
        public SquareGrid(int rows, int columns)
        {
            Columns = columns;
            Rows = rows;
        }

        public HashSet<Location> walls = new HashSet<Location>();
        public bool InBounds(Location id)
        {
            return 0 <= id.X && id.X < Columns &&
                0 <= id.Y && id.Y < Rows;
        }
        public bool Passable(Location id)
        {
            return !walls.Contains(id);
        }
        public double Cost(Location a, Location b)
        {
            return 1;
        }
        public IEnumerable<Location> PassableNeighbors(Location id)
        {
            foreach (Location direction in Directions)
            {
                Location next = new Location(id.X + direction.X, id.Y + direction.Y);

                if (InBounds(next) && Passable(next))
                {
                    yield return next;
                }
            }
        }
    }
}
