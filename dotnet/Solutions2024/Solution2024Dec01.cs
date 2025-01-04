using OldAdventOfCode.Classes2024;

namespace OldAdventOfCode.Solutions2024;

public class Solution2024Dec01 : Solution2024
{
    protected override long FirstSolution(List<string> lines) => GetLists(lines)
        .Aggregate(
            (agg, next) =>
            {
                agg.Sort();
                next.Sort();
                return agg.Zip(next, (first, second) => first > second ? first - second : second - first).ToList();
            }).Sum();

    protected override long SecondSolution(List<string> lines)
    {
        var lists = GetLists(lines);
        var groups = lists.Last().GroupBy(item => item).ToDictionary(group => group.Key, group => group.Count());
        return lists.First().Where(i => groups.ContainsKey(i)).Sum(i => groups[i] * i);
    }

    private static List<List<int>> GetLists(IEnumerable<string> lines) => ConvertToIntegerLists(lines, "   ")
        .Aggregate(Enumerable.Range(1, 2).Select(i => new List<int>()).ToList(), (agg, next) =>
        {
            agg.First().Add(next.First());
            agg.Last().Add(next.Last());
            return agg;
        });
}