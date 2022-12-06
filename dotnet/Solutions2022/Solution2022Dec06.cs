using System.Text;

namespace AdventOfCode.Solutions2022;

public class Solution2022Dec06 : Solution
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, 4);
    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, 14);

    private static long GeneralSolution(List<string> lines, int count) 
    {
        var line = lines.First();
        var i = count;
        while (true)
        {
            if (new HashSet<char>(line[(i-count)..i]).Count.Equals(count)) return i;
            i++;
        }
    }
}