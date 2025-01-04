using System.Collections.Generic;
using System.Linq;
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2021;

public class Solution2021Dec01 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, 1);
    }

    protected override long SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, 3);
    }

    private static long GeneralSolution(IReadOnlyCollection<string> lines, int indexesBack)
    {
        return ConvertToIntegerList(lines)
            .Where((v, i) => i >= indexesBack && v > ConvertToIntegerList(lines)[i - indexesBack]).Count();
    }
}