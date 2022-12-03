namespace AdventOfCode.Solutions2022;

public class Solution2022Dec03 : Solution
{
    protected override long FirstSolution(List<string> lines) => lines.Sum(line =>
        GetCharValue(line.Take(line.Length / 2).Intersect(line.TakeLast(line.Length / 2)).First()));

    protected override long SecondSolution(List<string> lines) => lines.Chunk(3)
        .Sum(chunk => GetCharValue(chunk[0].Intersect(chunk[1]).Intersect(chunk[2]).First()));

    private static long GetCharValue(char c) => c - (c > 96 ? 96 : 38);
}