using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Classes;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions2016;

public class Solution2016Dec01 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var map = new MapWithCardinalMovement();
        map.SetDirection(MapWithCardinalMovement.Direction.N);
        lines[0].Split(",").Select(s => s.Trim()).ToList().ForEach(move =>
        {
            if (move[0..1] == "R") map.RotateRight();
            else map.RotateLeft();
            map.Move(int.Parse(move[1..(move.Length)]));
        });
        return (long)map.DistanceFromOrigin(false);
    }

    protected override long SecondSolution(List<string> lines)
    {
        var locationsVisited = new HashSet<(int, int)>();
        var map = new MapWithCardinalMovement();
        map.SetDirection(MapWithCardinalMovement.Direction.N);
        foreach (var move in lines[0].Split(",").Select(s => s.Trim()).ToList())
        {
            if (move[0..1] == "R") map.RotateRight();
            else map.RotateLeft();
            var block = 0;
            while (block < int.Parse(move[1..(move.Length)]))
            {
                map.Move(1);
                if (!locationsVisited.Add(map.Location)) return (long)map.DistanceFromOrigin(false);
                block++;
            }
        }

        return (long)map.DistanceFromOrigin(false);
        }
    }
