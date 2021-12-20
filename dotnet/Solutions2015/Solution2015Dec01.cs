using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions2015;

public class Solution2015Dec01 : Solution
{
    protected override long FirstSolution(List<string> lines) =>
        lines[0].ToCharArray().GroupBy(c => c).OrderBy(g => g.Key).Select(g => g.Count())
            .Aggregate((acc, count) => acc - count);

    protected override long SecondSolution(List<string> lines)
    {
        var floor = 0;
        var chars = lines[0].ToCharArray();
        var i = 0;
        while(true)
        {
            floor += chars[i] == '(' ? 1 : -1;
            if (floor == -1) return i + 1;
            i++;
        }
    }
}