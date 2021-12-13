using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Utilities
{
    public static class Extensions
    {
        public static int Product(this IEnumerable<int> nums) => nums.Aggregate(1, (i, i1) => i * i1);

        public static int ToInteger(this List<int> nums) => 
            Enumerable.Range(0, nums.Count)
                .Reverse()
                .Select(n => Enumerable.Repeat(10, n + 1).Product()/10)
                .Zip(nums)
                .Select(z => z.First * z.Second)
                .Sum();
        
        public static int ToInteger(this List<byte> nums) => 
            Enumerable.Range(0, nums.Count)
                .Reverse()
                .Select(n => Enumerable.Repeat(2, n + 1).Product()/2)
                .Zip(nums)
                .Select(z => z.First * z.Second)
                .Sum();
    }
}