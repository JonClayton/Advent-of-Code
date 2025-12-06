using System.Text;

namespace AdventOfCode.Tools;

public static class Extensions
{
    extension<TType>(HashSet<TType> hashSet)
    {
        public HashSet<TType> AddAndReturn(TType input)
        { 
            hashSet.Add(input);
            return hashSet;
        }

        public void AddRange(IEnumerable<TType> input)
        {
            foreach (var member in input) hashSet.Add(member);
        }

        public HashSet<TType> AddRangeAndReturn(IEnumerable<TType> input)
        {
            hashSet.AddRange(input);
            return hashSet;
        }
    }

    public static long Product(this IEnumerable<long> nums)
    {
        return nums.Aggregate((long)1, (i, i1) => i * i1);
    }

    extension(string source)
    {
        public string RemoveWhitespace()
        {
            var builder = new StringBuilder(source.Length);
            foreach (var c in source.Where(c => !char.IsWhiteSpace(c)))
                builder.Append(c);
            return source.Length == builder.Length ? source : builder.ToString();
        }

        public string ConcatInOrder(string b) => string.Compare(source, b, StringComparison.InvariantCulture) < 0 ? source + b : b + source;
    }

    public static bool Equals(this List<int> a, List<int> b)
    {
        return a.Count == b.Count && a.Select((item, index) => b[index] == item).All(same => same);
    }

    // public static int Product(this IEnumerable<int> nums)
    // {
    //     return nums.Aggregate(1, (i, i1) => i * i1);
    // }

    // public static int ToInteger(this List<int> nums)
    // {
    //     return Enumerable.Range(0, nums.Count)
    //         .Reverse()
    //         .Select(n => Enumerable.Repeat(10, n + 1).Product() / 10)
    //         .Zip(nums)
    //         .Select(z => z.First * z.Second)
    //         .Sum();
    // }
    //
    // public static int ToInteger(this List<byte> nums)
    // {
    //     return Enumerable.Range(0, nums.Count)
    //         .Reverse()
    //         .Select(n => Enumerable.Repeat(2, n + 1).Product() / 2)
    //         .Zip(nums)
    //         .Select(z => z.First * z.Second)
    //         .Sum();
    // }
    //
    // public static IEnumerable<TType> Pop<TType>(this List<TType> source, int count = 1, bool reverse = false)
    // {
    //     var result = source.TakeLast(count).ToList();
    //     source.RemoveRange(source.Count - count, count);
    //     if (reverse) result.Reverse();
    //     return result;
    // }
}