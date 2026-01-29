namespace AdventOfCode.Solutions2015;

public class Solution2015Dec17 : Solution<long?>
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
        var liters = lines.Count > 5 ? 150 : 25;
        var sizes = lines.Select(int.Read).ToList();
        if (isFirstSolution) return EvaluateTree(sizes, liters);
        var unitLimit = 0;
        while (true)
        {
            var result = EvaluateTree(sizes, liters, unitLimit);
            if (result > 0) return result;
            unitLimit++;
        }
    }

    private static long EvaluateTree(List<int> sizes, int target, int unitLimit = int.MaxValue, int index = 0,
        int current = 0, int units = 0)
    {
        var isLastContainer = index == sizes.Count - 1;
        var size = sizes[index];
        var skipToNext = isLastContainer || unitLimit <= units
            ? 0
            : EvaluateTree(sizes, target, unitLimit, index + 1, current, units);
        if (size + current == target)
            return 1 + skipToNext;
        if (isLastContainer) return 0;
        return sizes[index] + current < target && unitLimit > units + 1
            ? skipToNext + EvaluateTree(sizes, target, unitLimit, index + 1, current + size, units + 1)
            : skipToNext;
    }
}