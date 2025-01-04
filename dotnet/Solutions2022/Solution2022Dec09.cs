using System.Numerics;
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2022;

public class Solution2022Dec09 : Solution
{
    private readonly Dictionary<char, Vector2> _directions = new()
    {
        { 'D', -Vector2.UnitY },
        { 'L', -Vector2.UnitX },
        { 'R', Vector2.UnitX },
        { 'U', Vector2.UnitY },
    };

    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, 2);
    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, 10);

    private long GeneralSolution(IEnumerable<string> lines, int knotCount)
    {
        return lines.Aggregate((Enumerable.Range(0, knotCount).Select(i => Vector2.Zero).ToList(),
                new HashSet<Vector2> { Vector2.Zero }),
            (pair, line) =>
            {
                var (currentKnotLocations, cabooseLocations) = pair;
                var direction = _directions[line[0]];
                var distance = int.Parse(line.Split(" ")[1]);
                Enumerable.Range(0, distance).Select(i => direction).ToList().ForEach(move =>
                {
                    currentKnotLocations = currentKnotLocations.Select((knot, i) =>
                    {
                        knot += move;
                        if (i < knotCount - 1)
                            move = (knot - currentKnotLocations[i + 1]).Length() >= 2
                                ? Vector2.Clamp(knot - currentKnotLocations[i + 1], -Vector2.One, Vector2.One)
                                : Vector2.Zero;
                        else cabooseLocations.Add(knot);
                        return knot;
                    }).ToList();
                });
                return (currentKnotLocations, cabooseLocations);
            }).Item2.Count;
    }
}