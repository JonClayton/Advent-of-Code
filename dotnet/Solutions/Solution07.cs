using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utilities;

namespace AdventOfCode2021.Solutions;

public class Solution07 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, false);
    }

    protected override long SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, true);
    }

    private static int GeneralSolution(IEnumerable<string> lines, bool isCostIncreasing)
    {
        var locations = lines.First().Split(",").Select(int.Parse).ToList();
        if (isCostIncreasing)
        {
            var mean = (float)locations.Sum() / locations.Count;
            var targetLocations = new List<int> { (int)Math.Floor(mean) };
            if (mean > targetLocations.First()) targetLocations.Add(targetLocations.First() + 1);
            return targetLocations.Select(t => locations
                    .Select(l => (Math.Abs(l - t) + 1) * Math.Abs(l - t) / 2)
                    .Sum())
                .Min();
        }

        locations.Sort();
        var median = locations[locations.Count / 2];
        return locations.Aggregate(0, (a, l) => a + Math.Abs(l - median));
    }
}