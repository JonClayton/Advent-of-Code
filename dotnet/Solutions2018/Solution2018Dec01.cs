using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Classes;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions2018;

public class Solution2018Dec01 : Solution
{
    protected override long FirstSolution(List<string> lines) => lines.Select(int.Parse).Sum();

    protected override long SecondSolution(List<string> lines)
    {
        var frequency = 0;
        var observed = new HashSet<int>();
        while (true)
            foreach (var change in lines.Select(i => int.Parse(i.Trim())))
            {
                frequency += change;
                if (!observed.Add(frequency)) return frequency;
            }
    }
}
