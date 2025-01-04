using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions2015;

public partial class Solution2015Dec08 : Solution<long?>
{
    [GeneratedRegex(@"\\x(\d|[a-f]){2}")]
    private static partial Regex AsciiRegex();
    
    [GeneratedRegex(@"""|\\")]
    private static partial Regex EncodingRegex();

    protected override long? FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long? SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(List<string> lines, bool isFirstSolution) => isFirstSolution
        ? lines.Select(DecodeCountDelta).Sum()
        : lines.Select(EncodeCountDelta).Sum();

    private static long DecodeCountDelta(string line) => line.Length - AsciiRegex()
        .Replace(line[1..^1].Replace(@"\""", @"""").Replace(@"\\", @"\"), "Z").RemoveWhitespace().Length;
    
    private static long EncodeCountDelta(string line) => EncodingRegex().Matches(line).Count + 2;
}

