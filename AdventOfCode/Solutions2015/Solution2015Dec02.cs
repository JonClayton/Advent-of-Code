namespace AdventOfCode.Solutions2015;

public class Solution2015Dec02 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines) =>
        GeneralSolution(lines, nums =>
        {
            var sides = new List<long> { nums[0] * nums[1], nums[0] * nums[2], nums[1] * nums[2] };
            return 2 * sides.Sum() + sides.Min();
        });

    protected override long? SecondSolution(List<string> lines) =>
        GeneralSolution(lines, nums => nums.Product() + 2 * (nums.Sum() - nums.Max()));
    
    private static long? GeneralSolution(List<string> lines, Func<List<long>, long> selector) =>
        lines.Select(line => line.Split("x").Select(long.Parse).ToList()).Select(selector).Sum();
}