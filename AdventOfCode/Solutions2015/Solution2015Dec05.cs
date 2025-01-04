using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions2015;

public partial class Solution2015Dec05 : Solution<long?>
{
    [GeneratedRegex("a|e|i|o|u")]
    private static partial Regex HasVowelsRegex();

    [GeneratedRegex(@"(.)\1")]
    private static partial Regex HasPairRegex();

    [GeneratedRegex("ab|cd|pq|xy")]
    private static partial Regex HasBadRegex();

    private readonly Regex _regex1 = HasVowelsRegex();
    private readonly Regex _regex2 = HasPairRegex();
    private readonly Regex _regex3 = HasBadRegex();

    protected override long? FirstSolution(List<string> lines) => lines.Where(IsNiceFirst).Count();
    protected override long? SecondSolution(List<string> lines) => lines.Where(IsNiceSecond).Count();

    private static bool HasBookends(string arg)
    {
        for (var i = 2; i < arg.Length; i++)
            if (arg[i] == arg[i - 2])
                return true;
        return false;
    }
    
    private static bool HasTwinPair(string arg)
    {
        var hashSet = new HashSet<string>();
        var priorPair = string.Empty;
        for (var i = 0; i < arg.Length - 1; i++)
        {
            var pair = arg.Substring(i, 2);
            if (priorPair == pair)
            {
                priorPair = string.Empty;
                continue;
            }

            if (!hashSet.Add(pair)) return true;
            priorPair = pair;
        }

        return false;
    }
    
    private static bool IsNiceSecond(string arg) => HasBookends(arg) && HasTwinPair(arg);

    private bool IsNiceFirst(string arg) =>
        !_regex3.IsMatch(arg) && _regex2.IsMatch(arg) && _regex1.Matches(arg).Count > 2;
}