namespace AdventOfCode.Solutions2015;

public class Solution2015Dec09 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long? SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        var pairs = lines.Select(line => new CityPair(line)).ToList();
        var distances = pairs.ToDictionary(pair => pair.Cities[0].ConcatInOrder(pair.Cities[1]), pair => pair.Distance);
        var cities = pairs.SelectMany(pair => pair.Cities).ToHashSet();
        var permutations = GetPermutations(cities);
        var tracker = isFirstSolution ? long.MaxValue : 0;
        foreach (var permutation in permutations)
        {
            long distance = 0;
            for (var i = 1; i < permutation.Count; i++)
                distance += distances[permutation[i].ConcatInOrder(permutation[i - 1])];
            if (CompareToTracker(distance)) tracker = distance;
        }

        return tracker;
        
        bool CompareToTracker(long value) => isFirstSolution ? value < tracker : value > tracker;
    }

    private static List<List<string>> GetPermutations(HashSet<string> inputs)
    {
        var permutations = inputs.Select(city => new List<string>{city}).ToList();
        for (var i = 1; i < inputs.Count; i++)
        {
            permutations = permutations.SelectMany(permutation =>
            {
                var options = new HashSet<string>(inputs);
                permutation.ForEach(city => options.Remove(city));
                return options.Select(city => new List<string>(permutation).Append(city).ToList());
            }).ToList();
        }
        return permutations;
    }

    private class CityPair
    {
        public CityPair(string input)
        {
            var chunks = input.Split(" = ");
            Distance = long.Parse(chunks[1]);
            Cities = chunks[0].Split(" to ").ToList();
        }

        public List<string> Cities  { get; }
        public long Distance { get; }
    }
}