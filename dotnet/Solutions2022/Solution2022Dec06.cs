using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2022;

public class Solution2022Dec06 : Solution
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines.First(), 4);
    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines.First(), 14);

    private static long GeneralSolution(string line, int count) =>
        Enumerable.Range(count, line.Length - count)
            .First(i => new HashSet<char>(line.Substring(i - count, count)).Count.Equals(count));
}