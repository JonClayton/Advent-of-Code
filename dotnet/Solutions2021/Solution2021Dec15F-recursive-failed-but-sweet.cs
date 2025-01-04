using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Xml.Schema;
using OldAdventOfCode.Classes;
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2021;

public class Solution2021Dec15F : Solution
{
    protected override long FirstSolution(List<string> lines) => new ChitonCavern(lines).FindBestPath().TotalRisk;

    protected override long SecondSolution(List<string> inputLines) => 5;
    


    private class ChitonPath
    {
        public Location Location;
        public HashSet<Location> LocationsVisited;
        public long TotalRisk;

        public ChitonPath(Location location, long totalRisk, HashSet<Location> locationsVisited)
        {
            Location = location;
            TotalRisk = totalRisk;
            LocationsVisited = locationsVisited.ToList().ToHashSet();
        }
    }

    private class Location : LocationWithNeighbors
    {
        public long LowestRiskToHere = long.MaxValue;
    }

    private class ChitonCavern : MapWithNeighbors<Location>
    {
        private readonly Location _destination;
        public ChitonCavern(IReadOnlyList<string> lines) : base(lines)
        {
            _destination = Locations[(lines[0].Length - 1, lines.Count - 1)];
        }

        public ChitonPath FindBestPath()
        {
            var location = Locations[(0,0)];
            var path = new ChitonPath(location, 0, new HashSet<Location>());
            return FindBestStep(path);
        }

        private ChitonPath FindBestStep(ChitonPath path)
        {
            Console.WriteLine($"at {path.Location.Row},{path.Location.Column}");
            if (path.Location.Equals(_destination)) return path;
            var bestPath = new ChitonPath(path.Location, long.MaxValue, new HashSet<Location>());
            if (path.Location.LowestRiskToHere <= path.TotalRisk) return bestPath;
            path.Location.LowestRiskToHere = path.TotalRisk;
            foreach (var rootLocation in path.Location.Neighbors)
            {
                var location = rootLocation as Location;
                if (path.LocationsVisited.Contains(location)) continue;
                var newLocationsVisited = path.LocationsVisited.ToList().ToHashSet();
                newLocationsVisited.Add(location);
                var nextPath =
                    FindBestStep(new ChitonPath(location, path.TotalRisk + location.Value, newLocationsVisited));
                bestPath = bestPath.TotalRisk > nextPath.TotalRisk ? nextPath : bestPath;
            }

            return bestPath;
        }
    }
}