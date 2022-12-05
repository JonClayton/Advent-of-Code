namespace AdventOfCode.Utilities;

public static class Extensions
{
    public static int Product(this IEnumerable<int> nums)
    {
        return nums.Aggregate(1, (i, i1) => i * i1);
    }
    public static long Product(this IEnumerable<long> nums)
    {
        return nums.Aggregate((long)1, (i, i1) => i * i1);
    }

    public static int ToInteger(this List<int> nums)
    {
        return Enumerable.Range(0, nums.Count)
            .Reverse()
            .Select(n => Enumerable.Repeat(10, n + 1).Product() / 10)
            .Zip(nums)
            .Select(z => z.First * z.Second)
            .Sum();
    }

    public static int ToInteger(this List<byte> nums)
    {
        return Enumerable.Range(0, nums.Count)
            .Reverse()
            .Select(n => Enumerable.Repeat(2, n + 1).Product() / 2)
            .Zip(nums)
            .Select(z => z.First * z.Second)
            .Sum();
    }

    public static IEnumerable<TType> Pop<TType>(this List<TType> source, int count = 1, bool reverse = false)
    {
        var result = source.TakeLast(count).ToList();
        source.RemoveRange(source.Count - count, count);
        if (reverse) result.Reverse();
        return result;
    }
}