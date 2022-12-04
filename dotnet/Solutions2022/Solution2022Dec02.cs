namespace AdventOfCode.Solutions2022;

public class Solution2022Dec02 : Solution
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, GetValueFirst);
    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, GetValueSecond);

    private static long GeneralSolution(IEnumerable<string> lines, Func<char, char, long> getValue) => lines
        .GroupBy(strings => strings)
        .Aggregate((long)0, (score, next) => score + next.Count() * getValue(next.Key[0], next.Key[2]));
    
    private static long GetValueFirst(char a, char b) => (b - a - 1) % 3 * 3 + b - 87;

    private static long GetValueSecond(char a, char b) => (b - 88) * 3 + (a + b - 1) % 3 + 1;
}