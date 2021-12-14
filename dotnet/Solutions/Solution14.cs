using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utilities;

namespace AdventOfCode2021.Solutions;

public class Solution14 : Solution
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, 10);

    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, 40);
    
    private static long GeneralSolution(List<string> lines, int cycles)
    {
        var pairInsertionMap = new Dictionary<string, (string, string)>();
        var pairCounts = new Dictionary<string, long>();
        lines.GetRange(2, lines.Count -2).ForEach(line =>
        {
            var input = line[..2];
            pairCounts.Add(input, 0);
            pairInsertionMap.Add(input, (string.Concat(line.AsSpan(0,1), line.AsSpan(6,1)), string.Concat(line.AsSpan(6,1), line.AsSpan(1,1))));
        });
        var polymerTemplate = lines.First();
        for (var i = 0; i < polymerTemplate.Length - 1; i++) pairCounts[polymerTemplate.Substring(i, 2)]++;
        var cycle = 0;
        while (cycle < cycles)
        {
            pairCounts.ToList().ForEach(entry =>
            {
                var outputs = pairInsertionMap[entry.Key];
                pairCounts[outputs.Item1] += entry.Value;
                pairCounts[outputs.Item2] += entry.Value;
                pairCounts[entry.Key] -= entry.Value;
            });
            cycle++;
        }

        var letterCountDictionary = pairCounts.Keys
            .SelectMany(k => new List<string> { k[0].ToString(), k[1].ToString() }).ToHashSet()
            .ToDictionary(x => x, x => (long)0);
        foreach (var (key, value) in pairCounts)
        {
            letterCountDictionary[key[0].ToString()] += value;
            letterCountDictionary[key[1].ToString()] += value;
        }
        // the dictionary will count each letter twice, as the start and end of a pair, except the ones that are 
        // at the ends of the string, which are the same ones as in the initial string.  So we add one to double count them too.
        letterCountDictionary[lines[0][0].ToString()]++;
        letterCountDictionary[lines[0][lines[0].Length - 1].ToString()]++;

        var countList = letterCountDictionary.Values.Select(v => v / 2).ToList();
        countList.Sort();
        return countList.Last() - countList.First();
    }
}