namespace AdventOfCode.Tools;

public static class Utilities
{
    public static long FindMaxMinLookup<TType>(List<List<TType>> lists, Dictionary<TType, long> dictionary,
        bool isMax = true) where TType : notnull
    {
        var tracker = isMax ? 0 : long.MaxValue;
        foreach (var distance in lists.Select(list => list.Sum(item => dictionary[item])).Where(CompareToTracker))
            tracker = distance;
        return tracker;
        bool CompareToTracker(long value) => isMax ? value > tracker : value < tracker;
    }
    
    public static List<List<TType>> GetPermutations<TType>(HashSet<TType> inputs)
    {
        var permutations = inputs.Select(input => new List<TType>{input}).ToList();
        for (var i = 1; i < inputs.Count; i++)
        {
            permutations = permutations.SelectMany(permutation =>
            {
                var options = new HashSet<TType>(inputs);
                permutation.ForEach(input => options.Remove(input));
                return options.Select(input => new List<TType>(permutation).Append(input).ToList());
            }).ToList();
        }
        return permutations;
    }
}