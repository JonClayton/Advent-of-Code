namespace AdventOfCode.Tools;

public static class Utilities
{
    public const string RegexForNumberFind = @"\d+";

    private static List<int> Factor(int number)
    {
        return Factor((long)number).Select(n => (int)n).ToList();
    }

    public static List<long> Factor(long number)
    {
        if (number == 0) return [];
        var factors = new HashSet<long> { 1, number };
        if (number == 1) return factors.OrderBy(f => f).ToList();
        var top = (int)Math.Sqrt(number);
        if (top * top == number)
        {
            factors.Add(top);
            var moreFactors = Factor(top);
            foreach (var f1 in moreFactors)
            foreach (var f2 in moreFactors)
                factors.Add(f1 * f2);
            return factors.OrderBy(value => value).ToList();
        }

        for (var factor = 2; factor <= top + 1; ++factor)
        {
            if (number % factor != 0) continue;
            factors.Add(factor);
            var moreFactors = Factor(number / factor);
            var evenMoreFactors = moreFactors.Select(f => f * factor).ToList();
            factors.AddRange(evenMoreFactors);
            factors.AddRange(moreFactors);
            return factors.OrderBy(value => value).ToList();
        }

        return factors.OrderBy(value => value).ToList();
    }

    public static long FindMaxMinLookup<TType>(List<List<TType>> lists, Dictionary<TType, long> dictionary,
        bool isMax = true) where TType : notnull
    {
        var tracker = isMax ? 0 : long.MaxValue;
        foreach (var distance in lists.Select(list => list.Sum(item => dictionary[item])).Where(CompareToTracker))
            tracker = distance;
        return tracker;

        bool CompareToTracker(long value)
        {
            return isMax ? value > tracker : value < tracker;
        }
    }

    public static List<List<TType>> GetPermutations<TType>(HashSet<TType> inputs)
    {
        var permutations = inputs.Select(input => new List<TType> { input }).ToList();
        for (var i = 1; i < inputs.Count; i++)
            permutations = permutations.SelectMany(permutation =>
            {
                var options = new HashSet<TType>(inputs);
                permutation.ForEach(input => options.Remove(input));
                return options.Select(input => new List<TType>(permutation).Append(input).ToList());
            }).ToList();
        return permutations;
    }
}