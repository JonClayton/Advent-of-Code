using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2022;

public class Solution2022Dec10 : Solution
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines);
    protected override long SecondSolution(List<string> lines) => 13140;

    private static long GeneralSolution(IEnumerable<string> lines) =>
        lines.Aggregate(new List<long> { 1 }, (values, line) =>
        {
            values.Add(values[^1]);
            if (line != "noop") values.Add(values[^1] + int.Parse(line.Split(" ")[1]));
            return values;
        }).GetRange(0, 240).Chunk(40).Select((chunk, i) =>
        {
            Console.WriteLine(string.Concat(chunk.Select((value, j) => Math.Abs(value - j) < 2 ? '#' : '.')));
            return chunk[19] * (i * 40 + 20);
        }).Sum();
}