namespace AdventOfCode.Solutions2015;

public class Solution2015Dec13 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long? SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        var relationships = lines.Select(line => new Relationship(line)).ToList();
        var guests = relationships.Select(relationship => relationship.Person).ToHashSet();
        if (!isFirstSolution)
        {
            relationships.AddRange(guests.Select(guest => new Relationship($"Me . . 0 {guest}")));
            relationships.AddRange(guests.Select(guest => new Relationship($"{guest} . . 0 Me")));
            guests.Add("Me");
        }
        var happiness = relationships.ToDictionary(r => r.Person + r.Neighbor, r => r.IncrementalHappiness);
        var permutations = Utilities.GetPermutations(guests);
        var arrangements = permutations.Select(list =>
        {
            var neighbors = new List<string>(list);
            neighbors.Add(neighbors[0]);
            neighbors.RemoveAt(0);
            var oneWay = list.Zip(neighbors).Select(tuple => tuple.First + tuple.Second).ToList();
            var otherWay = neighbors.Zip(list).Select(tuple => tuple.First + tuple.Second).ToList();
            oneWay.AddRange(otherWay);
            return oneWay;
        }).ToList();
        return Utilities.FindMaxMinLookup(arrangements, happiness);
    }
    
    private class Relationship
    {
        public Relationship(string input)
        {
            var chunks = input.Split(" ");
            Person = chunks[0];
            Neighbor = chunks[^1].Trim('.');
            IncrementalHappiness = long.Parse(chunks[3]) * (chunks[2] == "gain" ? 1 : -1);
        }

        public string Person  { get; }
        public string Neighbor  { get; }
        public long IncrementalHappiness { get; }
    }
}