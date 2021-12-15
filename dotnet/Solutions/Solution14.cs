using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Classes;
using AdventOfCode2021.Utilities;

namespace AdventOfCode2021.Solutions;

public class Solution14 : Solution
{
    protected override long FirstSolution(List<string> inputLines) => RunPolymerFactory(inputLines, 10);

    protected override long SecondSolution(List<string> inputLines) => RunPolymerFactory(inputLines, 40);
    
    private static long RunPolymerFactory(List<string> inputLines, int cycleCount) => 
        new PolymerFactory(inputLines).Cycle(cycleCount).Result();

    private class PolymerFactory : CohortCounter<string>
    {
        private readonly Dictionary<string, (string, string)> _pairInsertionMap = new();
        private readonly string _wrapAroundPair;

        public PolymerFactory(List<string> lines)
        {
            _wrapAroundPair = string.Concat(lines[0][0], lines[0][lines[0].Length-1]);
            ReadInitialValues(Enumerable.Range(0, lines[0].Length - 1).Select(i => (lines[0].Substring(i, 2), (long)1)));
            lines.GetRange(2, lines.Count - 2).ForEach(line =>
                _pairInsertionMap.Add(line[..2], (string.Concat(line[0], line[6]), string.Concat(line[6], line[1]))));
        }

        protected override void Cycle() => CohortCounts.ToList().ForEach(entry =>
        {
            var (key, value) = entry;
            CohortCounts[key] -= value;
            var (item1, item2) = _pairInsertionMap[key];
            AddToCohortCounts(item1, value);
            AddToCohortCounts(item2, value);
        });

        public override long Result()
        {
            AddToCohortCounts(_wrapAroundPair, 1);
            var results = CohortCounts.ToList()
                .SelectMany(entry => new List<(string, long)>
                    { (entry.Key[0].ToString(), entry.Value), (entry.Key[1].ToString(), entry.Value) })
                .GroupBy(t => t.Item1)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(t => t.Item2).Sum() / 2);
            return results.Values.Max() - results.Values.Min();
        }
    }
}