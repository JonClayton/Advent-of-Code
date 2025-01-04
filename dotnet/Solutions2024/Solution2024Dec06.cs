using OldAdventOfCode.Classes2024;

namespace OldAdventOfCode.Solutions2024;

public class Solution2024Dec06 : Solution2024
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(IEnumerable<string> lines, bool isFirst)
    {
        var map = new GenericMap<GenericLocation<char>, char>(lines);
        return 0;
    }
}

