using OldAdventOfCode.Classes2024;

namespace OldAdventOfCode.Solutions2024;

public class Solution2024Dec02 : Solution2024
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(IEnumerable<string> lines, bool isStrict)
    {
        var reports = ConvertToIntegerLists(lines, " ");
        return reports.Count(report => ReportIsSafe(report, !isStrict));
    }

    private static bool ReportIsSafe(List<int> report, bool allowOneDrop)
    {
        var isAscending = report[1] > report[0];
        var previous = report[0];
        for (var i = 1; i < report.Count; i++)
        {
            var current = report[i];
            if (LevelIsUnSafe(current))
            {
                if (!allowOneDrop) return false;
                var dropPrevious = report.GetRange(0, report.Count);
                dropPrevious.RemoveAt(i-1);
                if (ReportIsSafe(dropPrevious, false)) return true;
                var dropCurrent = report.GetRange(0, report.Count);
                dropCurrent.RemoveAt(i);
                if (ReportIsSafe(dropCurrent, false)) return true;
                return i == 2 && ReportIsSafe(report.GetRange(1, report.Count-1), false);
            }
            previous = current;
        }
        return true;

        bool LevelIsUnSafe(int level)
        {
            if (level == previous) return true;
            if (level < previous == isAscending) return true;
            return (level - previous) * (isAscending ? 1 : -1) > 3;
        }
    }
}