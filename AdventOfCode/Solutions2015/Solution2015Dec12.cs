using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions2015;

public partial class Solution2015Dec12 : Solution<long?>
{    
    [GeneratedRegex("(-?\\d+)")]
    private static partial Regex NumberRegex();

    protected override long? FirstSolution(List<string> lines) => lines
        .SelectMany(line => NumberRegex().Matches(line).Select(match => long.Parse(match.Value))).Sum();

    protected override long? SecondSolution(List<string> lines)
    {
        var json = lines.First();
        var doc = JsonDocument.Parse(json);
        // doc.RootElement.
        return lines.SelectMany(line => NumberRegex().Matches(line).Select(match => long.Parse(match.Value))).Sum();
    }

    [GeneratedRegex("(\\d+)")]
    private static partial Regex MyRegex1();
}