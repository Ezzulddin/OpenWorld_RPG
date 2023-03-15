using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld_RPG.AStar_Aglorithm
{
    public class AStarSearch
    {
        private List<Location> _calculatedPath;

        public Dictionary<Location, Location> CameFrom = new Dictionary<Location, Location>();

        public Dictionary<Location, double> CostSoFar = new Dictionary<Location, double>();

        public ReadOnlyCollection<Location> Path { get; }

        public Location Start { get; private set; }

        public Location Goal { get; private set; }

        public IWeightedGraph<Location> Graph { get; private set; }

        public AStarSearch(IWeightedGraph<Location> graph)
        {
            _calculatedPath = new List<Location>();
            Path = _calculatedPath.AsReadOnly();
            Graph = graph;
        }

        public void CalculatedPath(Location start, Location goal)
        {
            _calculatedPath.Clear();
            Start = start;
            Goal = goal;
            PrioretyQueue<Location> frontier = new PrioretyQueue<Location>();
            frontier.Enqueue(Start, 0);
            CameFrom[Start] = Start;
            CostSoFar[Start] = 0;

            while (frontier.Count > 0)
            {
                Location currentLocation = frontier.Dequeue();

                if (currentLocation.Equals(Goal))
                    break;

                foreach (Location nextLocation in Graph.PassableNeighbors(currentLocation))
                {
                    double newCost = CostSoFar[currentLocation] + Graph.Cost(currentLocation, nextLocation);
                    if (!CostSoFar.ContainsKey(nextLocation) || newCost < CostSoFar[nextLocation])
                    {
                        CostSoFar[nextLocation] = newCost;
                        double priority = newCost + Heuristic(nextLocation, Goal);
                        frontier.Enqueue(nextLocation, priority);
                        CameFrom[nextLocation] = currentLocation;
                    }
                }
            }

            Location location = Goal;
            while (location != Start)
            {
                _calculatedPath.Add(location);
                location = CameFrom[location];
            }
            _calculatedPath.Add(Start);
            _calculatedPath.Reverse();
        }

        private static double Heuristic(Location a, Location b)
        {
            double dx = Math.Abs(a.X - b.X);
            double dy = Math.Abs(a.Y - b.Y);
            return (dx > dy) ?
                10 * dy + 10 * (dx - dy) :
                10 * dx + 10 * (dy - dx);
        }
    }
}
