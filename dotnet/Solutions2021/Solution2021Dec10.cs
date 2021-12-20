using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions2021;

public class Solution2021Dec10 : Solution
{
    private readonly Dictionary<char, (char, int)> _closingCharValues = new()
    {
        { ')', ('(', 3) },
        { ']', ('[', 57) },
        { '}', ('{', 1197) },
        { '>', ('<', 25137) }
    };

    private readonly Dictionary<char, int> _stackedCharValues = new()
    {
        { '(', 1 },
        { '[', 2 },
        { '{', 3 },
        { '<', 4 }
    };

    protected override long FirstSolution(List<string> lines)
    {
        return lines.Select(line => line.ToCharArray())
            .Select(ValueChars)
            .Select(v => v.Item1)
            .Where(x => x >= 0)
            .ToList().Sum();
    }

    protected override long SecondSolution(List<string> lines)
    {
        var results = lines.Select(line => line.ToCharArray())
            .Select(ValueChars)
            .Select(v => v.Item2)
            .Where(x => x >= 0)
            .ToList();
        results.Sort();
        return results.ElementAt(results.Count / 2);
    }

    private (long, long) ValueChars(char[] chars)
    {
        var stack = new Stack<char>();
        foreach (var c in chars)
            if (!_closingCharValues.TryGetValue(c, out var outVar))
            {
                stack.Push(c);
            }
            else
            {
                var (closer, value) = outVar;
                if (!stack.Pop().Equals(closer)) return (value, -1);
            }

        return (-1, stack.ToList().Aggregate((long)0, (acc, c) => acc * 5 + _stackedCharValues[c]));
    }

    private static int GeneralSolution(IReadOnlyCollection<string> lines, int indexesBack)
    {
        return 5;
    }
}