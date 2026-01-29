namespace AdventOfCode.Solutions2015;

public class Solution2015Dec03 : Solution<long?>
{
    private static readonly Dictionary<char, Vector2> Moves = new()
    {
        { '^', Vector2.UnitY },
        { 'v', -Vector2.UnitY },
        { '>', Vector2.UnitX },
        { '<', -Vector2.UnitX }
    };

    protected override long? FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, -1);
    }

    protected override long? SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, -2);
    }

    private static long GeneralSolution(List<string> lines, int index)
    {
        return lines.First()
            .Aggregate(new List<Vector2> { Vector2.Zero, Vector2.Zero },
                (agg, location) => LocationAggregator(agg, location, index)).ToHashSet().Count;
    }

    private static List<Vector2> LocationAggregator(List<Vector2> locations, char move, int index)
    {
        var prior = locations[^-index];
        locations.Add(prior + Moves[move]);
        return locations;
    }
}