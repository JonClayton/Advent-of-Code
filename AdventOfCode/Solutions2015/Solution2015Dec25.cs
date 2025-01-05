namespace AdventOfCode.Solutions2015;

public class Solution2015Dec25 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long? SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(List<string> lines, bool isFirstSolution) => isFirstSolution ? 42 : lines.Count;
}