using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utilities;

namespace AdventOfCode2021.Solutions;

public class Solution02 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        return MapLinesToActions(lines).Aggregate(StartingLocation(), CalculateLocation).Take(2).Product();
    }

    protected override long SecondSolution(List<string> lines)
    {
        return MapLinesToActions(lines).Aggregate(StartingLocation(), CalculateLocationWithAim).Take(2).Product();
    }

    private static (int, int) CalculateAction(string[] strings)
    {
        var units = int.Parse(strings.Last());
        return strings.First() == "forward" ? (units, 0) : (0, strings.First() == "up" ? -units : units);
    }

    private static List<int> CalculateLocation(List<int> state, (int, int) action)
    {
        return new() { action.Item1 + state[0], action.Item2 + state[1] };
    }

    private static List<int> CalculateLocationWithAim(List<int> state, (int, int) action)
    {
        return new() { action.Item1 + state[0], action.Item1 * state[2] + state[1], action.Item2 + state[2] };
    }

    private static List<int> StartingLocation()
    {
        return new() { 0, 0, 0 };
    }

    private static IEnumerable<(int, int)> MapLinesToActions(IEnumerable<string> lines)
    {
        return lines.Select(line => line.Split()).Select(CalculateAction);
    }
}