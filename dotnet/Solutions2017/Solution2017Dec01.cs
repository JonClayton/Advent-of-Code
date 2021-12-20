using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions2017;

public class Solution2017Dec01 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var digits = lines[0].ToCharArray().Select(c => int.Parse(c.ToString())).ToList();
        var sum = digits.First() == digits.Last() ? digits.First() : 0;
        for (var i = 1; i < digits.Count; i++) sum += digits[i - 1] == digits[i] ? digits[i] : 0;
        return sum;
    }

    protected override long SecondSolution(List<string> lines)
    {
        var digits = lines[0].ToCharArray().Select(c => int.Parse(c.ToString())).ToList();
        var sum = 0;
        for (var i = 0; i < digits.Count / 2; i++) sum += digits[i  + digits.Count / 2] == digits[i] ? digits[i] : 0;
        return sum * 2;
    }
}
