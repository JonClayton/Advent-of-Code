using System.Collections.Generic;
using System.Linq;
using OldAdventOfCode.Classes;
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2021;

public class Solution2021Dec06 : Solution
{
    protected override long FirstSolution(List<string> lines) => ModelPopulationGrowth(lines, 80);

    protected override long SecondSolution(List<string> lines) => ModelPopulationGrowth(lines, 256);

    private static long ModelPopulationGrowth(IEnumerable<string> lines, int generations) => 
        new Population(lines).Cycle(generations).Result();

    private class Population : CohortCounter<int>
    {
        public Population(IEnumerable<string> lines) => ReadInitialValues(lines.First()
                .Split(",")
                .Select(int.Parse)
                .GroupBy(i => i)
                .Select(g => (g.Key, (long)g.Count())));
            
        public override long Result() => CohortCounts.Values.Aggregate((a, x) => a + x);

        protected override void Cycle()
        {
            CohortCounts = CohortCounts.ToDictionary(x => x.Key - 1, x => x.Value);
            CohortCounts.Remove(-1, out var births);
            CohortCounts[6] = births + CohortCounts.GetValueOrDefault(6, 0);
            CohortCounts[8] = births;
        }
    }
}