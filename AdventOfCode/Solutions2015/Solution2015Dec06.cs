using System.Numerics;
using System.Text.RegularExpressions;
using AdventOfCode.Maps;

namespace AdventOfCode.Solutions2015;

public partial class Solution2015Dec06 : Solution<long?>
{
    [GeneratedRegex(@"(^\D+)(\d{1,3}),(\d{1,3})\D+(\d{1,3}),(\d{1,3})$")]
    private static partial Regex MyRegex();

    private Map<Location<int>, int> _lightGrid = null!;
    
    protected override long? FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long? SecondSolution(List<string> lines) => GeneralSolution(lines, false);
    
    private static void FollowInstruction(Instruction instruction, Action<string, float, float> action)
    {
        for (var x = instruction.Start.X; x <= instruction.End.X; x++)
        for (var y = instruction.Start.Y; y <= instruction.End.Y; y++)
            action(instruction.Action, x, y);
    }
    
    private long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        _lightGrid = new Map<Location<int>, int>(1000, 1000, () => new Location<int>(0));
        lines.Select(line => new Instruction(MyRegex().Match(line).Groups)).ToList()
            .ForEach(instruction => FollowInstruction(instruction, isFirstSolution ? PerformAction1 : PerformAction2));
        return _lightGrid.Locations.Sum(pair => pair.Value.Value);
    }
    
    private void PerformAction1(string action, float x , float y)
    {
        _lightGrid.Locations[new Vector2(x, y)].Value = action switch
        {
            "turn on " => 1,
            "toggle " => _lightGrid.Locations[new Vector2(x, y)].Value == 0 ? 1 : 0,
            "turn off " => 0,
            _ => _lightGrid.Locations[new Vector2(x, y)].Value
        };
    }
    
    private void PerformAction2(string action, float x , float y)
    {
        switch (action)
        {
            case "turn on ":
                _lightGrid.Locations[new Vector2(x, y)].Value ++;
                break;
            case "toggle ":
                _lightGrid.Locations[new Vector2(x, y)].Value += 2;
                break;
            case "turn off ":
                if (_lightGrid.Locations[new Vector2(x, y)].Value == 0) break;
                _lightGrid.Locations[new Vector2(x, y)].Value --;
                break;
        }
    }
    
    private class Instruction(GroupCollection captures)
    {
        public string Action { get; } = captures[1].Value;
        public Vector2 Start { get; } = new(int.Parse(captures[2].Value), int.Parse(captures[3].Value));
        public Vector2 End { get; } = new(int.Parse(captures[4].Value), int.Parse(captures[5].Value));
    }
}