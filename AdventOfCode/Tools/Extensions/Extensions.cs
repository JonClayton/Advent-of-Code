namespace AdventOfCode.Tools.Extensions;

public static class Extensions
{
    public static long Product(this IEnumerable<long> nums)
    {
        return nums.Aggregate((long)1, (i, i1) => i * i1);
    }

    public static bool Equals(this List<int> a, List<int> b)
    {
        return a.Count == b.Count && a.Select((item, index) => b[index] == item).All(same => same);
    }

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

    extension(int)
    {
        /// <summary>Converts the string representation of a number in invariant culture to its 32-bit signed integer equivalent.</summary>
        /// <param name="s">A string containing a number to convert.</param>
        /// <returns>A 32-bit signed integer equivalent to the number specified in s.</returns>
        public static int Read(string s)
        {
            return int.Parse(s, CultureInfo.InvariantCulture);
        }
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