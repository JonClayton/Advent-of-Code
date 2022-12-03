namespace AdventOfCode.Solutions2022;

public class Solution2022Dec03 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        return lines.Sum(line => GetCharValue(line.Take(line.Length / 2).ToHashSet()
            .Intersect(line.TakeLast(line.Length / 2).ToHashSet()).First()));
    }

    protected override long SecondSolution(List<string> lines)
    {
        return lines.Chunk(3).Sum(chunk =>
            GetCharValue(chunk[0].ToHashSet().Intersect(chunk[1].ToHashSet()).Intersect(chunk[2].ToHashSet()).First()));
    }

    private static long GetCharValue(char c)
    {
        var asciiValue = (int)c;
        return asciiValue - (asciiValue > 96 ? 96 : 38);
    }
}