using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Classes;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions2019;

public class Solution2019Dec01 : Solution
{
    protected override long FirstSolution(List<string> lines) =>
        lines.Select(long.Parse).Select(FuelRequired).Sum();

    protected override long SecondSolution(List<string> lines)
    {
        var masses = lines.Select(long.Parse).ToList();
        long totalFuel = 0; 
        while (masses.Count > 0)
        {
            masses = masses.Select(FuelRequired).Where(fuel => fuel > 0).ToList();
            totalFuel += masses.Sum();
        }

        return totalFuel;
    }

    private long FuelRequired(long num) => Math.Max(num / 3 - 2, 0);
}

