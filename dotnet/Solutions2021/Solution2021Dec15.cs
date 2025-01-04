using System.Collections.Generic;
using System.Linq;
using OldAdventOfCode.Classes;
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2021;

public class Solution2021Dec15 : Solution
{
    protected override long FirstSolution(List<string> lines) => new ChitonCavern(lines, true).FindRiskOfBestPath();

    protected override long SecondSolution(List<string> lines) => new ChitonCavern(lines, false).FindRiskOfBestPath();
    
    private class Location : LocationWithNeighbors, IIndexedValue
    {
        public int? LowestRiskToHere;
        public int? IndexedValue => LowestRiskToHere;
    }

    private class ChitonCavern : MapWithNeighbors<Location>
    {
        private Location _destination;
        
        public ChitonCavern(IReadOnlyList<string> lines, bool isCavernSmall) : base(lines)
        {
            var width = lines[0].Length;
            var height = lines.Count;
            if (isCavernSmall) _destination = Locations[(width - 1, height - 1)];
            else Expand5X5(lines, height, width);
        }

        public long FindRiskOfBestPath()
        {
            var start = Locations[(0,0)];
            foreach (var (_, value) in Locations)
            {
                value.LowestRiskToHere = null;
                value.Neighbors = new HashSet<LocationWithNeighbors>();
                AssignNeighbors(value);
            }
            start.LowestRiskToHere = 0;
            var horizonOfKnownTotalRisks =
                new IndexedStacks<Location>(new List<Location> { start });
            while (true)
            {
                var location = horizonOfKnownTotalRisks.Pop();
                foreach (var neighbor in location.Neighbors.Cast<Location>()
                             .Where(neighbor => !neighbor.LowestRiskToHere.HasValue))
                {
                    var riskToNeighbor= location.LowestRiskToHere + neighbor.Value ?? int.MaxValue;
                    if (neighbor.Equals(_destination)) return riskToNeighbor;
                    neighbor.LowestRiskToHere = riskToNeighbor;
                    horizonOfKnownTotalRisks.Add(neighbor, riskToNeighbor);
                }
            }
        }

        private void Expand5X5(IReadOnlyList<string> lines, int height, int width)
        {
            var oneToFour = new List<int> { 1, 2, 3, 4 };
            var firstLocationsList = Locations.ToList();
            oneToFour.ForEach(i =>
            {
                foreach (var ((col, row), location) in firstLocationsList)
                {
                    var newValue = location.Value + i;
                    var newLocation = new Location
                    {
                        Row = row,
                        Column = col + width * i,
                        Value = newValue > 9 ? newValue - 9 : newValue
                    };
                    Locations.Add((newLocation.Column, row), newLocation);
                }
            });
            var secondLocationsList = Locations.ToList();
            oneToFour.ForEach(i =>
            {
                foreach (var ((col, row), location) in secondLocationsList)
                {
                    var newValue = location.Value + i;
                    var newLocation = new Location
                    {
                        Row = row + height * i,
                        Column = col,
                        Value = newValue > 9 ? newValue - 9 : newValue
                    };
                    Locations.Add((col, newLocation.Row), newLocation);
                }
            });
            _destination = Locations[(width * 5 - 1, height * 5 - 1)];
        }
    }
}