using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Transactions;
using System.Xml;
using System.Xml.Schema;
using OldAdventOfCode.Classes;
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2021;

public class Solution2021Dec17 : Solution
{
    private (int, int) _xTarget; 
    private (int, int) _yTarget;
    private long _yHighest = 0;
    private HashSet<(int, int)> _validVelocities = new();

    private bool InTarget(int x, int y) =>
        x >= _xTarget.Item1 && x <= _xTarget.Item2 && y >= _yTarget.Item1 && y <= _yTarget.Item2;
 
    protected override long FirstSolution(List<string> lines)
    {
        ReadInputs(lines);
        RunAnalysis(false);
        return _yHighest;
    }

    protected override long SecondSolution(List<string> lines)
    {
        _validVelocities = new HashSet<(int, int)>();
        RunAnalysis(true);
        return _validVelocities.Count;
    }

    private void ReadInputs(IReadOnlyList<string> lines)
    {
        var parts = lines[0].Split(" ");
        var xRange = parts[2][2..^1].Split("..").Select(int.Parse).ToList();
        _xTarget = (xRange[0], xRange[1]);
        var yRange = parts[3][2..].Split("..").Select(int.Parse).ToList();
        _yTarget = (yRange[0], yRange[1]); 
    }

    private void RunAnalysis(bool allowNegativeY)
    {
        var maxX = _xTarget.Item2;
        var minX = (int)Math.Floor(Math.Sqrt(2 * +_xTarget.Item1));
        var maxY = _yTarget.Item1 < 0 ? Math.Abs(_yTarget.Item1) : _yTarget.Item2 + 1;
        var minY = allowNegativeY && _yTarget.Item1 < 0 ? _yTarget.Item1 : 1;
        for (var x = minX; x <= maxX; x++)
        for (var y = minY; y <= maxY; y++) 
            TestTrajectory(x, y);
    }
    

    private void TestTrajectory(int x, int y)
    {
        var inputs = (x, y);
        var yMax = 0;
        var xLocation = 0;
        var yLocation = 0;
        while (x > 0 || yLocation > _yTarget.Item2)
        {
            xLocation += x;
            yLocation += y;
            yMax = y > 0 ? yLocation : yMax;
            y--;
            x = x == 0 ? 0 : x - 1;
            if (!InTarget(xLocation, yLocation)) continue;
            _yHighest = yMax > _yHighest ? yMax : _yHighest;
            _validVelocities.Add(inputs);
            break;
        }
    }
}