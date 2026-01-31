namespace AdventOfCode.Solutions2017;

public class Day03 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, true);
    }

    protected override long? SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, false);
    }

    private static long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        return isFirstSolution ? 42 : lines.Count;
    }
}
