using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;

namespace AdventOfCode.Solutions2022;

public class Solution2022Dec10 : Solution
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines);
    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines);

    // where x is cycle, y is value, z is sum of interesting
    private long GeneralSolution(List<string> lines)
    {
        var stringBuilder = new StringBuilder();
        var result = lines.Aggregate(new Vector3(1, 1, 0), (values, line) =>
        {
            stringBuilder.Append(Math.Abs((values.X - 1) % 40 - values.Y) < 2 ? '#' : '.');
            values += new Vector3(1, 0, 0);
            if ((values.X - 20) % 40 == 0) values += new Vector3(0, 0, values.Y * values.X);
            if (line == "noop") return values;
            stringBuilder.Append(Math.Abs(values.X % 40 - values.Y - 1) < 2 ? '#' : '.');
            values += new Vector3(1, int.Parse(line.Split(" ")[1]), 0);
            if ((values.X - 20) % 40 == 0) values += new Vector3(0, 0, values.Y * values.X);
            return values;
        });
        var str = stringBuilder.ToString();
        Enumerable.Range(0, 6).ToList()
            .ForEach(line => Console.WriteLine(str.Substring(line * 40, 40)));
        Console.WriteLine();
        Console.WriteLine();
        return (long)result.Z;
    }
}