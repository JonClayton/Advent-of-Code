using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions2021;

public class Solution2021Dec05 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, false);
    }

    protected override long SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, true);
    }

    private static int GeneralSolution(IEnumerable<string> lines, bool isDiagonalAllowed)
    {
        var seenOnce = new HashSet<(int, int)>();
        var seenTwice = new HashSet<(int, int)>();
        lines
            .Select(line => new LineSegment(line))
            .SelectMany(ls => ls.Points(isDiagonalAllowed))
            .Where(point => !seenOnce.Add(point)).ToList()
            .ForEach(point => seenTwice.Add(point));
        return seenTwice.Count;
    }
}

internal class LineSegment
{
    private readonly bool _isDiagonal;
    private readonly ValueTuple<int, int> _point0;
    private readonly ValueTuple<int, int> _point1;

    public LineSegment(string input)
    {
        var pairs = input.Split(" -> ").Select(part => part.Split(","))
            .Select(p => (int.Parse(p[0]), int.Parse(p[1]))).ToList();
        _point0 = pairs[0];
        _point1 = pairs[1];
        var (dx, dy) = DxDy();
        _isDiagonal = Math.Min(Math.Abs(dx), Math.Abs(dy)) > 0;
    }

    public IEnumerable<(int, int)> Points(bool isDiagonalAllowed = true)
    {
        var points = new List<ValueTuple<int, int>> { _point1 };
        if (_isDiagonal && !isDiagonalAllowed) return points;
        var (dx, dy) = DxDy();
        var length = Math.Max(Math.Abs(dx), Math.Abs(dy));
        for (var i = 0; i < length; i++)
            points.Add((_point0.Item1 + i * dx / length, _point0.Item2 + i * dy / length));
        return points;
    }

    private (int, int) DxDy()
    {
        var dx = _point1.Item1 - _point0.Item1;
        var dy = _point1.Item2 - _point0.Item2;
        return (dx, dy);
    }
}