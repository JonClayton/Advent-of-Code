using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Solution1 : Solution
    {
        protected override int FirstSolution(List<string> lines) => GeneralSolution(lines, 1);

        protected override int SecondSolution(List<string> lines) => GeneralSolution(lines, 3);

        private static int GeneralSolution(IReadOnlyCollection<string> lines, int indexesBack) => 
            ConvertToIntegers(lines).Where((v, i) => i >= indexesBack && v > ConvertToIntegers(lines)[i - indexesBack]).Count();
    }
}