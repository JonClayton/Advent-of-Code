using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utilities;

namespace AdventOfCode2021.Solutions;

public class Solution06 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, 80);
    }

    protected override long SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, 256);
    }

    private static long GeneralSolution(List<string> lines, int generations)
    {
        var population = new Population(lines);
        Enumerable.Range(0, generations).ToList().ForEach(_ => population.AgeOneDay());
        return population.TotalPopulation();
    }
}

internal class Population
{
    private Dictionary<int, long> _populationByAge;

    public Population(List<string> lines)
    {
        _populationByAge = lines.First()
            .Split(",")
            .Select(int.Parse)
            .GroupBy(i => i)
            .ToDictionary(g => g.Key, g => (long)g.Count());
    }

    public void AgeOneDay()
    {
        _populationByAge = _populationByAge.ToDictionary(x => x.Key - 1, x => x.Value);
        _populationByAge.Remove(-1, out var births);
        _populationByAge[6] = births + _populationByAge.GetValueOrDefault(6, 0);
        _populationByAge[8] = births;
    }

    public long TotalPopulation()
    {
        return _populationByAge.Values.Aggregate((a, x) => a + x);
    }
}