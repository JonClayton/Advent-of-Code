using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Solutions
{
    public class Solution9 : Solution
    {
        protected override int FirstSolution(List<string> lines)
        {
            var total = 0;
            for (var row = 0; row < lines.Count; row++)
            {
                var line = lines[row];
                for (var col = 0; col < lines[row].Length())
                {
                    var val = line[col];
                    if (row < 1 || val <= lines[row - 1][col])
                    {
                        if (row + 1 == lines.Count || val <= lines[row + 1][col])
                        {
                            if (col < 1 || val <= lines[row - 1][col])
                            
                        }
                        
                    } 
                }
            }
        }

        protected override int SecondSolution(List<string> lines) => 42;
    }
}