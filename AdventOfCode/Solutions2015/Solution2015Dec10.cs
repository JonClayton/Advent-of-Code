namespace AdventOfCode.Solutions2015;

public class Solution2015Dec10 : Solution<long?>
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
        var iterations = isFirstSolution ? 40 : 50;
        var str = lines.First();
        var i = 0;
        while (i < iterations)
        {
            str = LookAndSay(str);
            i++;
        }

        return str.Length;
    }

    private static string LookAndSay(string str)
    {
        var builder = new StringBuilder();
        var counter = 0;
        var last = 'X';
        foreach (var c in str)
            if (c == last)
            {
                counter++;
            }
            else
            {
                if (counter > 0) builder.Append(CultureInfo.InvariantCulture, $"{counter}{last}");
                counter = 1;
                last = c;
            }

        builder.Append(CultureInfo.InvariantCulture, $"{counter}{last}");
        return builder.ToString();
    }
}