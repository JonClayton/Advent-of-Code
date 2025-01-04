using System.Text.RegularExpressions;
using OldAdventOfCode.Classes2024;

namespace OldAdventOfCode.Solutions2024;

public partial class Solution2024Dec03 : Solution2024
{
    [GeneratedRegex(@"^mul\(")]
    private static partial Regex MulStartRegex();
    
    [GeneratedRegex(@"mul\(|do\(\)|don't\(\)")]
    private static partial Regex NextCommandRegex();

    [GeneratedRegex(@"mul\(\d*,\d*\)")]
    private static partial Regex NestedMulRegex();
    
    [GeneratedRegex(@"^\d*")]
    private static partial Regex IntRegex();
    
    [GeneratedRegex(@"^\d*\)")]
    private static partial Regex IntWithCloseRegex();
    
    [GeneratedRegex(@"^\d*,")]
    private static partial Regex IntWithCommaRegex();

    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(IEnumerable<string> lines, bool alwaysDo)
    { 
        var include = true;
        long sum = 0;
        foreach (var line in lines)
        { 
            var str = line;
            while (true)
            {
                str = PruneToNextCommand(str);
                if (str.Length == 0) break;
                (var value, str) = ValueMul(str);
            }
        }
        return sum;

        string PruneToNextCommand(string str)
        {
            while (true)
            {
                var match = NextCommandRegex().Match(str);
                if (!match.Success) return "";
                var nextLocation = match.Index;
                if (nextLocation == -1) return "";
                var start = nextLocation + 4;
                var end = str.LastIndexOf(')');
                if (!match.Value.Contains("do")) return str.Substring(start, end - start + 1);
                include = match.Value.Equals("do()");
                str = str.Substring(start, end - start + 1);
            }
        }

        (long value, string remainder) ValueMul(string str)
        {
            long x;
            if (MulStartRegex().IsMatch(str))
            {
                Console.WriteLine("nested mul!");
                (x, str) = ValueMul(str[4..]);
            }
            else (x, str) = ValueInt(str);
            if (x == 0 || str.First() != ',') return (0, str);
            str = str[1..];
            long y;
            if (MulStartRegex().IsMatch(str)) (y, str) = ValueMul(str[4..]);
            else (y, str) = ValueInt(str);
            if (y == 0 || str.First() != ')') return (0, str);
            if (include || alwaysDo) sum += x * y;
            return (x * y, str[1..]);
        }
    }

    private static (long value, string remainder) ValueInt(string str)
    {
        var match = IntRegex().Match(str);
        return match.Success ? (long.Parse(match.Value), str[match.Value.Length..]) : (0, str);
    }
    

}