namespace AdventOfCode.Solutions2015;

public class Day16 : Solution<long?>
{
    private static readonly Dictionary<string, int> ExpectedPossessions = new()
    {
        { "children", 3 },
        { "cats", 7 },
        { "samoyeds", 2 },
        { "pomeranians", 3 },
        { "akitas", 0 },
        { "vizslas", 0 },
        { "goldfish", 5 },
        { "trees", 3 },
        { "cars", 2 },
        { "perfumes", 1 }
    };

    private static readonly Dictionary<string, int> ExactPossessions = new()
    {
        { "children", 3 },
        { "samoyeds", 2 },
        { "akitas", 0 },
        { "vizslas", 0 },
        { "cars", 2 },
        { "perfumes", 1 }
    };

    private static readonly Dictionary<string, int> LesserPossessions = new()
    {
        { "goldfish", 5 },
        { "pomeranians", 3 }
    };

    private static readonly Dictionary<string, int> GreaterPossessions = new()
    {
        { "trees", 3 },
        { "cats", 7 }
    };

    protected override long? FirstSolution(List<string> lines)
    {
        return lines.Select(line => new Sue(line)).ToList().Single(IsRightSue).Number;
    }

    protected override long? SecondSolution(List<string> lines)
    {
        return lines.Select(line => new Sue(line)).ToList().Single(IsRightSue2).Number;
    }


    private static bool IsRightSue(Sue sue)
    {
        foreach (var expectedPossession in ExpectedPossessions)
            if (sue.Possessions.TryGetValue(expectedPossession.Key, out var actual) &&
                actual != expectedPossession.Value)
                return false;
        return true;
    }

    private static bool IsRightSue2(Sue sue)
    {
        foreach (var possession in ExactPossessions)
            if (sue.Possessions.TryGetValue(possession.Key, out var actual) &&
                actual != possession.Value)
                return false;
        foreach (var possession in GreaterPossessions)
            if (sue.Possessions.TryGetValue(possession.Key, out var actual) &&
                actual <= possession.Value)
                return false;
        foreach (var possession in LesserPossessions)
            if (sue.Possessions.TryGetValue(possession.Key, out var actual) &&
                actual >= possession.Value)
                return false;
        return true;
    }

    private class Sue
    {
        public Sue(string input)
        {
            var firstColon = input.IndexOf(':');
            Number = int.Read(input[4..firstColon]);
            var possessions = input[(firstColon + 1)..].Split(',');
            foreach (var possession in possessions)
            {
                var split = possession.Split(':');
                Possessions.Add(split[0].Trim(), int.Read(split[1]));
            }
        }

        public int Number { get; }

        public Dictionary<string, int> Possessions { get; } = new();
    }
}