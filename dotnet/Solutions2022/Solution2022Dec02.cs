namespace AdventOfCode.Solutions2022;

public class Solution2022Dec02 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        return lines.Select(line => FirstDictionary[line]).Sum();
    }

    protected override long SecondSolution(List<string> lines)
    {
        return lines.Select(line => SecondDictionary[line]).Sum();
    }

    private static readonly Dictionary<string, long> FirstDictionary = new()
    {
        { "A X", 4 },
        { "A Y", 8 },
        { "A Z", 3 },
        { "B X", 1 },
        { "B Y", 5 },
        { "B Z", 9 },
        { "C X", 7 },
        { "C Y", 2 },
        { "C Z", 6 },
    };
    
    private static readonly Dictionary<string, long> SecondDictionary = new()
    {
        { "A X", 3 },
        { "A Y", 4 },
        { "A Z", 8 },
        { "B X", 1 },
        { "B Y", 5 },
        { "B Z", 9 },
        { "C X", 2 },
        { "C Y", 6 },
        { "C Z", 7 },
    };

    private static long GeneralSolution(List<string> lines, int count)
    {
        return lines.Select(line => FirstDictionary[line]).Sum();
    }
}