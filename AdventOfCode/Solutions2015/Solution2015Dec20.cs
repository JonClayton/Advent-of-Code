namespace AdventOfCode.Solutions2015;

public class Solution2015Dec20 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long? SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        var target = long.Parse(lines[0]);
        var house = 0;
        while (true)
        {
            house++;
            var result = isFirstSolution ? FirstScore(house) : SecondScore(house);
            if (result >= target) return house;
        }
    }

    private static long FirstScore(long num) => Utilities.Factor(num).Select(factor => num / factor * 10).Sum();
    private static long SecondScore(long num) => FactorsWithComplementUnder50(num).Select(factor => factor * 11).Sum();

    private static List<long> FactorsWithComplementUnder50(long num)
    {
        var factors = new List<long>();
        var sqrt = (int)Math.Sqrt(num);
        for (var i = 1; i < 51 && i <= sqrt; i++)
            if (num % i == 0)
            {
                var complement = num / i;
                factors.Add(complement);
                if (complement <= 50 && complement != i) factors.Add(i);
            }
        return factors;
    }
}