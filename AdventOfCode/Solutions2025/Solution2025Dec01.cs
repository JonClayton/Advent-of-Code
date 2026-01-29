namespace AdventOfCode.Solutions2025;

public class Solution2025Dec01 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, true);
        // 1023 
    }

    protected override long? SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, false);
        // 5899
    }

    private static long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        var location = 50;
        long stopped = 0;
        long passed = 0;
        foreach (var line in lines)
        {
            var clicks = int.Read(line[1..]);
            var oneIfNotStartZero = location == 0 ? 0 : 1;
            if (line.First() == 'R')
            {
                location += clicks;
                passed += location / 100;
                location %= 100;
            }
            else
            {
                location -= clicks;
                if (location == 0) stopped++;
                if (location >= 0) continue;
                passed += oneIfNotStartZero - location / 100;
                location = (location + 1) % 100 + 99;
            }

            if (location != 0) continue;
            stopped++;
            passed--;
        }

        return isFirstSolution ? stopped : passed + stopped;
    }
}