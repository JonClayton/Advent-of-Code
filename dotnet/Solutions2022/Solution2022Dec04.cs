namespace AdventOfCode.Solutions2022;

public class Solution2022Dec04 : Solution
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, IsSubset);
    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, HasOverlap);

    private static long GeneralSolution(IEnumerable<string> lines, Func<List<List<int>>, bool> counter) =>
        lines
            .Select(line => line.Split(",")
                .Select(nums => nums.Split("-")
                    .Select(int.Parse).ToList())
                .ToList())
            .Count(counter);

    private static bool IsSubset(List<List<int>> pairs) =>
        pairs.First().First() == pairs.Last().First() ||
        pairs.First().Last() == pairs.Last().Last() ||
        pairs.First().First() < pairs.Last().First() == pairs.First().Last() > pairs.Last().Last();

    private static bool HasOverlap(List<List<int>> pairs) =>
        (pairs.First().First() <= pairs.Last().Last() && pairs.First().First() >= pairs.Last().First()) ||
        (pairs.First().Last() <= pairs.Last().Last() && pairs.First().Last() >= pairs.Last().First()) ||
        (pairs.First().First() < pairs.Last().First() && pairs.First().Last() > pairs.Last().Last());
}