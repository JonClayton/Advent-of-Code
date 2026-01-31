namespace AdventOfCode.Solutions2015;

public class Day09 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, true);
    }

    protected override long? SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, false);
    }

    private static long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        var pairs = lines.Select(line => new CityPair(line)).ToList();
        var pairDistances =
            pairs.ToDictionary(pair => pair.Cities[0].ConcatInOrder(pair.Cities[1]), pair => pair.Distance);
        var cities = pairs.SelectMany(pair => pair.Cities).ToHashSet();
        var segments = Utilities.GetPermutations(cities).Select(list => list.Aggregate(new List<string> { "" },
            (newList, next) =>
            {
                newList[^1] = next.ConcatInOrder(newList[^1]);
                newList.Add(next);
                return newList;
            })[1..^1]).ToList();
        return Utilities.FindMaxMinLookup(segments, pairDistances, !isFirstSolution);
    }

    private class CityPair
    {
        public CityPair(string input)
        {
            var chunks = input.Split(" = ");
            Distance = long.Parse(chunks[1], CultureInfo.InvariantCulture);
            Cities = chunks[0].Split(" to ").ToList();
        }

        public List<string> Cities { get; }
        public long Distance { get; }
    }
}