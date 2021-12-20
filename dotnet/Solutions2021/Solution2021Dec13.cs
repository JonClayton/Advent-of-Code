using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions2021;

public class Solution2021Dec13 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var sheet = new SecretSheet(lines);
        sheet.Fold(1);
        return sheet.Count;
    }

    protected override long SecondSolution(List<string> lines)
    {
        var sheet = new SecretSheet(lines);
        sheet.Fold(int.MaxValue);
        sheet.Print();
        return sheet.Count;
    }

    private static long GeneralSolution(List<string> lines, bool isBooHooToYouToo)
    {
        var sheet = new SecretSheet(lines);
        sheet.Fold(1);
        return sheet.Count;
    }
}

internal class SecretSheet
{
    private HashSet<(int, int)> _dots = new();
    private readonly List<(char, int)> _folds = new();

    public SecretSheet(List<string> lines)
    {
        var line = lines[0];
        var index = 0;
        while (!string.IsNullOrWhiteSpace(line))
        {
            var xy = line.Split(",").Select(int.Parse).ToList();
            _dots.Add((xy[0], xy[1]));
            index++;
            line = lines[index];
        }

        index++;
        while (index < lines.Count)
        {
            var parts = lines[index].Split("=");
            _folds.Add((parts[0].Last(), int.Parse(parts[1])));
            index++;
        }
    }

    public int Count => _dots.Count;

    public void Fold(int times)
    {
        var i = Math.Min(times, _folds.Count);
        Enumerable.Range(0, i).ToList().ForEach(i => Fold(_folds[i].Item1 == 'x', _folds[i].Item2));
    }

    private void Fold(bool isVertical, int location)
    {
        var xMark = isVertical ? location : int.MaxValue;
        var yMark = isVertical ? int.MaxValue : location;
        var newDots = new HashSet<(int, int)>();
        foreach (var (x, y) in _dots)
        {
            if (x == xMark || y == yMark) continue;
            var newX = x < xMark ? x : 2 * xMark - x;
            var newY = y < yMark ? y : 2 * yMark - y;
            newDots.Add((newX, newY));
        }

        _dots = newDots;
    }

    public void Print()
    {
        var dots = _dots.ToList();
        var columns = dots.Select(dot => dot.Item1).Max() + 1;
        var rows = dots.Select(dot => dot.Item2).Max() + 1;
        var emptyRow = Enumerable.Range(0, columns).Select(_ => ".");
        var board = Enumerable.Range(0, rows).Select(_ => emptyRow.ToList()).ToList();
        foreach (var (x, y) in dots) board[y][x] = "#";
        var printing = board.Select(row => string.Join(string.Empty, row));
        foreach (var line in printing) Console.WriteLine(line);
    }
}