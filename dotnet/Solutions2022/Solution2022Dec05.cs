using System.Text;

namespace AdventOfCode.Solutions2022;

public class Solution2022Dec05 : Solution
{
    protected override long FirstSolution(List<string> lines) => throw new NotImplementedException();
    protected override long SecondSolution(List<string> lines) => throw new NotImplementedException();
    protected override string FirstStringSolution(List<string> lines) => GeneralSolution(lines, true);
    protected override string SecondStringSolution(List<string> lines) => GeneralSolution(lines, false);

    private static string GeneralSolution(List<string> lines, bool oneAtATime) =>
        lines.Aggregate(new List<Stack<char>>(), (stacks, line) =>
            {
                if (string.IsNullOrWhiteSpace(line)) return stacks;
                return line[1] switch
                {
                    '1' => ConvertChartInformation(stacks),
                    'o' => MoveCrates(line, stacks, oneAtATime),
                    _ => ProcessInitialStackChart(line, stacks)
                };
            }).Where(stack => stack.Any()).Select(stack => stack.Pop())
            .Aggregate(new StringBuilder(), (sb, c) => sb.Append(c)).ToString();

    // This somewhat surprisingly reverses the order of the stacks
    private static List<Stack<char>> ConvertChartInformation(IEnumerable<Stack<char>> stacks) =>
        stacks.Select(stack => new Stack<char>(stack)).ToList();

    private static List<Stack<char>> MoveCrates(string line, List<Stack<char>> stacks, bool oneAtATime)
    {
        var parts = line.Split(" ");
        var crateCount = int.Parse(parts[1]);
        var finalDestination = stacks[int.Parse(parts[5]) - 1];
        var crateMover = new List<char>();
        while (crateCount > 0)
        {
            crateCount--;
            var crate = stacks[int.Parse(parts[3]) - 1].Pop();
            if (oneAtATime) finalDestination.Push(crate);
            else crateMover.Add(crate);
        }

        if (oneAtATime) return stacks;
        crateMover.Reverse();
        crateMover.ForEach(finalDestination.Push);
        return stacks;
    }

    private static List<Stack<char>> ProcessInitialStackChart(string line, List<Stack<char>> stacks)
    {
        for (var stack = 0; stack <= line.Length / 4; stack++)
        {
            if (stacks.Count <= stack) stacks.Add(new Stack<char>());
            var crateId = line[stack * 4 + 1];
            if (crateId != ' ') stacks[stack].Push(crateId);
        }

        return stacks;
    }
}