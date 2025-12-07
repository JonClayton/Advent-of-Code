namespace AdventOfCode.Solutions2025;

// ReSharper disable once UnusedType.Global
public class Solution2025Dec00 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long? SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(List<string> lines, bool isFirstSolution) => isFirstSolution ? 42 : lines.Count;
}