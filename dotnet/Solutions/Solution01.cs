using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utilities;

namespace AdventOfCode2021.Solutions
{
    public class Solution01 : Solution
    {
        protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, 1);

        protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, 3);

        private static long GeneralSolution(IReadOnlyCollection<string> lines, int indexesBack) => 
            ConvertToIntegers(lines).Where((v, i) => i >= indexesBack && v > ConvertToIntegers(lines)[i - indexesBack]).Count();
    }
}