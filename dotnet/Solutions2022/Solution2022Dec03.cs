namespace AdventOfCode.Solutions2022;

public class Solution2022Dec03 : Solution
{
    protected override long FirstSolution(List<string> lines) =>
        lines
            .Select(Halve)
            .Select(chunk => chunk.Aggregate((a, n) => a.Intersect(n)).First())
            .Select(GetCharValue)
            .Sum();

    protected override long SecondSolution(List<string> lines) =>
        lines
            .Chunk(3)
            .Select(chunk => chunk.Aggregate((a, n) => string.Concat(a.Intersect(n))).First())
            .Select(GetCharValue)
            .Sum();
    
    private static long GetCharValue(char c) => c - (c > 96 ? 96 : 38);
    private static IEnumerable<IEnumerable<char>> Halve(string s) => 
        new List<IEnumerable<char>> { s.Take(s.Length / 2), s.TakeLast(s.Length / 2) };
}