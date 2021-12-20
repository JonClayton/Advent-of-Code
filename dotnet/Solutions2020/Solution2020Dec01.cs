using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions2020;

public class Solution2020Dec01 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var complements = new HashSet<int>();
        foreach (var num in (lines.Select(int.Parse)))
        {
            if (complements.Contains(num)) return num * (2020 - num);
            complements.Add(2020 - num);
        }
        return 0;
    }

    protected override long SecondSolution(List<string> lines)
    {
        var nums = lines.Select(int.Parse).ToList();
        foreach (var num1 in nums)
        {
            foreach (var num2 in nums)
            {
                foreach (var num3 in nums.Where(num3 => num1 + num2 + num3 == 2020))
                    return num1 * num2 * num3;
            }
        }

        return 0;
    }
}
