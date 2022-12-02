namespace AdventOfCode.Solutions2022;

public class Solution2022Dec02 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        return lines
            .GroupBy(strings => strings)
            .Select(group => (group.Key, group.Count()))
            .Aggregate((long)0, (score, next) => score + next.Item2 * GetValueFirst(next.Item1));
    }

    protected override long SecondSolution(List<string> lines)
    {
        return lines
            .GroupBy(strings => strings)
            .Select(group => (group.Key, group.Count()))
            .Aggregate((long)0, (score, next) => score + next.Item2 * GetValueSecond(next.Item1));
    }

    private static long GetValueFirst(string str)
    {
        var result = (str[2] - str[0] - 1) % 3;
        var valueOfSelection = str[2] - 87;
        return result * 3 + valueOfSelection;
    }
    
    private static long GetValueSecond(string str)
    {
        var result = str[2] - 88;
        var valueOfSelection = (str[0] + str[2] - 1) % 3 + 1;
        return result * 3 + valueOfSelection;
    }
}