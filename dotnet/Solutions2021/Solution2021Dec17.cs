using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Transactions;
using System.Xml;
using System.Xml.Schema;
using AdventOfCode.Classes;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions2021;

public class Solution2021Dec17 : Solution
{
    private HashSet<int> _xTarget;
    private HashSet<int> _yTarget;
    protected override long FirstSolution(List<string> lines)
    {
        var parts = lines[0].Split(" ");
        var xRange = parts[2][2..^1].Split("..").Select(int.Parse).ToList();
        _xTarget = Enumerable.Range(xRange[0], xRange[1] - xRange[0]).ToHashSet();
        var yRange = parts[3][2..].Split("..").Select(int.Parse).ToList();
        _yTarget = Enumerable.Range(yRange[0], yRange[1] - yRange[0]).ToHashSet();
        var maxX = xRange[1];
        var minX = Math.Sqrt(2 * xRange[0]);
        var minXInt = minX == Math.Floor(minX) ? (int)minX : (int)minX + 1; 
        for (var x = minX
    }

    protected override long SecondSolution(List<string> lines) => 5;

    private bool LocationInTargetRange((int, int) location) =>
        _xTarget.Contains(location.Item1) && _yTarget.Contains(location.Item2);
}