namespace AdventOfCode.Solutions2022;

public class Solution2022Dec04 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, 1);
    }

    protected override long SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, 3);
    }

    private static long GeneralSolution(IEnumerable<string> lines, int count)
    {
        return 100;
    }
}